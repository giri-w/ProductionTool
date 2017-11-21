using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class SACTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SACTest()
            : this(null)
        { }

        public SACTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Spatial Accuracy Test";
            this.Description = "Test signal stability from the measurement images";
            this.Steps = new List<TestStep>()
            {
                new SACTestStep01(this.testManager),
                new SACTestStep02(this.testManager),
                new SACTestStep03(this.testManager),
                new SACTestStep04(this.testManager),
                new SACTestStep05(this.testManager),
                new SACTestStep06(this.testManager),
                new SACTestStep07(this.testManager),
                new SACTestStep08(this.testManager),
                new SACTestStep09(this.testManager),
                new SACTestStep10(this.testManager),
                new SACTestStep11(this.testManager),
                new SACTestStep12(this.testManager),
                new SACTestStep13(this.testManager),

            };
            this.SetToFirstStep();
        }
    }
}
