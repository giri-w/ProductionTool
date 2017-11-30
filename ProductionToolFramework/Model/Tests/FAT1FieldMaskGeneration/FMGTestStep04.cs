using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;
using WinSCP;
using System.Linq;
using TestToolFramework.View;

namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    public class FMGTestStep04 : TestStep
    {
        [Obsolete]
        public FMGTestStep04()
            : this(null)
        { }


        public FMGTestStep04(TestManager testManager)
            : base(testManager)
        {
            // form setup
            this.Name				= "Data Selection";
            this.Instructions 		= 
										"- Select the measurement result from the list\n" +
										"- Press DOWNLOAD to download the measurement to test folder";
            this.SupportingImage 	= @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions 		= EButtonOptions.Next | EButtonOptions.Back | EButtonOptions.Download;
            this.Results 			= new List<Result>();
            
            // forward and backward handler
            this.OnTestUpdated(false);
            
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // continue to next step
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // back to previous step
                this.OnTestCanceled(true);
            }

            if (userAction == EButtonOptions.Download)
            {
                MeasurementForm browser = new MeasurementForm();
                Application.Run(browser);
            }

        }
        
    }
}
