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
												" Locate GRID4 folder location in the computer \n" +
												" GRID4 Directory : {0}\n\n" +
												" Press Browse  to set Grid4  Measurement Location\n" +
												"\nWhen finished, press Next";


        public LUTTestStep05(TestManager testManager)
            : base(testManager)
        {
            this.Name 				= "GRID4 Location";
            this.Instructions 		= string.Empty;
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Browse;
            this.Results 			= new List<Result>();
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                Grid4Location = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
                this.Instructions = string.Format(LUTTestStep05.InstructionText, Grid4Location);
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
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous st4ep
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Browse)
            {
                testResult = info;

                if (File.Exists(testSetting)) // check if config.xml exist
                {
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
                    this.OnTestUpdated(true);
                }

                else
                {
                    MessageBox.Show("Setting.xml missing");
                }


            }
        }
    }
}
