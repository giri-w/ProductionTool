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
        
        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Measurement_test.xml";
        string ftpFile1 = "/Settings/system/testFolder/Measurement_test.xml";
        string hostPath = "/Settings/system/testFolder/";
       
        private const string InstructionText =
                                "LED Brightness during measurement\n\n" +
                                "Current Value     : {0} \n" +
                                "Recommended Value : 150\n" +
                                "Press Next to reset LED Brightneess\n";


        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep11"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTestStep11()
            : this(null)
        { }


        public FMGTestStep11(TestManager testManager)
            : base(testManager)
        {

            FtpTransfer ftp = new FtpTransfer();
            XmlDocument doc = new XmlDocument();
            bool resultDownload1 = ftp.Download(hostName, fingerprint, localPath, ftpFile1);
            
            // Find the nodes LED Brightness
            doc.Load(localFile1);
            XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
            
            // change the value of the fixedmasks
            LEDbrightness = Convert.ToInt16(LED.InnerText);
            
            // form setup
            this.Name = "Reset LED Brightness";
            this.Instructions = "LED Brightness during measurement\n\n" +
                                "Current Value     : " + LEDbrightness.ToString() + "\n" +
                                "Recommended Value : 150\n" +
                                "Press Next to reset LED Brightneess\n";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
            this.Results = new List<Result>();
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

        public override void Stop()
        {
            base.Stop();
            
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                // update value from textBox
                LEDbrightness = 150;

                // update LED brightness to measurement.xml
                XmlDocument doc = new XmlDocument();
                doc.Load(localFile1);
                XmlNode LED = doc.SelectSingleNode("/MeasurementConfig/CompartmentLedBrightness");
                LED.InnerText = LEDbrightness.ToString();
                doc.Save(localFile1);


                // upload measurement.xml to server
                FtpTransfer ftp = new FtpTransfer();
                bool resultUpload1 = ftp.Upload(hostName, fingerprint, localFile1, hostPath);
                
                // write result to debug console
                Console.WriteLine(LEDbrightness.ToString());
                this.OnTestUpdated(true);


                // update status
                string remark = "LED Brightness : " + LEDbrightness;
                this.Results.Add(new BooleanResult("Reset LED Brightness Test", remark, resultUpload1));
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
