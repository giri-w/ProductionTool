using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HemicsFat;
using System.Xml;
using Demcon.ProductionTool.View;
using System.Globalization;
using System.Windows.Forms;

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
                                "LED Brightness setting for measurement\n\n" +
                                "Current Value\t\t: {0}\n" + 
                                "Recommended Value\t: 50\n\n" +
                                "Press \"Update\" to set new value for LED Brightness";
                                

        public FMGTestStep02(TestManager testManager)
            : base(testManager)
        {
            // form setup
            this.Name 				= "2. LED Brightness";
            this.Instructions 		= "Loading ...";
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
                Console.WriteLine("Connect FTP to obtain LED Brightness");

                if (ftp.checkConnection())
                {
                    // download measurement.xml
                    bool resultDownload1 = ftp.Download(localPath, ftpFile1);

                    // update LED brightness to measurement.xml
                    doc.Load(localFile1);
                    XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                    LEDbrightness = (LED.InnerText);
                    doc.Save(localFile1);

                    this.Instructions = string.Format(FMGTestStep02.InstructionText, LEDbrightness);
                }
                else
                {
                    this.Instructions =  "Machine is not connected\n" +
                                         "Please check if internet connection is available";
                }

                
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
                
               if (InputDialog.GetInput("LED Brightness", "Please input new LED Brightness setting\r\n(LED Brightness is in numeric format)", LEDbrightness, out LEDbrightness) && !string.IsNullOrWhiteSpace(LEDbrightness))
               {

                    float value;
                    while (!(float.TryParse(LEDbrightness, NumberStyles.Float, CultureInfo.InvariantCulture, out value)))
                    {
                        bool success = InputDialog.GetInput("LED Brightness", "LED Brightness should be a numeric value", LEDbrightness, out LEDbrightness) && !string.IsNullOrWhiteSpace(LEDbrightness);
                    }
                   
                        XmlDocument doc = new XmlDocument();
                        // update LED brightness to measurement.xml
                        doc.Load(localFile1);
                        XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                        LED.InnerText = value.ToString();
                        doc.Save(localFile1);

                        // upload measurement.xml to server
                        FtpTransfer ftp = new FtpTransfer();
                        bool resultUpload1 = ftp.Upload(localFile1, hostPath);
                        string remark = "LED Brightness : " + value.ToString();
                        this.Results.Add(new BooleanResult("LED Berightness Updated", remark, resultUpload1));

                        // write result to debug console
                        Console.WriteLine(value.ToString());
                        this.Instructions = "Loading ...";
                        this.OnTestUpdated(true);
                    
               }
               
            }

        }
    }
}
