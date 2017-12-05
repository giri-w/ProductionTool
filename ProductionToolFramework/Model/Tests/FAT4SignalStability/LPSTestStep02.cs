using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class LPSTestStep02 : TestStep
    {

        // Variables 
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 = "/Settings/system/Measurement_test.xml";
        string hostPath = "/Settings/system/";

        [Obsolete]
        public LPSTestStep02()
            : this(null)
        { }

        public LPSTestStep02(TestManager testManager)
            : base(testManager)
        {
            this.Name = "2. Initial Configuration";
            this.Instructions =
                            "Initial configuration during measurement\n" +
                            "- Number Of Iteration\n" +
                            "- Fixed Mask\n" +
                            "- Laser Power\n" +
                            "Press \"Update\" to start configuration and continue to the next step";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Update;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // should be empty in the real program
                string remark = this.Name + " : SKIPPED";
                this.Results.Add(new BooleanResult(this.Name,remark, true));
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
                this.Results.Add(new BooleanResult("Download configuration","Measurement.xml",resultDownload1));

                // update setting values
                doc.Load(localFile1);
                XmlNode iteration       = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/NumberOfIterations");
                XmlNode enableMask      = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/EnableFixedMask");
                XmlNode maskType        = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskType");
                XmlNode maskLeft        = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathLeft");
                XmlNode maskRight       = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathRight");
                XmlNode laserPower      = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/DefaultLaserPower");
                iteration.InnerText     = "0";
                enableMask.InnerText    = "true";
                maskType.InnerText      = "Engine";
                maskLeft.InnerText      = @"D:\Settings\system\FixedMasks\MotorMatrixMask_handL_max63.png";
                maskRight.InnerText     = @"D:\Settings\system\FixedMasks\MotorMatrixMask_handR_max63.png";
                laserPower.InnerText    = "0";
                doc.Save(localFile1);
                this.Results.Add(new BooleanResult("Setting Values", "Updated", true));

                // upload measurement.xml to the machine
                bool resultUpload1 = ftp.Upload(localFile1, hostPath);
                this.Results.Add(new BooleanResult("Upload configuration", "Measurement.xml", resultUpload1));

                // write result to debug console
                MessageBox.Show("Configuration Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Initial Configuration: DONE");
                this.OnTestUpdated(true);


            }

        }
    }
}
