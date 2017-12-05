using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using HemicsFat;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep05 : TestStep
    {
        [Obsolete]
        public LUTTestStep05()
            : this(null)
        { }

        private string testSetting = @"Setting\config.xml";
        private string Grid4Location = string.Empty;

        private const string InstructionText =
                                                " Locate GRID4 measurement folder for LUT test\n\n" +
                                                " GRID4 Directory\t: {0}\n\n" +
                                                " Press \"Browse\" to set new Grid4 Measurement Location\n";

        public LUTTestStep05(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "5. GRID4 Location";
            this.Instructions 		= "Loading...";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Browse;
            this.Results 			= new List<Result>();

            // forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            
            new Task(() =>
            {
                // obtain GRID 4 location
                ChangeXml chg = new ChangeXml();
                Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");

                this.Instructions = string.Format(LUTTestStep05.InstructionText, Grid4Location);
                this.OnTestUpdated(false);
                System.Threading.Thread.Sleep(10);
            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                bool check = string.IsNullOrEmpty(Grid4Location);
                this.Results.Add(new BooleanResult("GRID4 Location", Grid4Location, !check));
                this.Instructions = "Loading...";
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Browse)
            {
                testResult = info;

                XmlDocument doc = new XmlDocument();

                // Find the nodes of fixed mask
                doc.Load(testSetting);
                XmlNode Grid4Dir = doc.SelectSingleNode("/Test/FAT1/LUT/GRID4");

                // change the value of the fixedmasks
                Grid4Dir.InnerText = testResult;
                doc.Save(testSetting);
                MessageBox.Show("Setting saved");

                // write result to log window
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult("GRID4 Location", testResult, !check));
                this.Instructions = "Loading...";
                this.OnTestUpdated(true);
            }

        }
    }
}
