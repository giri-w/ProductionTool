using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using WinSCP;
using System.Linq;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep04 : TestStep
    {
        [Obsolete]
        public FMGTestStep04()
            : this(null)
        { }


        public FMGTestStep04(TestManager testManager)
            : base(testManager)
        {
            // form setup
            this.Name				= "Data Selection";
            this.Instructions 		= 
										"- Select the measurement result from the list\n" +
										"- Copy the folder name and paste it in the download box\n" +
										"- Press UPDATE to download the measurement to test folder";
            this.SupportingImage 	= string.Empty;
			this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.Results 			= new List<Result>();
            
            // forward and backward handler
            this.OnTestUpdated(false);
            
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Update)
            {

                directoryList();
              
            }

        }

        public static void directoryList()
        {
            try
            {
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol	 					= Protocol.Ftp,
                    HostName	 					= "172.17.5.108",
                    PortNumber 	 					= 990,
                    UserName 	 					= "Service Engineer",
                    Password 	 					= "192+SERV.HEMI",
                    FtpSecure 	 					= FtpSecure.Implicit,
                    TlsHostCertificateFingerprint   = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f"
                    };

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);
                    RemoteDirectoryInfo directory =
                    session.ListDirectory("/Measurements/298");

                    
                    IEnumerable<RemoteFileInfo> latest =
                    directory.Files
                        .Where(file => file.IsDirectory)
                        .OrderByDescending(file => file.FullName)
                        .Take(10);

                        
                    int counter = 0;
                    bool exist;
                    foreach (RemoteFileInfo fileInfo in latest)
                    {

                        exist = false;
                        Console.WriteLine(
                            "{0} with size {1}, permissions {2} and last modification at {3}\n",
                            fileInfo.Name, fileInfo.Length, fileInfo.FilePermissions,
                            fileInfo.LastWriteTime);
                            counter++;
                            if (counter > 5)
                                break;
                        try {
                            if (counter > 1)
                            {
                                RemoteDirectoryInfo check = session.ListDirectory("/Measurements/298/" + fileInfo.Name + "/Raw data");
                                List<string> termsList = new List<string>();
                                foreach (RemoteFileInfo listInfo in check.Files)
                                {
                                    termsList.Add(listInfo.Name);
                                }
                                string[] terms = termsList.ToArray();

                                bool a = Array.Exists(terms, element => element == "left_high_reflection.png");
                                bool b = Array.Exists(terms, element => element == "right_high_reflection.png");
                                
                                if (a && b)
                                    Console.WriteLine("\n File ada");
                                else
                                    Console.WriteLine("\n Tidak ada");
                            }
                            
                            
                        }
                        catch{ }
                        
                        


                    }
                        // Your code
                        Console.WriteLine("FINISH");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
               
            }
        }

     



    }
}
