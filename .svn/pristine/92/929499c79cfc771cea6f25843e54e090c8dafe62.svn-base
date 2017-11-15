using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.CalibrationTest
{
    public class CalibrationTestStep001 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalibrationTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public CalibrationTestStep001()
            : this (null)
        { }

        public CalibrationTestStep001(TestManager testManager)
            : base (testManager)
        {
            this.Name = "Aansluiten voor test";
            this.Instructions = "- Instructie tekst\n" +
                                "- Zie afbeelding\n" +
                                "- Druk op OK";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
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
