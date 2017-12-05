using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep02 : TestStep
    {
        // Variables 
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 = "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath = "/Settings/system/testFolder/";

        [Obsolete]
        public SACTestStep02()
            : this(null)
        { }

        public SACTestStep02(TestManager testManager)
            : base(testManager)
        {
            this.Name = "2. Fixed Mask Configuration";
            this.Instructions = 
                                "Config fixed mask for Spacial Accuracy Test\n\n" +
                                "Update to:\n" +
                                "- MotorMatrixMask_accuracyL.png\n" +
                                "- MotorMatrixMask_accuracyR.png";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Update;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                remark = this.Name + " : SKIPPED";
                this.Results.Add(new BooleanResult(this.Name, remark, true));
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

                // download measurement.xml from the machine
                bool resultDownload1 = ftp.Download(localPath, ftpFile1);
                this.Results.Add(new BooleanResult("Download configuration", "Measurement.xml", resultDownload1));

                // update setting values
                doc.Load(localFile1);
                XmlNode maskLeft = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathLeft");
                XmlNode maskRight = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathRight");
                maskLeft.InnerText = @"D:\Settings\system\FixedMasks\MotorMatrixMask_accuracyL.png";
                maskRight.InnerText = @"D:\Settings\system\FixedMasks\MotorMatrixMask_accuracyR.png";
                doc.Save(localFile1);
                this.Results.Add(new BooleanResult("Setting Values", "Updated", true));

                // upload measurement.xml to the machine
                bool resultUpload1 = ftp.Upload(localFile1, hostPath);
                this.Results.Add(new BooleanResult("Upload configuration", "Measurement.xml", resultUpload1));

                // write result to debug console
                Console.WriteLine("Fixed Mask Configuration: DONE");
                this.OnTestUpdated(true);


            }
        }
    }
}
