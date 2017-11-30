using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    public class SACTestStep01 : TestStep
    {
        [Obsolete]
        public SACTestStep01()
            : this(null)
        { }

        public SACTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Machine Preparation";
            this.Instructions = "Remove targets and POM plates from the hand rests";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Machine Preparation", "Checked", true));
            }

            this.OnTestUpdated(true);
        }
    }
}
