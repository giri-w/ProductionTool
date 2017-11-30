using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using WinSCP;
using System.Linq;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep11 : TestStep
    {
        private int LEDbrightness;
        string localPath 						= @"C:\TestFolder\";
        string localFile1 						= @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 						= "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath 						= "/Settings/system/testFolder/";
       
        private const string InstructionText	=
												  "LED Brightness during measurement\n\n" +
												  "Current Value     : {0} \n" +
												  "Recommended Value : 150\n" +
												  "Press Next to reset LED Brightneess\n";
		
		[Obsolete]
        public FMGTestStep11()
            : this(null)
        { }

		public FMGTestStep11(TestManager testManager)
            : base(testManager)
        {

            FtpTransfer ftp 		= new FtpTransfer();
            XmlDocument doc 		= new XmlDocument();
            bool resultDownload1 	= ftp.Download(localPath, ftpFile1);
            
            // Find the nodes LED Brightness
            doc.Load(localFile1);
            XmlNode LED 			= doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
            LEDbrightness 			= Convert.ToInt16(LED.InnerText);
            doc.Save(localFile1);
            
            // form setup
            this.Name 				= "Reset LED Brightness";
            this.Instructions 		= 
									  "LED Brightness during measurement\n\n" +
									  "Current Value     : " + LEDbrightness.ToString() + "\n" +
									  "Recommended Value : 150\n" +
									  "Press Next to reset LED Brightneess\n";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.Results 			= new List<Result>();
			
			// forward and backward handler
            this.OnTestUpdated(false);

        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
               this.Instructions = string.Format(FMGTestStep11.InstructionText, LEDbrightness);
                 
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
			
            if (userAction == EButtonOptions.Analyze)
            {
                XmlDocument doc = new XmlDocument();
				// update value from textBox
                LEDbrightness = 150;

                // update LED brightness to measurement.xml
                
                doc.Load(localFile1);
                XmlNode LED 		= doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LED.InnerText 		= LEDbrightness.ToString();
                doc.Save(localFile1);

                // upload measurement.xml to server
                FtpTransfer ftp 	= new FtpTransfer();
                bool resultUpload1 	= ftp.Upload(localFile1, hostPath);
                
                // write result to debug console
                Console.WriteLine(LEDbrightness.ToString());
                this.OnTestUpdated(true);

                // update status
                string remark 		= "LED Brightness : " + LEDbrightness;
                this.Results.Add(new BooleanResult("Reset LED Brightness Test", remark, resultUpload1));
                this.OnTestUpdated(true);
            }

        }
        
    }
}
