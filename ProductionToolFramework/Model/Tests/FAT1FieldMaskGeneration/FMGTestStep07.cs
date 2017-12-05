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
    public class FMGTestStep07 : TestStep
    {
        [Obsolete]
        public FMGTestStep07()
            : this(null)
        { }

        private string testSetting 			 = @"Setting\config.xml";


        public FMGTestStep07(TestManager testManager)
            : base(testManager)
        {

            this.Name                   = "7. Analyze Right Mask";
            this.Instructions 			= 
											"Check if Right Mask location is correct\n\n" +
											"- Press Next to check the Left Hand mask\n" +
											"- Press Back to change the measurement setting";
            this.SupportingImage 		= @"Python\figure\FAT1FieldMaskGeneration\RightFieldMask.png";
            this.ButtonOptions 			= EButtonOptions.Next | EButtonOptions.Back;
            this.Results 				= new List<Result>();
			
            // forward and backward handler
            this.OnTestUpdated(false);
            this.OnTestCanceled(false);
  
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Right Mask has been set up properly", true));
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
