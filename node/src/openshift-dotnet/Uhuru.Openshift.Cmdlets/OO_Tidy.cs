﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Uhuru.Openshift.Common;
using Uhuru.Openshift.Common.Models;
using Uhuru.Openshift.Runtime;

namespace Uhuru.Openshift.Cmdlets
{
    [Cmdlet("OO", "Tidy")]
    public class OO_Tidy : System.Management.Automation.Cmdlet 
    {
        [Parameter]
        public string WithAppUuid;

        [Parameter]
        public string WithAppName;

        [Parameter]
        public string WithContainerUuid;

        [Parameter]
        public string WithContainerName;

        [Parameter]
        public string WithNamespace;

        [Parameter]
        public string WithRequestId;

        [Parameter]
        public string CartName;

        [Parameter]
        public string ComponentName;

        [Parameter]
        public string WithSoftwareVersion;

        [Parameter]
        public string CartridgeVendor;

        protected override void ProcessRecord()
        {
            ReturnStatus status = new ReturnStatus();
            ApplicationContainer container = new ApplicationContainer(WithAppUuid, WithContainerUuid, null, WithAppName,
                            WithContainerName, WithNamespace, null, null, null);
            try
            {
                status.Output = container.Tidy();
                status.ExitCode = 0;
            }
            catch (Exception ex)
            {
                Logger.Error("Error running oo-tidy command: {0} - {1}", ex.Message, ex.StackTrace);
                status.Output = ex.ToString();
                status.ExitCode = 1;
            }
            this.WriteObject(status);
        }
    }
}
