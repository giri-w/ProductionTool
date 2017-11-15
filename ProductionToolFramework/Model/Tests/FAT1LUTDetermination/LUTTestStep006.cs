﻿using System;
using Demcon.ProductionTool.Hardware;

namespace Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination
{
    public class LUTTestStep006 : SpecificTestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep006"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public LUTTestStep006()
            : this(null)
        { }

        public LUTTestStep006(TestManager testManager)
            : base(testManager)
        {
            this.Name = "LUT Determination complete";
            this.Instructions = "LUT Test is complete.\nPress Finished to return to main menu or press Back to retry the test";
            this.SupportingImage = String.Empty;
            this.ButtonOptions = EButtonOptions.Finish | EButtonOptions.Back;
            this.OnTestUpdated(false);
        }

        public override void Execute(EButtonOptions userAction, string info)
        {

            this.Results.Clear();

            if (userAction == EButtonOptions.Next)
            {
                this.Results.Add(new BooleanResult("LUT Determination complete", "LUT Test is finished", true));
                this.OnTestUpdated(true);
            }


            if (userAction == EButtonOptions.Back)
            {
                this.OnTestCanceled(true);
            }


        }
    }
}