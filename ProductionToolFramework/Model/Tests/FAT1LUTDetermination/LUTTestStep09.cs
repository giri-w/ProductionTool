using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
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
                                                " Locate GRID measurement folder for LUT test\n\n" +
                                                " GRID Directory\t: {0}\n\n" +
                                                " Press \"Browse\" to set new Grid Measurement Location";

        public LUTTestStep09(TestManager testManager)
            : base(testManager)
        {
            this.Name 						 = "9. GRID Location";
            this.Instructions				 = "Loading ...";
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
            new Task(() =>
            {
                // obtain GRID location
                ChangeXml chg = new ChangeXml();
                GridLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "LUT", "GRID4");

                this.Instructions = string.Format(LUTTestStep09.InstructionText, GridLocation);
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
                bool check = string.IsNullOrEmpty(GridLocation);
                this.Results.Add(new BooleanResult("GRID Location", GridLocation, !check));
                this.Instructions = "Loading ...";
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

                // Update Grid location
                XmlDocument doc = new XmlDocument();

                // Find the nodes of fixed mask
                doc.Load(testSetting);
                XmlNode GridDir = doc.SelectSingleNode("/Test/FAT1/LUT/GRID");

                // Change the value of the fixedmasks
                GridDir.InnerText = testResult;
                doc.Save(testSetting);
                MessageBox.Show("Setting saved");

                // Write result to log window
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult("GRID Location", testResult, !check));
                this.Instructions = "Loading ...";
                this.OnTestUpdated(true);
               
            }

        }
    }
}
