using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Bea.Core.Services;

namespace Bea.Web.Providers
{
    public class ApplicationSettingsProvider : IApplicationSettingsProvider
    {
        public string WebsiteAddress
        {
            get { return WebConfigurationManager.AppSettings["websiteAddress"]; }
        }

        public string WebsiteName
        {
            get { return WebConfigurationManager.AppSettings["websiteName"]; }
        }

        public string FromEmailAddress
        {
            get { return WebConfigurationManager.AppSettings["fromEmailAddress"]; }
        }

        public string NoReplyEmailAddress
        {
            get { return WebConfigurationManager.AppSettings["noReplyEmailAddress"]; }
        }

        public bool RebuildSchema
        {
            get { return WebConfigurationManager.AppSettings["rebuildSchema"] == null ? false : bool.Parse(WebConfigurationManager.AppSettings["rebuildSchema"]); }
        }

        public string TemplatesPath
        {
            get { return WebConfigurationManager.AppSettings["templatesPath"]; }
        }
    }
}