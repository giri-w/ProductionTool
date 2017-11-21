using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class VSCTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VSCTest()
            : this(null)
        { }

        public VSCTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Signal Stability Test";
            this.Description = "Test signal stability from the measurement images";
            this.Steps = new List<TestStep>()
            {
                new VSCTestStep01(this.testManager),
                new VSCTestStep02(this.testManager),
                new VSCTestStep03(this.testManager),
                new VSCTestStep04(this.testManager),
                
            };
            this.SetToFirstStep();
        }
    }
}
