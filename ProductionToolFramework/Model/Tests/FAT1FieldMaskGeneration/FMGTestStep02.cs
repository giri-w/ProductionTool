using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using Demcon.ProductionTool.View;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep02 : TestStep
    {
        
		[Obsolete]
        public FMGTestStep02()
            : this(null)
        { }

		private string LEDbrightness;

        string localPath 		= @"C:\TestFolder\";
        string localFile1 		= @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 		= "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath 		= "/Settings/system/testFolder/";
       
        private const string InstructionText =
                                "LED Brightness during measurement\n\n" +
                                "Recommended Value : 50\n" +
                                "Current Value     : {0}\n" +
                                "Set new value in the text box and Press Change\n" +
                                "and Press Next to continue to the next step\n";

        public FMGTestStep02(TestManager testManager)
            : base(testManager)
        {
            // form setup
            this.Name 				= "LED Brightness";
            this.Instructions 		= string.Empty;
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            
            this.Results			= new List<Result>();
            this.OnTestUpdated(false);
            
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {

                FtpTransfer ftp 		= new FtpTransfer();
                XmlDocument doc 		= new XmlDocument();
				
				// download measurement.xml
                bool resultDownload1 	= ftp.Download(localPath, ftpFile1);
				
                // update LED brightness to measurement.xml
                doc.Load(localFile1);
                XmlNode LED 			= doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LEDbrightness 			= (LED.InnerText);
				doc.Save(localFile1);
                
				this.Instructions 		= string.Format(FMGTestStep02.InstructionText,LEDbrightness);
				this.OnTestUpdated(false);
                System.Threading.Thread.Sleep(10);

            }).Start();
        }




        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
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

            if (userAction == EButtonOptions.Update)
            {
                
               if (InputDialog.GetInput("LED Brightness", "Please input new LED Brightness", LEDbrightness, out LEDbrightness) && !string.IsNullOrWhiteSpace(LEDbrightness))
               {
                   XmlDocument doc = new XmlDocument();
                   // update LED brightness to measurement.xml
                   doc.Load(localFile1);
                   XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                   LED.InnerText = LEDbrightness.ToString();
                   doc.Save(localFile1);

                   // upload measurement.xml to server
                   FtpTransfer ftp = new FtpTransfer();
                   bool resultUpload1 = ftp.Upload(localFile1, hostPath);
                   string remark = "LED Brightness : " + LEDbrightness.ToString();
                   this.Results.Add(new BooleanResult("LED Berightness Updated", remark, resultUpload1));

                   // write result to debug console
                   Console.WriteLine(LEDbrightness.ToString());
                   this.OnTestUpdated(true);
               }
               
            }

        }
    }
}
