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
        

        private int LEDbrightness;
        private string selectedData;

        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 = "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath = "/Settings/system/testFolder/";
       
        private const string InstructionText =
                                "LED Brightness during measurement\n\n" +
                                "Recommended Value : 50\n" +
                                "Current Value     : {0}\n" +
                                "Set new value in the text box and Press Change\n" +
                                "and Press Next to continue to the next step\n";


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep04"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTestStep04()
            : this(null)
        { }


        public FMGTestStep04(TestManager testManager)
            : base(testManager)
        {

            // initial setup | download measurement.xml document
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.Results = new List<Result>();
            /*

            FtpTransfer ftp = new FtpTransfer();
            XmlDocument doc = new XmlDocument();

            bool resultDownload1 = ftp.Download(hostName, fingerprint, localPath, ftpFile1);
            
            // Find the nodes LED Brightness
            doc.Load(localFile1);
            XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
            
            // change the value of the fixedmasks
            LEDbrightness = Convert.ToInt16(LED.InnerText);

            */
            
            // form setup
            this.Name = "Data Selection";
            this.Instructions = "LED Brightness during measurement\n\n" +
                                "Recommended Value : 50\n" +
                                "Current Value     : " + LEDbrightness.ToString() + "\n" +
                                "Set new value in the text box and Press Change\n" +
                                "and Press Next to continue to the next step\n";
            this.SupportingImage = string.Empty;
            
            this.OnTestUpdated(false);
            
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
               this.Instructions = string.Format(FMGTestStep04.InstructionText, LEDbrightness);
                 
            }).Start();
        }

        public override void Stop()
        {
            base.Stop();
            
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                
                string remark = "Selected Measurement : " + selectedData;
                this.Results.Add(new BooleanResult("Data Selection Test", remark, true));
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
                // update value from textBox
                string[] textValue = VarValue.Split(',');
                LEDbrightness = Convert.ToInt16(textValue[0]);

                // update LED brightness to measurement.xml
                XmlDocument doc = new XmlDocument();
                doc.Load(localFile1);
                XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LED.InnerText = LEDbrightness.ToString();
                doc.Save(localFile1);

                
                // upload measurement.xml to server
                FtpTransfer ftp = new FtpTransfer();
                bool resultUpload1 = ftp.Upload(hostName, fingerprint, localFile1, hostPath);
                string remark = "LED Brightness : " + LEDbrightness.ToString();
                this.Results.Add(new BooleanResult("LED Berightness Updated", remark, resultUpload1));

                // write result to debug console
                Console.WriteLine(LEDbrightness.ToString());
                this.OnTestUpdated(true);

            }

        }

        public static void directoryList()
        {
            try
            {
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = "172.17.5.108",
                    PortNumber = 990,
                    UserName = "Service Engineer",
                    Password = "192+SERV.HEMI",
                    FtpSecure = FtpSecure.Implicit,
                    TlsHostCertificateFingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f"
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
