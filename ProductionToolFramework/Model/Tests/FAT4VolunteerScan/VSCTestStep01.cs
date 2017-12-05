using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    public class VSCTestStep01 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTestStep01()
            : this(null)
        { }

        public VSCTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name = "1. Choose volunteers";
            this.Instructions = "- Choose 3 volunteers with different hand sizes\n" +
                                "- Preferably 1x small, 1x medium, 1x large\n" +
                                "- Perform 2 measurements with each volunteer\n" +
                                "- Write down the name of the measurements";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.Next;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                this.Results.Add(new BooleanResult("", "", true));
                this.OnTestUpdated(true);
            }

            
        }
    }
}
