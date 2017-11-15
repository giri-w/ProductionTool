using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep0023 : TestStep
    {
        
        private const string InstructionText =
                                "- Veranderende instructie met huidige waarde ter controle voor het doorgaan?\n" +
                                "- Huidige tijd: {0}.\n" +
                                "- Druk op OK";

        private bool keepUpdating;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep0023"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep0023()
            : this(null)
        { }

        public LUTTestStep0023(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Variable Setting";
            this.Instructions = " Setting up variable that are used in measurement \n"+
                                " - var Intensity   -> Default : 0.4\n" +
                                " - var Mass        -> Default : 500000\n" +
                                " - var Distance  -> Default : 50\n" +
                                "\nWhen finished, press OK";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.VarOptions = EVarOptions.Intensity | EVarOptions.Mass | EVarOptions.Distance;
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

           
            this.OnTestUpdated(true);



        }
    }
}
