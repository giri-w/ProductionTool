using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

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
													"Locate measurement folder in the computer\n" +
													"as a source for Field Mask Generation\n" +
													"Source directory : {0}\n" +
													"Press Browse to set the measurement location";

		public FMGTestStep05(TestManager testManager)
            : base(testManager)
        {
            ChangeXml chg						= new ChangeXml();
            SourceLocation						= chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
            this.Name 							= "Source location";
            this.Instructions 					= 
												  "Locate measurement folder in the computer\n" +
												  "as a Source for Field Mask Generation\n" +
												  "Source directory : " + SourceLocation + "\n" +
												  "Press Browse to set the measurement location";
                                 
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
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
                this.Instructions = string.Format(FMGTestStep05.InstructionText, SourceLocation);
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
				
				// check if config.xml exist
                if (File.Exists(testSetting)) 
                {

                    var xml = XDocument.Load(testSetting);
                    bool isLUTConfigExist = xml.Element("Test").Elements("FAT1").Elements("FMG").Any();

                    if (isLUTConfigExist)
                    {
                        chg.modifyElement(testSetting, "Test", "FAT1", "FMG", "Source", testResult);
                        MessageBox.Show("Setting saved");
                    }
                    else
                        MessageBox.Show("Ada yang salah dengan XML");


                }
				
				// write result to windows log
                bool check = string.IsNullOrEmpty(testResult);
                this.Results.Add(new BooleanResult("Source Location", testResult, !check));
                this.OnTestUpdated(true);

            }

        }
    }
}
