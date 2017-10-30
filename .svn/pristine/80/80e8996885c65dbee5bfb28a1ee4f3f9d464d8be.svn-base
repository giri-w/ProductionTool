using System;
using System.IO;

namespace Demcon.ProductionTool.Model
{
    public class TestConfig
    {
        public static readonly string ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Finapres Medical Systems");
        public static readonly string ConfigFileName = Path.Combine(TestConfig.ConfigFilePath, "TestConfig.xml");

        public TestConfig()
        {
        }

        public void SetDefaults()
        {
            this.NanoCoreCOMPort = "COM14";
            this.FingerSimulatorCOMPort = "COM15";
            this.DataRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NanoCoreTestResults");
        }

        public bool IsConfiguredCorrectly()
        {
            return
                !string.IsNullOrWhiteSpace(this.NanoCoreCOMPort) &&
                !string.IsNullOrWhiteSpace(this.FingerSimulatorCOMPort) &&
                !string.IsNullOrWhiteSpace(this.DataRootPath);
        }

        public string NanoCoreCOMPort { get; set; }
        public string FingerSimulatorCOMPort { get; set; }
        public string DataRootPath { get; set; }
    }
}
