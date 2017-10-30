using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.GenericTest
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class GenericTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public GenericTest()
            : this(null)
        { }

        public GenericTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Eindtest prestatie";
            this.Description = "Controleert de meetprestatie van de Nano Core";
            this.Steps = new List<TestStep>()
            {
                new GenericTestStep001(this.testManager),
                new GenericTestStep002(this.testManager),
                new GenericTestStep003(this.testManager),
                new GenericTestStep004(this.testManager),
            };
            this.SetToFirstStep();
        }
    }
}
