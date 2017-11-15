using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;


namespace Demcon.ProductionTool.Model.Tests.VolunteerScan
{
    public class VolunteerScanStep002 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VolunteerScanStep002()
            : this(null)
        { }

        public VolunteerScanStep002(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Analyze measurements";
            this.Instructions = "If any measurements were done wrong first remove them from the system. \n" +
                                "Enter the measurement names separated by a comma (,) and press 'Process'. \n Now press 'Ok' to download the files. This may take some time.";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK|EButtonOptions.Analyze;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            this.VarOptions = EVarOptions.Measurements;
            
        }
       
        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"\TestFolder\";
       

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Analyze)
            {
                FtpTransfer ftpTransfer = new FtpTransfer();
                MessageBox.Show(VarValue);
                string[] textValue = VarValue.Split(',');
                for (int i = 0; i < 2; i++)
                {
                    textValue[i] = textValue[i].Replace(" ", string.Empty);
                }

                string hostPath1 = "/Measurements/297/"+textValue[0];
                string hostPath2 = "/Measurements/297/"+textValue[1];
                //string hostPath3 = "/Measurements/297/"+textValue[2];
                //string hostPath4 = "/Measurements/297/"+textValue[3];
                //string hostPath5 = "/Measurements/297/"+textValue[4];
                //string hostPath6 = "/Measurements/297/"+textValue[5];
                bool result1 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath1);
                this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath1, result1));
                bool result2 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath2);
                this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath2, result2));
                //bool result3 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath3);
                //this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath3, result3));
                //bool result4 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath4);
                //this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath4, result4));
                //bool result5 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath5);
                //this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath5, result5));
                //bool result6 = ftpTransfer.DownloadLatest(hostName, fingerprint, localPath, hostPath6);
                //this.Results.Add(new BooleanResult("File transfer", "Downloaded:" + hostPath6, result6));
            }

            this.OnTestUpdated(true);
        }
    }
}
