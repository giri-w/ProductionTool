using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class SSTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SSTest()
            : this(null)
        { }

        public SSTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Signal Stability Test";
            this.Description = "Test signal stability from the measurement images";
            this.Steps = new List<TestStep>()
            {
                new SSTestStep001(this.testManager),
                new SSTestStep002(this.testManager),
                new SSTestStep003(this.testManager),
                new SSTestStep004(this.testManager),
                
            };
            this.SetToFirstStep();
        }
    }
}
