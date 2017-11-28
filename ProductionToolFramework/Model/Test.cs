﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model.Tests.GenericTest;
using Demcon.ProductionTool.Model.Tests.FAT1FieldMaskGeneration;
using Demcon.ProductionTool.Model.Tests.FAT1LUTDetermination;
using Demcon.ProductionTool.Model.Tests.FAT4SignalStability;
using Demcon.ProductionTool.Model.Tests.FAT4SpatialAccuracy;
using Demcon.ProductionTool.Model.Tests.FAT4VolunteerScan;

namespace Demcon.ProductionTool.Model
{
    [Serializable]
    [XmlInclude(typeof(GenericTest))]
    [XmlInclude(typeof(FMGTest))]
    [XmlInclude(typeof(LUTTest))]
    [XmlInclude(typeof(LPSTest))]
    [XmlInclude(typeof(SACTest))]
    [XmlInclude(typeof(VSCTest))]

    public abstract class Test : IConclusionItem
    {
        private int currentTestStepIndex = -1;
        protected TestManager testManager;
        private static readonly object TestLock = new object();
        /// <summary>
        /// Initializes a new instance of the <see cref="Test"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public Test()
            : this(null)
        { }

        public Test(TestManager testManager)
        {
            this.testManager = testManager;
            this.Date = DateTime.Now;
        }

        public event EventHandler<EventArgs<bool>> UpdatedEvent;
        public event EventHandler<EventArgs<bool>> CanceledEvent;


        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public string Description { get; set; }

        [XmlAttribute]
        public DateTime Date { get; set; }

        [XmlAttribute]
        public string Source
        {
            get
            {
                return this.testManager.ApplicationVersion;
            }

            set
            {
                // ignore the set; only here in order to enable this property to be serialized.
            }
        }

        private List<TestStep> steps;
        public List<TestStep> Steps
        {
            get
            {
                return this.steps;
            }
            
            set
            {
                this.steps = value;
            }
        }

        public TestStep CurrentTestStep
        {
            get
            {
                TestStep retVal = null;
                List<TestStep> steps = this.Steps;
                if (steps != null && this.CurrentTestStepIndex >= 0 && this.CurrentTestStepIndex < steps.Count)
                {
                    retVal = steps[this.CurrentTestStepIndex];
                }

                return retVal;
            }
        }

        public virtual void Execute(EButtonOptions userAction,string info)
        {
            new Task(() =>
            {
                if (Monitor.TryEnter(Test.TestLock))
                {
                    try
                    {
                        this.CurrentTestStep.Results.Clear();
                        this.CurrentTestStep.Execute(userAction,info);
                    }
                    catch (Exception ex)
                    {
                        StringBuilder sb = new StringBuilder("Error while executing test, please restart:");
                        Exception e = ex;
                        while (e != null)
                        {
                            sb.AppendLine(e.Message);
                            e = e.InnerException;
                        }

                        this.CurrentTestStep.Results.Add(new ErrorResult("Error while executing test", sb.ToString()));
                    }
                    finally
                    {
                        Monitor.Exit(Test.TestLock);
                    }
                }
            }).Start();
        }

        public TestStep GetNextStep()
        {
            if (this.CurrentTestStep != null)
            {
                this.CurrentTestStep.Stop();
            }

            this.CurrentTestStepIndex++;
            if (this.CurrentTestStep != null)
            {
                this.CurrentTestStep.Start();
            }

            return this.CurrentTestStep;
        }

        public TestStep GetPrevStep()
        {
            if (this.CurrentTestStep != null)
            {
                this.CurrentTestStep.Stop();
            }

            this.CurrentTestStepIndex--;    
            if (this.CurrentTestStep != null)
            {
                this.CurrentTestStep.Start();
            }

            return this.CurrentTestStep;
        }

        public void SetToFirstStep()
        {
            this.CurrentTestStepIndex = 0;
            this.OnTestUpdated(this, new EventArgs<bool>(false));
        }

        [XmlAttribute]
        public ETestConclusion Conclusion
        {
            get
            {
                return (ETestConclusion)this.Steps.Min(s => (int)s.Conclusion);
            }

            set
            {
                // ignore the set; only here in order to enable this property to be serialized.
            }
        }

        public int StepCount
        {
            get
            {
                int retVal = 0;
                if (this.Steps != null)
                {
                    retVal = this.Steps.Count;
                }

                return retVal;
            }
        }

        private int CurrentTestStepIndex
        {
            get
            {
                return this.currentTestStepIndex;
            }

            set
            {
                if (this.currentTestStepIndex != value)
                {
                    if (this.CurrentTestStep != null)
                    {
                        this.CurrentTestStep.UpdatedEvent -= new EventHandler<EventArgs<bool>>(this.OnTestUpdated);
                        this.CurrentTestStep.CanceledEvent -= new EventHandler<EventArgs<bool>>(this.OnTestCanceled);
                    }

                    this.currentTestStepIndex = value;
                    if (this.CurrentTestStep != null)
                    {
                        this.CurrentTestStep.UpdatedEvent += new EventHandler<EventArgs<bool>>(this.OnTestUpdated);
                        this.CurrentTestStep.CanceledEvent += new EventHandler<EventArgs<bool>>(this.OnTestCanceled);
                    }
                }
            }
        }

        protected void OnTestCanceled(object sender, EventArgs<bool> e)
        {
            if (e.Value)
            {
                this.GetPrevStep();
            }

            EventHandler<EventArgs<bool>> handler = this.CanceledEvent;
            if (handler != null)
            {
                bool canceled = this.CurrentTestStep == null;
                this.CanceledEvent(sender, new EventArgs<bool>(canceled));
            }
        }

        protected void OnTestUpdated(object sender, EventArgs<bool> e)
        {
            if (e.Value)
            {
                this.GetNextStep();
            }

            EventHandler<EventArgs<bool>> handler = this.UpdatedEvent;
            if (handler != null)
            {
                bool completed = this.CurrentTestStep == null;
                this.UpdatedEvent(sender, new EventArgs<bool>(completed));
            }
        }
    }
}
