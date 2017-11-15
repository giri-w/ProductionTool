using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.VolunteerScan
{
    public class VolunteerScanStep001 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VolunteerScanStep001()
            : this(null)
        { }

        public VolunteerScanStep001(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Choose volunteers";
            this.Instructions = "Choose 3 volunteers with different hand sizes, preferably 1x small, 1x medium, 1x large\n" +
                                "Perform 2 measurements with each volunteer and write down the name of the measurements";                                
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
                this.Results.Add(new BooleanResult("", "", true));
            }

            this.OnTestUpdated(true);
        }
    }
}
