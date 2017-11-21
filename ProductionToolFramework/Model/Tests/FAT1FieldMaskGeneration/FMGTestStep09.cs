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
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep09"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTestStep09()
            : this(null)
        { }

        private const string InstructionText =
                                "Measurement Information\n" +
                                "- Source Location   : {0}\n" +
                                "- Threshold         : {1}\n\n" +
                                "Check if Left Mask location is correct\n\n" +
                                "- Press Next to continue\n" +
                                "- or press Back to change the measurement setting";

        
        private string SourceLocation = string.Empty;
        private double Threshold;
        private string testSetting = @"Setting\config.xml";


        public FMGTestStep09(TestManager testManager)
            : base(testManager)
        {
            ChangeXml chg = new ChangeXml();
            this.Name = "Analyze Left Mask";
            this.Instructions = string.Empty;
            this.SupportingImage = @"Python\figure\FAT1FieldMaskGeneration\LeftFieldMask.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);

            // set initial value for setting from XML
            SourceLocation  = chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
            Threshold       = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Threshold"));
            
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                SourceLocation = chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
                Threshold = Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Threshold"));
                this.Instructions = string.Format(FMGTestStep09.InstructionText, SourceLocation, Threshold);
               
            }).Start();
        }

        public override void Stop()
        {
            base.Stop();
            
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Stop();
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Analyze the measurement", "Mask has been set up properly", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }


        }

      
    }

    

}
