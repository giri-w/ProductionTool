using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.CalibrationTest
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class CalibrationTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalibrationTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public CalibrationTest()
            : this(null)
        { }

        public CalibrationTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Kalibratie lekdichtheid vingersimulator";
            this.Description = "Controleert vingersimulator op lekdichtheid";
            this.Steps = new List<TestStep>()
            {
                new CalibrationTestStep001(this.testManager),
                new CalibrationTestStep002(this.testManager),
                new CalibrationTestStep003(this.testManager),
            };
            this.SetToFirstStep();
        }
    }
}
