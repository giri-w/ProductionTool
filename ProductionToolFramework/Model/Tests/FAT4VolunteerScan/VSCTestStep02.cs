using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using TestToolFramework.View;

namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    public class VSCTestStep02 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTestStep02()
            : this(null)
        { }

        public VSCTestStep02(TestManager testManager)
            : base(testManager)
        {
            this.Name = "2. Download measurements";
            this.Instructions = "Select the measurement result from the list\n" +
                                "Download all six measurements:\n" +
                                " - 2 x small\n" +
                                " - 2 x normal\n" +
                                " - 2 x big\n" +
                                "- Press \"Download\" to download the measurement to local computer";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next | EButtonOptions.Download | EButtonOptions.Back;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }
        
        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Download)
            {
                MeasurementForm browser = new MeasurementForm();
                Application.Run(browser);
            }
            
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
                this.OnTestUpdated(true);
            }

            if (userAction == EButtonOptions.Back)
            {
                // Check or do something (with the hardware?) for the test
                this.OnTestCanceled(true);
            }
        }
    }
}
