﻿using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class SelfTest : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTest()
            : this(null)
        { }

        public SelfTest(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Self test";
            this.Description = "Check if the volunteer scan results are correct";
            this.Steps = new List<TestStep>()
            {
                new SelfTestStep000(this.testManager),
                new SelfTestStep005(this.testManager),
                new SelfTestStep001(this.testManager),
                new SelfTestStep002(this.testManager),
                new SelfTestStep003(this.testManager),
                new SelfTestStep004(this.testManager),
               
            };
            this.SetToFirstStep();
        }
    }
}