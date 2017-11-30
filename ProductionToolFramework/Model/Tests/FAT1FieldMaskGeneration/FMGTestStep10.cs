using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using WinSCP;
using System.Linq;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep10 : TestStep
    {
        
        string checkFiles 						= string.Empty;
        string localFile1 						= @"/Python/figure/left_mask.png";
        string localFile2 						= @"/Python/figure/right_mask.png";
        string localFile3 						= @"/Python/figure/left_mask_high.png";
        string localFile4 						= @"/Python/figure/right_mask_high.png";
				
        string hostPath 						= "/Settings/system/testFolder/CameraSettings";

        private const string InstructionText 	=
												  "Copy generated images to the machine\n" +
												  "- left_mask.png\n" +
												  "- right_mask.png\n" +
												  "- left_mask_high.png\n" +
												  "- right_mask_high.png\n\n" +
												  "INFO :\n{0}";
        
		[Obsolete]
        public FMGTestStep10()
            : this(null)
        { }


        public FMGTestStep10(TestManager testManager)
            : base(testManager)
        {

            if (File.Exists(localFile1) && File.Exists(localFile2) && File.Exists(localFile3) && File.Exists(localFile4))
                checkFiles = "All files available\nPress Next to start uploading";
            else
                checkFiles = "Some files are missing\nPress Back to regenerate the images";

            // form setup
            this.Name = "Update Field Mask";
            this.Instructions = "Copy generated images to the machine\n" +
                                "- left_mask.png\n" +
                                "- right_mask.png\n" +
                                "- left_mask_high.png\n" +
                                "- right_mask_high.png\n\n" +
                                "INFO :\n" + 
                                checkFiles;
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.Results = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
            
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
                if (File.Exists(localFile1) && File.Exists(localFile2) && File.Exists(localFile3) && File.Exists(localFile4))
                    checkFiles = "All files available\nPress Next to start uploading";
                else
                    checkFiles = "Some files are missing\nPress Back to regenerate the images";

                this.Instructions = string.Format(FMGTestStep10.InstructionText, checkFiles);
                 
            }).Start();
        }
		
		public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Analyze)
            {
                // upload generated images to server
                FtpTransfer ftp = new FtpTransfer();
                bool resultUpload1 = ftp.Upload(localFile1, hostPath);
                bool resultUpload2 = ftp.Upload(localFile2, hostPath);
                bool resultUpload3 = ftp.Upload(localFile3, hostPath);
                bool resultUpload4 = ftp.Upload(localFile4, hostPath);

                bool finalResult = resultUpload1 && resultUpload2 && resultUpload3 && resultUpload4;

                string remark;
                // continue to next step
                if (finalResult)
                    remark = "Updating field mask : SUCCESS";
                else
                    remark = "Updating field mask : FAILED";

                this.Results.Add(new BooleanResult("Data Selection Test", remark, finalResult));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                this.Results.Add(new BooleanResult(this.Name, "SKIPPED", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }


        }

    }
}
