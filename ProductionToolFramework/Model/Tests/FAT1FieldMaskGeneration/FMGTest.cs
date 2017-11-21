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
                new FMGTestStep01(this.testManager),
                new FMGTestStep02(this.testManager),
                new FMGTestStep03(this.testManager),
                new FMGTestStep04(this.testManager),
                new FMGTestStep05(this.testManager),
                new FMGTestStep06(this.testManager),
                new FMGTestStep07(this.testManager),
                new FMGTestStep08(this.testManager),
                new FMGTestStep09(this.testManager),
                new FMGTestStep10(this.testManager),
                new FMGTestStep11(this.testManager),
                new FMGTestStep12(this.testManager),
            };
            this.SetToFirstStep();
        }
    }
}
