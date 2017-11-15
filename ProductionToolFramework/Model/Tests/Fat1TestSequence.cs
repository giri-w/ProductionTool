﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Model.Tests.GenericTest;
using Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination;
using Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration;
using Demcon.ProductionTool.Model.Tests.SelfTest;


namespace Demcon.ProductionTool.Model
{
    [Serializable]
    public class Fat1TestSequence : TestSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestSequence"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public Fat1TestSequence()
            : this(null)
        { }

        public Fat1TestSequence(TestManager testManager)
            : base(testManager)
        {
            this.Name = "FAT 1";
            this.Description = "Work instruction to configure the right settings";
            this.AddtionalInformationRequestText1 = "PCA serie nummer";
            this.AddtionalInformationRequestText2 = "W100 batch nummer";
            this.Tests = new List<Test>();
            this.Tests.Add(new GenericTest(this.testManager));
            this.Tests.Add(new SelfTest(this.testManager));
            this.Tests.Add(new LUTTest(this.testManager));
            this.Tests.Add(new FMGTest(this.testManager));
            this.SetToFirstTest();
        }

        protected override string CheckAdditionalInfo1(string additionalInfo)
        {
            string retVal = string.Empty;

            // No input expected by default, implement check to verify the input.
            if (string.IsNullOrWhiteSpace(additionalInfo))
            {
                retVal = this.AddtionalInformationRequestText1 + "Can not be empty\n";
            }

            return retVal;
        }

        protected override string CheckAdditionalInfo2(string additionalInfo)
        {
            string retVal = string.Empty;

            // No input expected by default, implement check to verify the input.
            if (string.IsNullOrWhiteSpace(additionalInfo))
            {
                retVal = this.AddtionalInformationRequestText2 + "Can not be empty\n";
            }

            return retVal;
        }
    }
}
