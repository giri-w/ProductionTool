//-----------------------------------------------------------------------
// <copyright file="HwManager.cs" company="Finapres Medical Systems B.V.">
//     Copyright (C) 2016 Finapres Medical Systems B.V. (Finapres). All Rights Reserved.
//     Reproduction or disclosure of this file or its contents without the prior
//     written consent of Finapres is prohibited.
// </copyright>
// <disclaimer>
//     The software is provided 'as is' without any guarantees or warranty. Although
//     Finapres has attempted to find and correct any bugs in the software, Finapres is not
//     responsible for any damage or losses of any kind caused by the use or misuse
//     of the software.
// </disclaimer>
//-----------------------------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demcon.ProductionTool.Hardware
{
    /// <summary>
    /// This class controls and maintaines all hardware.
    /// </summary>
    public class HwManager : IDisposable
    {
        private const int UpdateSleepTime_ms = 1;
        private bool keepUpdateTimerRunning;
        private bool isUpdateRunning;

        /// <summary>
        /// Initializes a new instance of the <see cref="HwManager"/> class.
        /// </summary>
        public HwManager()
        {
            // Construct Hardware interfaces
            this.StartUpdateTimer();
        }

        ~HwManager()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void StartUpdateTimer()
        {
            this.keepUpdateTimerRunning = true;
            new Task(() =>
            {
                this.isUpdateRunning = true;
                while (this.keepUpdateTimerRunning)
                {
                    this.Update();
                    Thread.Sleep(HwManager.UpdateSleepTime_ms);
                }
                this.isUpdateRunning = false;
            }).Start();
        }

        private void Update()
        {
            // Do stuff periodically, like getting the latest data
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // quit update timer
                this.keepUpdateTimerRunning = false;
                int timeOut = 10;
                do
                {
                    Thread.Sleep(2 * HwManager.UpdateSleepTime_ms);
                } while (this.isUpdateRunning && --timeOut > 0);

                // free managed resources
            }
        }
    }
}
