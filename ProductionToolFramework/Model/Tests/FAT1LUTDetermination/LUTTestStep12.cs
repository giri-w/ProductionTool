using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep12 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep12"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep12()
            : this(null)
        { }

        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string localFile2 = @"C:\TestFolder\Measurement_test.xml";
        string hostPath = "/Settings/system/CameraSettings/";

        private const string InstructionText =
                                "Motor Matrix configurations are ready\n" +
                                "Press Update to upload files to the system\n:" +
                                "Press Next to continue to the next step\n";

        public LUTTestStep12(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Motor Matrix Configuration";
            this.Instructions = string.Empty;
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
                this.Instructions = string.Format(LUTTestStep12.InstructionText);

            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                remark = "Motor Matrix Configuration: SKIPPED";
                this.Results.Add(new BooleanResult("Motor Matrix Configuration", remark, true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Update)
            {
                FtpTransfer ftp = new FtpTransfer();
                XmlDocument doc = new XmlDocument();

                // upload measurement.xml to server
                bool resultUpload1 = ftp.Upload(hostName, fingerprint, localFile1, hostPath);
                bool resultUpload2 = ftp.Upload(hostName, fingerprint, localFile2, hostPath);

                // write result to log window
                remark = "Motor Matrix Configuration : UPLOADED";
                this.Results.Add(new BooleanResult("Motor Matrix Configuration", remark, resultUpload1 & resultUpload2));
                this.OnTestUpdated(true);
            }

        }
    }
}
