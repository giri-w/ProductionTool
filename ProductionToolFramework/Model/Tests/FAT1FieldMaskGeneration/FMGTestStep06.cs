using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HemicsFat;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep06 : TestStep
    {
        [Obsolete]
        public FMGTestStep06()
            : this(null)
        { }

        bool resultBool;
        private Double Threshold;
		private string SourceLocation 		 = string.Empty;
        private string testSetting			 = @"Setting\config.xml";
        private const string InstructionText =
                                                " Variable setting for analyzing the measurement \n\n" +
                                                " - Source Location\t: {0}\n" +
                                                " - Threshold\t\t: {1}\n\n" +
                                                "Press \"Process\" to start analyzing";

        public FMGTestStep06(TestManager testManager)
            : base(testManager)
        {
            // set initial value for setting from XML
            this.Name 				= "6. Data Processing : Right Hand";
            this.Instructions       = "Loading ...";
									  
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Analyze;
            this.VarOptions 		= EVarOptions.Threshold;
            this.Results 			= new List<Result>();
			
			// forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
            resultBool = false;
        }

        public override void Start()
        {
            this.Results.Clear();
            ChangeXml chg = new ChangeXml();
            new Task(() =>
            {
                SourceLocation 			= chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Source");
                Threshold 				= Convert.ToDouble(chg.ObtainElement(testSetting, "Test", "FAT1", "FMG", "Threshold"));
                this.VarValue           = Threshold.ToString();
                this.Instructions = string.Format(FMGTestStep06.InstructionText, SourceLocation, Threshold);

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
                if (testResult.Contains("PASS"))
                    resultBool =  true;
                else
                    resultBool = false;
            
                this.Results.Add(new BooleanResult("Data Processing", "Python Script executed", resultBool));
                this.Instructions = "Loading ...";
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Back to previous step
				this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Analyze)
            {
                ChangeXml chg = new ChangeXml();
                
                // retrieve variable value from text Box
                string[] textValue = VarValue.Split(',');
                Threshold = Convert.ToDouble(textValue[0]);
                chg.modifyElement(testSetting, "Test", "FAT1", "FMG", "Threshold", Threshold.ToString());

                // Python arguments
                Python py = new Python();
                String pythonLocation = @"Python/2. FieldMaskGeneration.py";
                string fullPath = Path.GetFullPath(pythonLocation);
                string[] pythonArgument = { fullPath, SourceLocation, Threshold.ToString(),"Right" }; //additional
                pyFullPath = fullPath;
                pyArgument = py.compArray(pythonArgument);
                
            }


        }
    }
}
