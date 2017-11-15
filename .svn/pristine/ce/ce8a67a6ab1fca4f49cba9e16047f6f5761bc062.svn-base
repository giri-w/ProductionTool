using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep0021 : TestStep
    {
        
        private const string InstructionText =
                                "- Veranderende instructie met huidige waarde ter controle voor het doorgaan?\n" +
                                "- Huidige tijd: {0}.\n" +
                                "- Druk op OK";

        private bool keepUpdating;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep0021"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep0021()
            : this(null)
        { }

        public LUTTestStep0021(TestManager testManager)
            : base(testManager)
        {
            this.Name = "GRID4 Location";
            this.Instructions = " Locate GRID4 folder location in the computer \n" + 
                                " Press GRID4  to set Grid4  Measurement Location\n" +
                                "\nWhen finished, press OK";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK | EButtonOptions.GRID4;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

     

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Stop();

            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Result", "Just a random result", Math.IEEERemainder(DateTime.Now.Second, 2) > 0));
                
            }

            if (userAction == EButtonOptions.GRID4)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Result", info, true));
            }

            this.OnTestUpdated(true);

        }
    }
}
