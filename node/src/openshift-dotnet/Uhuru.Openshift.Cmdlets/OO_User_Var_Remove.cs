﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Uhuru.Openshift.Runtime;

namespace Uhuru.Openshift.Cmdlets
{
    [Cmdlet("OO", "User-Var-Remove")]
    public class OO_User_Var_Remove : System.Management.Automation.Cmdlet 
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
        public string WithKeys;

        [Parameter]
        public string WithGears;

        protected override void ProcessRecord()
        {
            ReturnStatus status = new ReturnStatus();

            ApplicationContainer container = new ApplicationContainer(WithAppUuid, WithContainerUuid, null, WithAppName,
             WithContainerName, WithNamespace, null, null, null);
            try
            {
                List<string> gears = null;
                List<string> keys = WithKeys.Split(' ').ToList();
                if (!string.IsNullOrEmpty(WithGears))
                {
                    gears = new List<string>();
                    JArray gearsObj = (JArray)JsonConvert.DeserializeObject(WithGears);
                    foreach (var gearObj in gearsObj)
                    {
                        gears.Add(gearObj.ToString());
                    }
                }

                status.Output = container.UserVarRemove(keys,gears);
                status.ExitCode = 0;
            }
            catch (Exception ex)
            {
                Logger.Error("Error running oo-user-var-remove command: {0} - {1}", ex.Message, ex.StackTrace);
                status.Output = ex.ToString();
                status.ExitCode = 1;
            }
            this.WriteObject(status);
        }
    }
}
