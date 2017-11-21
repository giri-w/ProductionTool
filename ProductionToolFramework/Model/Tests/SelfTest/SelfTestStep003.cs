using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;
using System.Xml;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep003 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTestStep003()
            : this(null)
        { }

        public SelfTestStep003(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Change Debug.xml + Selftest.xml";
            this.Instructions = "Press 'Ok' to change the SkipSelfTest values in debug.xml \n and the minimum and maximum gray values in Selftest.xml";
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }


        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"C:\TestFolder\";
        string localFile1 = @"C:\TestFolder\Debug_test.xml";
        string ftpFile1 = "/Settings/system/Debug_test.xml";
        string localFile2 = @"C:\TestFolder\SelfTest_test.xml";
        string ftpFile2 = "/Settings/system/SelfTest_test.xml";
        string hostPath = "/Settings/system/";


        string skipSelfTest1 = "true";
        string skipSelfTest2 = "true";
        string skipSelfTest3 = "false";
        string skipSelfTest4 = "false";

        string minGray = "0";
        string maxGray = "255";

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                // Check or do something (with the hardware?) for the test
                ChangeXml changeXml = new ChangeXml();
                FtpTransfer ftpTransfer = new FtpTransfer();

                bool resultDownload1 = ftpTransfer.Download(hostName, fingerprint, localPath, ftpFile1);
                this.Results.Add(new BooleanResult("Debug.xml Download", "", resultDownload1));
                bool resultDownload2 = ftpTransfer.Download(hostName, fingerprint, localPath, ftpFile2);
                this.Results.Add(new BooleanResult("SelfTest.xml Download", "", resultDownload2));
                
                bool resultSelfTest = changeXml.ChangeSelfTest(localFile1, skipSelfTest1,skipSelfTest2,skipSelfTest3,skipSelfTest4);
                this.Results.Add(new BooleanResult("Change Selftest values", "", resultSelfTest));
                bool resultGrayValue = changeXml.ChangeGrayValue(localFile2, minGray, maxGray);
                this.Results.Add(new BooleanResult("Change Gray values", "", resultGrayValue));

                bool resultUpload1 = ftpTransfer.Upload(hostName, fingerprint, localFile1, hostPath);
                this.Results.Add(new BooleanResult("Debug.xml Upload", "", resultUpload1));
                bool resultUpload2 = ftpTransfer.Upload(hostName, fingerprint, localFile2, hostPath);
                this.Results.Add(new BooleanResult("SelfTest.xml Upload", "", resultUpload2));
               
          
            }
                this.OnTestUpdated(true);
        }
    }
}
