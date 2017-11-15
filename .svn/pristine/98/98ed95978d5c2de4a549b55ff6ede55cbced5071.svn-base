using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep004 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep004"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep004()
            : this(null)
        { }

        public LUTTestStep004(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Generate XML";
            this.Instructions = "- Instructie tekst\n" +
                                "- Zie afbeelding\n" +
                                "- Druk op OK";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK | EButtonOptions.No;
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
