﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model.Tests.CalibrationTest;
using Demcon.ProductionTool.Model.Tests.GenericTest;
using Demcon.ProductionTool.Model.Tests.VolunteerScan;
using Demcon.ProductionTool.Model.Tests.SelfTest;
using Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration;
using Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination;
using Demcon.ProductionTool.Model.Tests.FAT4SignalStability;
using Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy;
using Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan;

namespace Demcon.ProductionTool.Model
{
    /// <summary>
    /// Functionality that makes the test step work in the application
    /// </summary>
    /// <seealso cref="Demcon.ProductionTool.Model.IConclusionItem" />
    [Serializable]

    // Generic Test
    [XmlInclude(typeof(GenericTestStep001))]
    [XmlInclude(typeof(GenericTestStep002))]
    [XmlInclude(typeof(GenericTestStep003))]

    // Volunteer Scan
    [XmlInclude(typeof(VolunteerScanStep001))]
    [XmlInclude(typeof(VolunteerScanStep002))]
    [XmlInclude(typeof(VolunteerScanStep003))]
    [XmlInclude(typeof(VolunteerScanStep004))]

    // Self Test
    [XmlInclude(typeof(SelfTestStep001))]
    [XmlInclude(typeof(SelfTestStep002))]
    [XmlInclude(typeof(SelfTestStep003))]
    [XmlInclude(typeof(SelfTestStep004))]

    // Calibration 
    [XmlInclude(typeof(CalibrationTestStep001))]
    [XmlInclude(typeof(CalibrationTestStep002))]
    [XmlInclude(typeof(CalibrationTestStep003))]

    // Field Mask Generation
    [XmlInclude(typeof(FMGTestStep01))]
    [XmlInclude(typeof(FMGTestStep02))]
    [XmlInclude(typeof(FMGTestStep03))]
    [XmlInclude(typeof(FMGTestStep04))]
    [XmlInclude(typeof(FMGTestStep05))]
    [XmlInclude(typeof(FMGTestStep06))]
    [XmlInclude(typeof(FMGTestStep07))]
    [XmlInclude(typeof(FMGTestStep08))]
    [XmlInclude(typeof(FMGTestStep09))]
    [XmlInclude(typeof(FMGTestStep10))]
    [XmlInclude(typeof(FMGTestStep11))]
    [XmlInclude(typeof(FMGTestStep12))]

    // LUT Determination
    [XmlInclude(typeof(LUTTestStep01))]
    [XmlInclude(typeof(LUTTestStep02))]
    [XmlInclude(typeof(LUTTestStep03))]
    [XmlInclude(typeof(LUTTestStep04))]
    [XmlInclude(typeof(LUTTestStep05))]
    [XmlInclude(typeof(LUTTestStep06))]
    [XmlInclude(typeof(LUTTestStep07))]
    [XmlInclude(typeof(LUTTestStep08))]
    [XmlInclude(typeof(LUTTestStep09))]
    [XmlInclude(typeof(LUTTestStep10))]
    [XmlInclude(typeof(LUTTestStep11))]
    [XmlInclude(typeof(LUTTestStep12))]
    [XmlInclude(typeof(LUTTestStep13))]

    // Signal Stability
    [XmlInclude(typeof(LPSTestStep01))]
    [XmlInclude(typeof(LPSTestStep02))]
    [XmlInclude(typeof(LPSTestStep03))]
    [XmlInclude(typeof(LPSTestStep04))]
    [XmlInclude(typeof(LPSTestStep05))]
    [XmlInclude(typeof(LPSTestStep06))]
    [XmlInclude(typeof(LPSTestStep07))]
    [XmlInclude(typeof(LPSTestStep08))]

    // Spatial Accuracy
    [XmlInclude(typeof(SACTestStep01))]
    [XmlInclude(typeof(SACTestStep02))]
    [XmlInclude(typeof(SACTestStep03))]
    [XmlInclude(typeof(SACTestStep04))]
    [XmlInclude(typeof(SACTestStep05))]
    [XmlInclude(typeof(SACTestStep06))]
    [XmlInclude(typeof(SACTestStep07a))]
    [XmlInclude(typeof(SACTestStep07b))]
    [XmlInclude(typeof(SACTestStep08))]
    [XmlInclude(typeof(SACTestStep09))]
    [XmlInclude(typeof(SACTestStep10))]
    [XmlInclude(typeof(SACTestStep11))]
    [XmlInclude(typeof(SACTestStep12a))]
    [XmlInclude(typeof(SACTestStep12b))]
    [XmlInclude(typeof(SACTestStep13))]

    // VolunteerScan
    [XmlInclude(typeof(VSCTestStep01))]
    [XmlInclude(typeof(VSCTestStep02))]
    [XmlInclude(typeof(VSCTestStep03))]
    [XmlInclude(typeof(VSCTestStep04))]

    public abstract class TestStep : IConclusionItem
    {
        protected TestManager testManager;
        private bool sleeping = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestStep"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public TestStep()
            : this(null)
        { }

        public TestStep(TestManager testManager)
        {
            this.testManager = testManager;
            this.Results = new List<Result>();
        }

        public event EventHandler<EventArgs<bool>> UpdatedEvent;
        public event EventHandler<EventArgs<bool>> CanceledEvent;

        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public string Instructions { get; set; }

        [XmlIgnore]
        public string SupportingImage { get; set; }

        [XmlIgnore]
        public EButtonOptions ButtonOptions { get; set; }
        public EVarOptions VarOptions { get; set; }
        public List<Result> Results { get; set; }
        public string testResult = string.Empty;
        public string VarValue = string.Empty;
        public string pyFullPath = string.Empty;
        public string[] pyArgument = new string[10]; 


        [XmlAttribute]
        public ETestConclusion Conclusion
        {
            get
            {
                ETestConclusion retVal = ETestConclusion.NotTested;
                if (this.Results != null && this.Results.Count > 0)
                {
                    retVal = (ETestConclusion)this.Results.Min(r => (int)r.Conclusion);
                }

                return retVal;
            }

            set
            {
                // ignore the set; only here in order to enable this property to be serialized.
            }
        }

        public abstract void Execute(EButtonOptions userAction,string info);

        public virtual void InputReceived(string input)
        {
            // Implement in the steps.
            Console.WriteLine(String.Format("Step {0} received unexpected input: {1}", this.Name, input));
        }

        protected virtual void OnTestUpdated(bool completed)
        {
            EventHandler<EventArgs<bool>> handler = this.UpdatedEvent;
            if (handler != null)
            {
                this.UpdatedEvent(this, new EventArgs<bool>(completed));
            }
        }

        protected virtual void OnTestCanceled(bool canceled)
        {
            EventHandler<EventArgs<bool>> handler = this.CanceledEvent;
            if (handler != null)
            {
                this.CanceledEvent(this, new EventArgs<bool>(canceled));
            }
        }

        /// <summary>
        /// Starts actions that have to be performed while this step is active.
        /// NOTE: This should not generate results as conclusion of this test. Use the Execute method for that.
        /// </summary>
        public virtual void Start()
        { }

        /// <summary>
        /// Stops actions that were started in the Start method.
        /// </summary>
        public virtual void Stop()
        {
            this.sleeping = false;
        }

        protected void Sleep(int secondsToSleep)
        {
            this.sleeping = true;

            for (int i = 0; i < secondsToSleep && sleeping; i++)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        protected void Sleep(DateTime startTime, int millisecondsToSleep)
        {
            this.sleeping = true;
            DateTime wakeupTime = startTime.AddMilliseconds(millisecondsToSleep);
            while (DateTime.Now < wakeupTime && this.sleeping)
            {
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
