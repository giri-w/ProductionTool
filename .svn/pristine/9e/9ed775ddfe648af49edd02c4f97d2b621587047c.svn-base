using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class LUTTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTest()
            : this(null)
        { }

        public LUTTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Look Up Table Determination Test";
            this.Description = "Generate Look Up Table for camera images";
            this.Steps = new List<TestStep>()
            {
                new LUTTestStep01(this.testManager),
                new LUTTestStep02(this.testManager),
                new LUTTestStep03(this.testManager),
                new LUTTestStep04(this.testManager),
                new LUTTestStep05(this.testManager),
                new LUTTestStep06(this.testManager),
                new LUTTestStep07(this.testManager),
                new LUTTestStep08(this.testManager),
                new LUTTestStep09(this.testManager),
                new LUTTestStep10(this.testManager),
                new LUTTestStep11(this.testManager),
                new LUTTestStep12(this.testManager),
                new LUTTestStep13(this.testManager),

            };
            this.SetToFirstStep();
        }
    }
}
