using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep02 : TestStep
    {
        

        private int LEDbrightness;

        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 = "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath = "/Settings/system/testFolder/";
       
        private const string InstructionText =
                                "LED Brightness during measurement\n\n" +
                                "Recommended Value : 50\n" +
                                "Current Value     : {0}\n" +
                                "Set new value in the text box and Press Change\n" +
                                "and Press Next to continue to the next step\n";


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep02"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTestStep02()
            : this(null)
        { }


        public FMGTestStep02(TestManager testManager)
            : base(testManager)
        {
            // form setup
            this.Name = "LED Brightness";
            this.Instructions = string.Empty;
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Update;
            this.VarOptions = EVarOptions.LED;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {

                FtpTransfer ftp = new FtpTransfer();
                XmlDocument doc = new XmlDocument();
                bool resultDownload1 = ftp.Download(hostName, fingerprint, localPath, ftpFile1);
                // update LED brightness to measurement.xml
                doc.Load(localFile1);
                XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LEDbrightness = Convert.ToInt16(LED.InnerText);
                this.Instructions = string.Format(FMGTestStep02.InstructionText,LEDbrightness);

            }).Start();
        }




        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                
                string remark = "LED Brightness : " + LEDbrightness.ToString();
                this.Results.Add(new BooleanResult("LED Berightness Test", remark, true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Update)
            {
                // update value from textBox
                string[] textValue = VarValue.Split(',');
                LEDbrightness = Convert.ToInt16(textValue[0]);

                // update LED brightness to measurement.xml
                XmlDocument doc = new XmlDocument();
                doc.Load(localFile1);
                XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LED.InnerText = LEDbrightness.ToString();
                doc.Save(localFile1);

                
                // upload measurement.xml to server
                FtpTransfer ftp = new FtpTransfer();
                bool resultUpload1 = ftp.Upload(hostName, fingerprint, localFile1, hostPath);
                string remark = "LED Brightness : " + LEDbrightness.ToString();
                this.Results.Add(new BooleanResult("LED Berightness Updated", remark, resultUpload1));

                // write result to debug console
                Console.WriteLine(LEDbrightness.ToString());
                this.OnTestUpdated(true);

            }

        }
    }
}
