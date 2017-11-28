﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep001 : TestStep
    {
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

                bool resultDownload = ftpTransfer.Download(localPath, ftpFile);
                this.Results.Add(new BooleanResult("File Download", "", resultDownload));
                bool resultDebug = changeDebug.ChangeSelfTest(localFile, skipSelfTest1, skipSelfTest2, skipSelfTest3, skipSelfTest4);
                this.Results.Add(new BooleanResult("Change Selftest values", "", resultDebug));
                bool resultUpload = ftpTransfer.Upload(localFile, hostPath);
                this.Results.Add(new BooleanResult("File Upload", "", resultUpload));
            }

            this.OnTestUpdated(true);
        }
    }
}
