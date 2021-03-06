﻿using System;
using System.Collections.Generic;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep01 : TestStep
    {
        [Obsolete]
        public LUTTestStep01()
            : this(null)
        { }

        public LUTTestStep01(TestManager testManager)
            : base(testManager)
        {
            this.Name = "1. Machine Preparation";
            this.Instructions = "- Remove targets and hand support blocks from the scanner\n" +
                                "- Cover the arm detection";
            this.SupportingImage = @"Images\UI Demcon\ImNoAvailable.png";
            this.ButtonOptions = EButtonOptions.Next;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.Next)
            {
                // Continue to the next step
                this.Results.Add(new BooleanResult("Machine Preparation", "Checked", true));
                this.OnTestUpdated(true);
            }

            
        }
    }
}
