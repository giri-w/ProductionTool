using System;
using System.Collections.Generic;

namespace Demcon.ProductionTool.Model.Tests
{
    /// <summary>
    /// Implement gereric functionality that is used in all test steps, that is specific for the project
    /// </summary>
    /// <seealso cref="Demcon.ProductionTool.Model.TestStep" />
    public abstract class SpecificTestStep : TestStep
    {
        /// <summary>
        /// The status object; this should be an object in the hardware / driver handling the results of the tests.
        /// Can be done differently, but this is how it was done in the initial project.
        /// </summary>
        private object status = new object();

        private EventHandler resultHandler;

        public SpecificTestStep(TestManager testManager)
            : base (testManager)
        {
            this.Results = new List<Result>();
        }

        protected void ExecuteTest(int timeout)
        {
            if (status != null)
            {
                // in some cases tests might be run twice, however we only want a single resulthandler
                if (resultHandler == null)
                {
                    resultHandler = new EventHandler(this.ResultsUpdatedEvent);

                    // Subscribe to the eventhandler here. For the example project, the 'event' is triggered some time after.
                    // For the actual application, the Task should be removed.
                    // status.ResultsUpdatedEvent += resultHandler;
                }

                // Execute the actions of the test
                new System.Threading.Tasks.Task(() =>
                {
                    System.Threading.Thread.Sleep(3000);
                    resultHandler(this, null);
                }).Start();

                if (timeout >= 0)
                {
                    // Wait for the test to finish
                    this.testManager.WaitForTestResponse(timeout);
                    if (this.Results.Count == 0)
                    {
                        this.StopTest();
                        this.Results.Add(new BooleanResult("Error while testing", "Test didn't finish in time", false));
                        this.OnTestUpdated(true);
                    }
                }
            }
        }

        public void StopTest()
        {
            this.testManager.StopWaitingForResponse();
            if (this.status != null)
            {
                // status.ResultsUpdatedEvent -= resultHandler;
                resultHandler = null;
            }
        }

        protected virtual void OnTestUpdated(bool completed)
        {
            base.OnTestUpdated(completed);
            if (completed && this.status != null)
            {
                // status.ResultsUpdatedEvent -= resultHandler;
                resultHandler = null;
            }
        }

        /// <summary>
        /// Can be called when the hardware has the results of the test ready.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResultsUpdatedEvent(object sender, EventArgs e)
        {
            if (sender != null)
            {
                this.HandleResult(sender);
            }

            this.testManager.StopWaitingForResponse();
        }

        protected virtual void HandleResult(object status)
        {
            // Implement in the steps.
            Console.WriteLine(String.Format("Step {0} received unexpected status", this.Name));
        }

        private void HandleError(object sender, object e)
        {
            this.Results.Add(new ErrorResult("Error received during test " + this.Name, e.ToString()));
            this.testManager.StopWaitingForResponse();
            this.OnTestUpdated(true);
        }
    }
}
