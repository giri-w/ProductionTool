using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using HemicsFat;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep06 : TestStep
    {
        [Obsolete]
        public LUTTestStep06()
            : this(null)
        { }

        string leftFixedMask = @"D:\Settings\system\FixedMasks\MotorMatrixMask_LUT_gridL.png";
        string rightFixedMask = @"D:\Settings\system\FixedMasks\MotorMatrixMask_LUT_gridR.png";

        string localPath 		= @"C:\TestFolder\";
        string localFile1 		= @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1			= "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath			= "/Settings/system/testFolder/";


        public LUTTestStep06(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "6. Fixed Mask Setting Grid";
            this.Instructions       =   "Config fixed mask for Grid Measurement\n\n" +
                                        "Update to:\n" +
                                        "- MotorMatrixMask_LUT_gridL.png\n" +
                                        "- MotorMatrixMask_LUT_gridR.png\n\n";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.Results 			= new List<Result>();
			
			// forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            string remark = string.Empty;
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                remark = "Fixed Mask Setting : SKIPPED";
                this.Results.Add(new BooleanResult("Fixed Mask Setting", remark, true));
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

                bool resultDownload1 = ftp.Download(localPath, ftpFile1);

                // Find the nodes of fixed mask
                doc.Load(localFile1);
                XmlNode Left  = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathLeft");
                XmlNode Right = doc.SelectSingleNode("/MeasurementConfig/HandExtraction/FixedMaskMotorPathRight");

                // change the value of the fixedmasks
                Left.InnerText  = leftFixedMask;
                Right.InnerText = rightFixedMask;
                doc.Save(localFile1);

                // upload measurement.xml to server
                bool resultUpload1 = ftp.Upload(localFile1, hostPath);

                // write result to log window
                remark = "Fixed Mask Setting : UPDATED";
                this.Results.Add(new BooleanResult("Fixed Mask Setting", remark, resultUpload1));
                this.OnTestUpdated(true);
            }


        }
    }
}
