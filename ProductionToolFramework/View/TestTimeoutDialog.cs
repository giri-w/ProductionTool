using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demcon.ProductionTool.View
{
    public partial class TestTimeoutDialog : Form
    {
        private static object UpdateLock = new object();
        private Task updateTask;
        private bool doUpdate;

        public TestTimeoutDialog(int timeoutTime)
        {
            InitializeComponent();
            this.doUpdate = true;
            this.updateTask = this.CreateUpdateTask(timeoutTime);
            this.updateTask.Start();
            this.TimeoutDialogResult = System.Windows.Forms.DialogResult.None;
        }

        // In this implementation the Form.DialogResult will always be Cancel
        // (automatically set on this.Close), so create a different property.
        public DialogResult TimeoutDialogResult { get; private set; }

        public void Stop()
        {
            lock (UpdateLock)
            {
                if (this.doUpdate)
                {
                    this.doUpdate = false;
                    this.BeginInvoke((Action)(() =>
                    {
                        this.Close();
                    }));
                }
            }
        }

        private Task CreateUpdateTask(int timeoutTime)
        {
            return new Task(() =>
            {
                DateTime timeout = DateTime.Now.AddMilliseconds(timeoutTime);
                TimeSpan timeLeft = timeout.Subtract(DateTime.Now);
                while (this.doUpdate && DateTime.Now.CompareTo(timeout) < 0)
                {
                    Thread.Sleep(1000);
                    lock (UpdateLock)
                    {
                        if (this.doUpdate)
                        {
                            this.BeginInvoke((Action)(() =>
                            {

                                timeLeft = timeout.Subtract(DateTime.Now);
                                this.MessageLabel.Text = string.Format("De test wordt uitgevoerd; dit kan nog {0:0} seconden duren", timeLeft.TotalSeconds);
                            }));
                        }
                    }
                }

                if (this.TimeoutDialogResult == System.Windows.Forms.DialogResult.None)
                {
                    this.TimeoutDialogResult = System.Windows.Forms.DialogResult.OK;
                }

                this.Stop();
            });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.TimeoutDialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Stop();
        }
    }
}
