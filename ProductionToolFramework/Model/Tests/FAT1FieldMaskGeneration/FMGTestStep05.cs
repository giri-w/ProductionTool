using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml.Linq;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep05 : TestStep
    {
        [Obsolete]
        public FMGTestStep05()
            : this(null)
        { }

        private string testSetting 				= @"Setting\config.xml";
        private string SourceLocation 			= string.Empty;
        private const string InstructionText 	=
													"Locate measurement folder for Field Mask Generation test\n\n" +
													"Source directory : {0}\n\n" +
													"Press \"Browse\" to set new measurement location";


		public FMGTestStep05(TestManager testManager)
            : base(testManager)
        {
            this.Name 							= "5. Source location";
            this.Instructions 					= "Loading ...";

            this.SupportingImage				= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 					= EButtonOptions.Next|EButtonOptions.Back|EButtonOptions.Browse;
            this.Results 						= new List<Result>();

            // forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
        }

        public override void Start()
        {
            this.Results.Clear();
            new Task(() =>
            {
                ChangeXml chg = new ChangeXml();
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
                this.Instructions = string.Format(FMGTestStep05.InstructionText, SourceLocation);
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
                bool check = string.IsNullOrEmpty(SourceLocation);
                this.Results.Add(new BooleanResult("Source Location", SourceLocation, !check));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Browse)
            {
                ChangeXml chg = new ChangeXml();
				
                // save folder location 
                testResult = info;

                // update source setting in config.xml
                var xml = XDocument.Load(testSetting);
                chg.modifyElement(testSetting, "Test", "FAT1", "FMG", "Source", testResult);
                MessageBox.Show("Setting saved");

				// write result to windows log
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult("Source Location", testResult, !check));
                this.Instructions = "Loading ...";
                this.OnTestUpdated(true);

            }

        }
    }
}
