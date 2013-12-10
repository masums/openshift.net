﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Uhuru.Openshift.Runtime;

namespace Uhuru.Openshift.Cmdlets
{
    [Cmdlet("OO", "Restart")]
    class OO_Restart : System.Management.Automation.Cmdlet 
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

        [Parameter]
        public SwitchParameter All;

        [Parameter]
        public SwitchParameter ParallelConcurrencyRatio;

        protected override void ProcessRecord()
        {
            ApplicationContainer container = new ApplicationContainer(WithAppUuid, WithContainerUuid, null, WithAppName,
                WithContainerName, WithNamespace, null, null, null);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options["all"] = All;
            options["parallelConcurrencyRatio"] = ParallelConcurrencyRatio;
            try
            {
                this.WriteObject(container.Restart(CartName, options));
            }
            catch (Exception ex)
            {
                this.WriteObject(ex.ToString());
            }
        }
    }
}
