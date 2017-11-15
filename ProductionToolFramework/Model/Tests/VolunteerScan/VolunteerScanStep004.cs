using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.VolunteerScan
{
    public class VolunteerScanStep004 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VolunteerScanStep004()
            : this(null)
        { }

        public VolunteerScanStep004(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Workinstruction";
            this.Instructions = "You can also do a visual check of the results. The images are located in the folder shown at the results. \n" +
                                "Summarize the result in the systemvalidation document"; 
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
