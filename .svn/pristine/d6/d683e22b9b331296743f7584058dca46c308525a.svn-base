using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Demcon.ProductionTool.General;
using Demcon.ReportGeneratorLibrary;

namespace Demcon.ProductionTool.Model
{
    public class TestManager : IDisposable
    {
        private const int UpdateSleepTime = 250;
        private static readonly object updateTimerLock = new object();
        private TestSequence currentSequence;
        private bool keepUpdateTimerRunning;
        private Timer testTimeoutTimer;
        private CancellationTokenSource source;

        public TestManager(string applicationVersion)
        {
            this.ApplicationVersion = applicationVersion;
            this.source = new CancellationTokenSource();
            this.GetConfigurationData();
        }

        public event EventHandler<EventArgs<string>> InformationRequest;
        public event EventHandler TestStopped;

        private void GetConfigurationData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TestConfig));
            if (!Directory.Exists(TestConfig.ConfigFilePath))
            {
                Directory.CreateDirectory(TestConfig.ConfigFilePath);
            }

            if (!File.Exists(TestConfig.ConfigFileName))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(TestConfig.ConfigFileName));
                this.Configuration = new TestConfig();
                this.Configuration.SetDefaults();

                // Use an XML writer with explicit XmlWriterSettins to handle newlines (\r\n) properly
                XmlWriterSettings xmlSettings = new XmlWriterSettings();
                xmlSettings.NewLineHandling = NewLineHandling.Entitize;
                xmlSettings.Indent = true;
                XmlWriter xmlWriter = XmlWriter.Create(TestConfig.ConfigFileName, xmlSettings);
                try
                {
                    serializer.Serialize(xmlWriter, this.Configuration);
                }
                catch (Exception e)
                {
                    throw new Exception("Error saving configuration file '" + TestConfig.ConfigFileName + "'; Error while deserializing '" + TestConfig.ConfigFileName + "': " + e.Message, e.InnerException);
                }
                finally
                {
                    xmlWriter.Close();
                }
            }
            else
            {
                try
                {
                    using (StreamReader reader = new StreamReader(TestConfig.ConfigFileName))
                    {
                        object o = serializer.Deserialize(reader);
                        if (typeof(TestConfig).Equals(o.GetType()))
                        {
                            if (((TestConfig)o).IsConfiguredCorrectly())
                            {
                                this.Configuration = (TestConfig)o;
                            }
                            else
                            {
                                throw new Exception("Error loading configuration file '" + TestConfig.ConfigFileName + "'; Not all parameters are configured (correctly)");
                            }
                        }
                        else
                        {
                            throw new Exception("Error loading configuration file '" + TestConfig.ConfigFileName + "'; Unexpected deserialized object type: " + o.GetType().ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error loading configuration file '" + TestConfig.ConfigFileName + "'; Error while deserializing '" + TestConfig.ConfigFileName + "': " + e.Message, e.InnerException);
                }
            }
        }        

        ~TestManager()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Occurs when a GUI action in this page requests a test sequence to be done.
        /// </summary>
        public event EventHandler<EventArgs<string>> TestSequenceRequestedEvent;
        public event EventHandler CurrentStepChangedEvent;
        public event EventHandler<EventArgs<int>> SetTestTimeout;

        public TestConfig Configuration { get; set; }

        public TestSequence CreateTest(ETestSequence sequence)
        {
            switch (sequence)
            {
                case ETestSequence.Generic:
                    this.CurrentSequence = new GenericTestSequence(this);
                    break;
                case ETestSequence.None:
                    this.CurrentSequence = null;
                    break;
                default:
                    break;
            }

            return this.CurrentSequence;
        }

        public void StartTest()
        {
            if (this.CurrentSequence == null)
            {
                // Connect to hardware
            }
            else
            {
                if (this.TestSequenceRequestedEvent != null)
                {
                    this.TestSequenceRequestedEvent(this, null);
                }

                // Disconnect to hardware
            }
        }

        public string StopTest()
        {
            EventHandler handler = this.TestStopped;
            if (handler != null)
            {
                handler(this, null);
            }

            string savedPath;
            try
            {
                savedPath = this.CurrentSequence.Save(this.Configuration.DataRootPath);
                if (this.CurrentSequence.AutoGenerateReport)
                {
                    this.GenerateReport(this.CurrentSequence.SerialNumber, this.CurrentSequence.Dwo);
                }
            }
            finally
            {
                this.CurrentSequence = null;
                if (this.TestSequenceRequestedEvent != null)
                {
                    this.TestSequenceRequestedEvent(this, null);
                }
            }

            return savedPath;
        }

        public string GenerateReport(string serialNumber, string dwo)
        {
            string testPath = Path.Combine(this.Configuration.DataRootPath.ToString(), serialNumber, dwo);
            string pdf;
            using (ReportGenerator g = new ReportGenerator(testPath))
            {
                g.Generate();
                pdf = g.ReportPdf;
            }
            return pdf;
        }

        public void RequestInfo(string userText)
        {
            if (this.InformationRequest != null)
            {
                this.InformationRequest(this, new EventArgs<string>(userText));
            }
        }

        public string ApplicationVersion { get; set; }

        public TestSequence CurrentSequence
        {
            get
            {
                return this.currentSequence;
            }

            private set
            {
                if (this.currentSequence != null)
                {
                    this.currentSequence.UpdatedEvent -= new EventHandler<EventArgs<bool>>(OnCurrentStepChanged);
                }

                this.currentSequence = value;
                if (this.currentSequence != null)
                {
                    this.currentSequence.UpdatedEvent += new EventHandler<EventArgs<bool>>(OnCurrentStepChanged);
                    this.currentSequence.SetToFirstTest();
                }
            }
        }

        public List<Test> CurrentTests
        {
            get
            {
                List<Test> retVal = null;
                if (this.currentSequence != null)
                {
                    retVal = this.currentSequence.Tests;
                }

                return retVal;
            }
        }

        public Test CurrentTest
        {
            get
            {
                Test retVal = null;
                if (this.currentSequence != null)
                {
                    retVal = this.currentSequence.CurrentTest;
                }

                return retVal;
            }
        }

        public List<TestStep> CurrentTestSteps
        {
            get
            {
                List<TestStep> retVal = null;
                if (this.CurrentTest != null)
                {
                    retVal = this.CurrentTest.Steps;
                }

                return retVal;
            }
        }

        public TestStep CurrentTestStep
        {
            get
            {
                TestStep retVal = null;
                if (this.CurrentTest != null)
                {
                    retVal = this.CurrentTest.CurrentTestStep;
                }

                return retVal;
            }
        }

        public void Execute(EButtonOptions userAction, string info)
        {
            if (userAction == EButtonOptions.Cancel)
            {
                this.SetCurrentTest(null);
            }
            else
            {
                this.CurrentSequence.Execute(userAction,info);
            }
        }

   

        public void SetCurrentTest(Test newCurrentTest)
        {
            if (this.CurrentSequence != null)
            {
                this.CurrentSequence.SetCurrentTest(newCurrentTest);
            }
        }

        public void SetWaitTimer(int timeout)
        {
            this.SetTestTimeout(this, new EventArgs<int>(timeout));
            this.source = new CancellationTokenSource();
        }

        public void WaitForTestResponse(int timeout)
        {
            this.SetTestTimeout(this, new EventArgs<int>(timeout));
            this.source = new CancellationTokenSource();
            this.source.Token.WaitHandle.WaitOne(timeout);
        }

        public void StopWaitingForResponse()
        {
            this.source.Cancel();
        }

        private void OnCurrentStepChanged(object sender, EventArgs<bool> e)
        {
            EventHandler handler = this.CurrentStepChangedEvent;
            if (handler != null)
            {
                handler(sender, null);
            }
        }

        //private void StartUpdateTimer()
        //{
        //    new Task(() =>
        //    {
        //        if (Monitor.TryEnter(updateTimerLock))
        //        {
        //            this.keepUpdateTimerRunning = true;
        //            try
        //            {
        //                while (keepUpdateTimerRunning)
        //                {
        //                    this.Update();
        //                    Thread.Sleep(UpdateSleepTime);
        //                }
        //            }
        //            finally
        //            {
        //                Monitor.Exit(updateTimerLock);
        //            }
        //        }
        //    }).Start();
        //}

        //private void Update()
        //{
        //    this.NanoCore.Update();
        //    this.NanoCore.Interpreter.QueryMode();
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                this.keepUpdateTimerRunning = false;
            }
        }
    }
}
