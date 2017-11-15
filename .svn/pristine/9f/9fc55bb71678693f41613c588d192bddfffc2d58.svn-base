using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.LUTDetermination
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
                new LUTTestStep001(this.testManager),
                new LUTTestStep0021(this.testManager),
                new LUTTestStep0022(this.testManager),
                new LUTTestStep0023(this.testManager),
                new LUTTestStep0031(this.testManager),
                new LUTTestStep0032(this.testManager),
                new LUTTestStep004(this.testManager),
                new LUTTestStep005(this.testManager),
                new LUTTestStep006(this.testManager),
            };
            this.SetToFirstStep();
        }
    }
}
