using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demcon.ProductionTool.Model
{
    class ReportGeneratorTestSequence : TestSequence
    {
        public ReportGeneratorTestSequence(TestManager testManager)
            : base(testManager)
        {
        }

        public string Save(string dataRootPath)
        {
            return this.testManager.GenerateReport(this.SerialNumber, this.Dwo);
        }
    }
}
