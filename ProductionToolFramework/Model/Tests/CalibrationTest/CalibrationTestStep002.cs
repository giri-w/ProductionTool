﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Demcon.ProductionTool.Model.Tests.CalibrationTest
{
    public class CalibrationTestStep002 : TestStep
    {
        private const string InstructionText = 
                                "- Veranderende instructie met huidige waarde ter controle voor het doorgaan?\n" +
                                "- Huidige tijd: {0}.\n" +
                                "- Druk op OK";

        private bool keepUpdating;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalibrationTestStep002"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public CalibrationTestStep002()
            : this(null)
        { }

        public CalibrationTestStep002(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Vingersimulator op druk brengen";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        public override void Start()
        {
            this.Results.Clear(); 
            new Task(() =>
            {
                this.keepUpdating = true;
                while (keepUpdating)
                {
                    this.Instructions = string.Format(CalibrationTestStep002.InstructionText, DateTime.Now);
                    this.OnTestUpdated(false);
                    System.Threading.Thread.Sleep(1000);
                }
            }).Start();
        }

        public override void Stop()
        {
            base.Stop();
            this.keepUpdating = false;
        }

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Stop();

            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                // Check or do something (with the hardware?) for the test
                this.Results.Add(new BooleanResult("Result", "Just a random result", Math.IEEERemainder(DateTime.Now.Second, 2) > 0));
            }

            this.OnTestUpdated(true);
        }
    }
}