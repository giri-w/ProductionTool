using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT4SignalStability
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class LPSTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LPSTest()
            : this(null)
        { }

        public LPSTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Laser Power Stability Test";
            this.Description = "Test Laser Power stability from the measurement images";
            this.Steps = new List<TestStep>()
            {
                new LPSTestStep01(this.testManager),
                new LPSTestStep02(this.testManager),
                new LPSTestStep03(this.testManager),
                new LPSTestStep04(this.testManager),
                new LPSTestStep05(this.testManager),
                new LPSTestStep06(this.testManager),
                new LPSTestStep07(this.testManager),
                new LPSTestStep08(this.testManager),
                
            };
            this.SetToFirstStep();
        }
    }
}
