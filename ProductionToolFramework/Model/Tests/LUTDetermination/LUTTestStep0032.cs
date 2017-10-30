using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
{
    public class LUTTestStep0032 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep0032"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep0032()
            : this(null)
        { }

        public LUTTestStep0032(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Inspect mask location";
            this.Instructions = "Check if every green dots are in white dots\n" +
                                "- Press OK to continue\n" +
                                "- Press Retry to analyze measurement with different setting";
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
