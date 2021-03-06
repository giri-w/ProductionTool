﻿using Demcon.ProductionTool.Model.Tests.GenericTest;
using System;
using System.Collections.Generic;


namespace Demcon.ProductionTool.Model.Tests.VolunteerScan
{
    /// <summary>
    /// The Nano Core must be able to switch between cuffs during a measurement
    /// </summary>
    public class VolunteerScan : Test
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTest"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public VolunteerScan()
            : this(null)
        { }

        public VolunteerScan(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Volunteer scan";
            this.Description = "Check if the volunteer scan results are correct";
            this.Steps = new List<TestStep>()
            {
                new VolunteerScanStep001(this.testManager),
                new VolunteerScanStep002(this.testManager),
                new VolunteerScanStep003(this.testManager),
                new VolunteerScanStep004(this.testManager),
                
            };
            this.SetToFirstStep();
        }
    }
}
