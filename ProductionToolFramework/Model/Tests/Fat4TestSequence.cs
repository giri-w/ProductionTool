using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Model.Tests.VolunteerScan;
using Demcon.ProductionTool.Model.Tests.FAT4SignalStability;
using Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy;
using Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan;

namespace Demcon.ProductionTool.Model
{
    [Serializable]
    public class Fat4TestSequence : TestSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestSequence"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public Fat4TestSequence()
            : this(null)
        { }

        public Fat4TestSequence(TestManager testManager)
            : base(testManager)
        {
            this.Name = "FAT 4";
            this.Description = "Work instructions for acceptance of the system";
            this.AddtionalInformationRequestText1 = "PCA serie nummer";
            this.AddtionalInformationRequestText2 = "W100 batch nummer";
            this.Tests = new List<Test>();
            this.Tests.Add(new LPSTest(this.testManager));
            this.Tests.Add(new SACTest(this.testManager));
            this.Tests.Add(new VSCTest(this.testManager));
            this.Tests.Add(new VolunteerScan(this.testManager));
            
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

        protected override string CheckAdditionalInfo2(string additionalInfo)
        {
            string retVal = string.Empty;

            // No input expected by default, implement check to verify the input.
            if (string.IsNullOrWhiteSpace(additionalInfo))
            {
                retVal = this.AddtionalInformationRequestText2 + " mag niet leeg zijn\n";
            }

            return retVal;
        }
    }
}
