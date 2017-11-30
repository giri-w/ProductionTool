using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    public class LPSTestStep01 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LPSTestStep01()
            : this(null)
        { }

        public LPSTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Machine Preparation";
            this.Instructions = 
                            "- Place POM plate on the glass plate\n" +
                            "- Place black paper delay target at left and right target\n" +
                            "- Place the flaps in the scanner for armholes\n" +
                            "- Ensure the slider is closed";
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
                this.Results.Add(new BooleanResult(this.Name, "Checked", true));
                this.OnTestUpdated(true);
            }

            
        }
    }
}
