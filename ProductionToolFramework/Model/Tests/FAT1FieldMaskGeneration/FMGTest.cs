using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class FMGTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public FMGTest()
            : this(null)
        { }

        public FMGTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Field Mask Generation Test";
            this.Description = "Generate Field Mask from camera images";
            this.Steps = new List<TestStep>()
            {
                new FMGTestStep001(this.testManager),
                new FMGTestStep002(this.testManager),
                new FMGTestStep003(this.testManager),
                new FMGTestStep004(this.testManager),
                new FMGTestStep005(this.testManager),
                new FMGTestStep006(this.testManager),

            };
            this.SetToFirstStep();
        }
    }
}
