using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;
using System.Xml;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep09 : TestStep
    {
        [Obsolete]
        public LUTTestStep09()
            : this(null)
        { }

        private string testSetting 			 = @"Setting\config.xml";
        private string GridLocation 		 = string.Empty;

        private const string InstructionText =
												" Locate GRID folder location in the computer \n" +
												" GRID Directory : {0}\n\n" +
												" Press Browse  to set Grid  Measurement Location\n" +
												"\nWhen finished, press Next";

        public LUTTestStep09(TestManager testManager)
            : base(testManager)
        {
            this.Name 						 = "GRID Location";
            this.Instructions				 = string.Empty;
            this.SupportingImage			 = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions				 = EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Browse;
            this.Results					 = new List<Result>();
			// forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                GridLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");
                this.Instructions = string.Format(LUTTestStep09.InstructionText, GridLocation);
            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                bool check = string.IsNullOrEmpty(GridLocation);
                this.Results.Add(new BooleanResult("GRID Location", GridLocation, !check));
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

                if (File.Exists(testSetting)) // check if config.xml exist
                {
                    XmlDocument doc = new XmlDocument();

                    // Find the nodes of fixed mask
                    doc.Load(testSetting);
                    XmlNode GridDir = doc.SelectSingleNode("/Test/FAT1/LUT/GRID");

                    // change the value of the fixedmasks
                    GridDir.InnerText = testResult;
                    doc.Save(testSetting);
                    MessageBox.Show("Setting saved");

                    // write result to log window
                    bool check = string.IsNullOrEmpty(testResult);
                    this.Results.Add(new BooleanResult("GRID Location", testResult, !check));
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
