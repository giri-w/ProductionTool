using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep002 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTestStep002()
            : this(null)
        { }

        public SelfTestStep002(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Selftest + indicator Leds";
            this.Instructions = "Start the Hemics application and see if the system succesfully goes through all the selftests. \n" +
                                "Write down in the work instructions if all Patient Indicator Leds work during the self test \n" +
                                "This is the LED bar above the logo \n Check if the frontplate with the Hemics logo is installed and write this down in the workinstruction";
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
                MessageBox.Show("WARNING: The lasers are now activated \n Make sure that holes in the slides cap are covered with tape. \n Make sure the white testcap is mounted under the optical box.");
                this.Results.Add(new BooleanResult("", "", true));
            }

            this.OnTestUpdated(true);
        }
    }
}
