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
        string localFile1 						= @"Python/figure/FAT1FieldMaskGeneration/left_mask.png";
        string localFile2 						= @"Python/figure/FAT1FieldMaskGeneration/right_mask.png";
        string localFile3 						= @"Python/figure/FAT1FieldMaskGeneration/left_mask_high.png";
        string localFile4 						= @"Python/figure/FAT1FieldMaskGeneration/right_mask_high.png";
				
        string hostPath 						= "/Settings/system/testFolder/CameraSettings";

        private const string InstructionText 	=
												  "Copy Field Mask images to the machine\n\n" +
												  "INFO :\n{0}";
        
		[Obsolete]
        public FMGTestStep10()
            : this(null)
        { }


        public FMGTestStep10(TestManager testManager)
            : base(testManager)
        {

            this.Name = "10. Update Field Mask";
            this.Instructions = "Loading ...";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
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
                    checkFiles = "All files are available\nPress \"Update\" to start uploading";
                else
                    checkFiles = "Some files are missing\nPress \"Back\" to regenerate the images";

                this.Instructions = string.Format(FMGTestStep10.InstructionText, checkFiles);
                this.OnTestUpdated(false);
                System.Threading.Thread.Sleep(10);

            }).Start();
        }
		
		public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Update)
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
                this.Instructions = "Loading ...";
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                this.Results.Add(new BooleanResult(this.Name, "SKIPPED", true));
                this.Instructions = "Loading ...";
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.Instructions = "Loading ...";
                this.OnTestCanceled(true);
            }


        }

    }
}
