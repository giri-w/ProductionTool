using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using HemicsFat;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep09 : TestStep
    {
        [Obsolete]
        public FMGTestStep09()
            : this(null)
        { }

        private double Threshold;
        private string SourceLocation 			= string.Empty;
        private string testSetting 				= @"Setting\config.xml";
		private const string InstructionText 	=
												   "Measurement Information\n" +
												   "- Source Location   : {0}\n" +
												   "- Threshold         : {1}\n" +
												   "Check if Left Mask location is correct\n\n" +
												   "- Press Next to continue\n" +
												   "- or press Back to change the measurement setting";
        public FMGTestStep09(TestManager testManager)
            : base(testManager)
        {
            // set initial value for setting from XML
			ChangeXml chg 						= new ChangeXml();
            SourceLocation  					= chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
            Threshold       					= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Threshold"));
			this.Name 							= "Analyze Left Mask";
            this.Instructions 					= 
												  "Measurement Information\n" +
												  "- Source Location   : "+SourceLocation+"\n" +
												  "- Threshold         : "+Threshold+"\n" +
												  "Check if Left Mask location is correct\n\n" +
												  "- Press Next to continue\n" +
												  "- or press Back to change the measurement setting";
			
            this.SupportingImage 				= @"Python\figure\FAT1FieldMaskGeneration\LeftFieldMask.png";
            this.ButtonOptions 					= EButtonOptions.Next | EButtonOptions.Back;
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
                SourceLocation 					= chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
                Threshold 						= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Threshold"));
                this.Instructions 				= string.Format(FMGTestStep09.InstructionText, SourceLocation, Threshold);
               
            }).Start();
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Mask has been set up properly", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
				this.OnTestCanceled(true);
            }

        }
      
    }
}
