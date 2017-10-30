using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Model.Tests.CalibrationTest;

namespace Demcon.ProductionTool.Model
{
    [Serializable]
    public class CalibrationTestSequence : TestSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalibrationTestSequence"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public CalibrationTestSequence()
            : this(null)
        { }

        public CalibrationTestSequence(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Periodieke kalibratie van productietooling";
            this.Description = "Periodieke kalibratie van productietooling";
            this.Tests = new List<Test>();
            this.Tests.Add(new CalibrationTest(this.testManager));
            this.SetToFirstTest();
        }

        protected override string CheckAdditionalInfo1(string additionalInfo)
        {
            string retVal = string.Empty;

            // No input expected by default, implement check to verify the input.
            if (string.IsNullOrWhiteSpace(additionalInfo))
            {
                retVal = this.AddtionalInformationRequestText1 + " mag niet leeg zijn\n";
            }

            return retVal;
        }
    }
}
