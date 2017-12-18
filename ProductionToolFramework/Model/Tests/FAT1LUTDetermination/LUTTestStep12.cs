using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep12 : TestStep
    {
        [Obsolete]
        public LUTTestStep12()
            : this(null)
        { }

        string localFile1 		 = @"Python\figure\FAT1LUTDetermination\PyLeft-MotorMatrix-Low.xml";
		string localFile2 		 = @"Python\figure\FAT1LUTDetermination\PyRight-MotorMatrix-Low.xml";
        string hostPath			 = "/Settings/system/testFolder/CameraSettings/";
        

        public LUTTestStep12(TestManager testManager)
            : base(testManager)
        {
            this.Name 			 = "12. Motor Matrix Configuration";
            this.Instructions =
                                    "Motor Matrix configurations are ready\n" +
                                    "Press \"Update\" to upload files to the system\n";

            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions	 = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.Results		 = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                remark = "Motor Matrix Configuration: SKIPPED";
                this.Results.Add(new BooleanResult("Motor Matrix Configuration", remark, true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Update)
            {
                FtpTransfer ftp = new FtpTransfer();
                XmlDocument doc = new XmlDocument();

                // set address
                string xlocalFile1 = Path.GetFullPath(localFile1);
                string xlocalFile2 = Path.GetFullPath(localFile2);
                // upload measurement.xml to server
                bool resultUpload1 = ftp.Upload(xlocalFile1, hostPath);
                bool resultUpload2 = ftp.Upload(xlocalFile2, hostPath);

                // write result to log window
                remark = "Motor Matrix Configuration : UPLOADED";
                this.Results.Add(new BooleanResult("Motor Matrix Configuration", remark, resultUpload1 & resultUpload2));
                this.OnTestUpdated(true);
            }

        }
    }
}
