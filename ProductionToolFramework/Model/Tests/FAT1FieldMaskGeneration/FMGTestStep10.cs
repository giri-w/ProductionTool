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
        
        string checkFiles = string.Empty;
        string localFile1 = @"/Python/figure/left_mask.png";
        string localFile2 = @"/Python/figure/right_mask.png";
        string localFile3 = @"/Python/figure/left_mask_high.png";
        string localFile4 = @"/Python/figure/right_mask_high.png";

        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string hostPath = "/Settings/system/testFolder/CameraSettings";

        private const string InstructionText =
                                "Copy generated images to the machine\n" +
                                "- left_mask.png\n" +
                                "- right_mask.png\n" +
                                "- left_mask_high.png\n" +
                                "- right_mask_high.png\n\n" +
                                "INFO :\n{0}";
                                


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep10"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
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

            // initial setup | download measurement.xml document
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
            this.Results = new List<Result>();

            // form setup
            this.Name = "Update Field Mask";
            this.Instructions = "Copy generated images to the machine\n" +
                                "- left_mask.png\n" +
                                "- right_mask.png\n" +
                                "- left_mask_high.png\n" +
                                "- right_mask_high.png\n\n" +
                                "INFO :\n" + 
                                checkFiles;
            this.SupportingImage = string.Empty;
            
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

        public override void Stop()
        {
            base.Stop();
            
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // upload generated images to server
                FtpTransfer ftp = new FtpTransfer();
                bool resultUpload1 = ftp.Upload(hostName, fingerprint, localFile1, hostPath);
                bool resultUpload2 = ftp.Upload(hostName, fingerprint, localFile2, hostPath);
                bool resultUpload3 = ftp.Upload(hostName, fingerprint, localFile3, hostPath);
                bool resultUpload4 = ftp.Upload(hostName, fingerprint, localFile4, hostPath);

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

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }


        }


     



    }
}
