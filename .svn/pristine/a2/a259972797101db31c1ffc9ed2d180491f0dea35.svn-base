using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep001 : TestStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericTestStep001"/> class.
        /// DO NOT USE! Only for Serializabililty!
        /// </summary>
        [Obsolete]
        public SelfTestStep001()
            : this(null)
        { }

        public SelfTestStep001(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Change SkipSelfTest";
            this.Instructions = "Press 'Ok' to change the SkipSelfTest values in Debug.xml";                 
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }

        string hostName = "172.17.5.108";
        string fingerprint = "ea:11:8b:78:c3:85:8b:25:3b:a3:7a:38:4e:68:81:d7:07:3d:d6:4f";
        string localPath = @"C:\TestFolder\";
        string localFile = @"C:\TestFolder\Debug_test.xml";
        string ftpFile = "/Settings/system/Debug_test.xml";
        string hostPath = "/Settings/system/";
        
        
        string skipSelfTest1 = "false";
        string skipSelfTest2 = "false";
        string skipSelfTest3 = "false";
        string skipSelfTest4 = "true";

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                FtpTransfer ftpTransfer = new FtpTransfer();
                ChangeXml changeDebug = new ChangeXml();

                bool resultDownload = ftpTransfer.Download(hostName, fingerprint, localPath, ftpFile);
                this.Results.Add(new BooleanResult("File Download", "", resultDownload));
                bool resultDebug = changeDebug.ChangeSelfTest(localFile, skipSelfTest1, skipSelfTest2, skipSelfTest3, skipSelfTest4);
                this.Results.Add(new BooleanResult("Change Selftest values", "", resultDebug));
                bool resultUpload = ftpTransfer.Upload(hostName, fingerprint, localFile, hostPath);
                this.Results.Add(new BooleanResult("File Upload", "", resultUpload));
            }

            this.OnTestUpdated(true);
        }
    }
}
