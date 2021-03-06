﻿using System;
using System.Collections.Generic;
using Demcon.ProductionTool.Hardware;
using System.Threading.Tasks;
using System.Windows.Forms;
using HemicsFat;


namespace Demcon.ProductionTool.Model.Tests.SelfTest
{
    public class SelfTestStep004 : TestStep
    {
        [Obsolete]
        public SelfTestStep004()
            : this(null)
        { }

        public SelfTestStep004(TestManager testManager)
            : base(testManager)
        {
            this.Name = "Last selftest check";
            this.Instructions = "Restart the system and check whether the system succesfully goes through the selftests. \n" +
                                "Press 'Ok' to change the SelfTest.xml settings back to the original values."; 
            this.SupportingImage = string.Empty;
            this.ButtonOptions = EButtonOptions.OK;
            this.Results = new List<Result>();
            this.OnTestUpdated(false);
        }
        string localPath = @"C:\TestFolder\";
        string localFile = @"C:\TestFolder\SelfTest_test.xml";
        string ftpFile = "/Settings/system/SelfTest_test.xml";
        string hostPath = "/Settings/system/";

        string minGray = "0";
        string maxGray = "40";

        public override void Execute(EButtonOptions userAction, string info)
        {
            this.Results.Clear();
            if (userAction == EButtonOptions.OK)
            {
                ChangeXml changeXml = new ChangeXml();
                FtpTransfer ftpTransfer = new FtpTransfer();

                bool resultDownload = ftpTransfer.Download(localPath, ftpFile);
                this.Results.Add(new BooleanResult("File Download", "", resultDownload));

                bool resultGrayValue = changeXml.ChangeGrayValue(localFile, minGray, maxGray);
                this.Results.Add(new BooleanResult("Change Selftest values", "", resultGrayValue));

                bool resultUpload = ftpTransfer.Upload(localFile, hostPath);
                this.Results.Add(new BooleanResult("File Upload", "", resultUpload));
         
                
            }

            this.OnTestUpdated(true);
        }
    }
}
