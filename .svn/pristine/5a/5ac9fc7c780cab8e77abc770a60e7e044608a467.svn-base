using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Demcon.ProductionTool.General;
using Demcon.ProductionTool.Model.Tests.CalibrationTest;
using Demcon.ProductionTool.Model.Tests.GenericTest;

namespace Demcon.ProductionTool.Model
{
    /// <summary>
    /// Functionality that makes the test step work in the application
    /// </summary>
    /// <seealso cref="Demcon.ProductionTool.Model.IConclusionItem" />
    [Serializable]
    [XmlInclude(typeof(GenericTestStep001))]
    [XmlInclude(typeof(GenericTestStep002))]
    [XmlInclude(typeof(GenericTestStep003))]
    [XmlInclude(typeof(CalibrationTestStep001))]
    [XmlInclude(typeof(CalibrationTestStep002))]
    [XmlInclude(typeof(CalibrationTestStep003))]
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
