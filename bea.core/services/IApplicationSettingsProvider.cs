using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.Services
{
    public interface IApplicationSettingsProvider
    {
        String WebsiteAddress { get; }
        String WebsiteName { get; }
        String FromEmailAddress { get; }
        String NoReplyEmailAddress { get; }
        bool RebuildSchema { get; }
        String TemplatesPath { get; }
    }
}
