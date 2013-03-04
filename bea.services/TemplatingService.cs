using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace Bea.Services
{
    public class TemplatingService : ITemplatingService
    {
        private readonly VelocityEngine _engine;

        public TemplatingService(String templatesPath)
        {
            _engine = new VelocityEngine();
            _engine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templatesPath);
            _engine.Init();
        }

        public String GetTemplatedDocument(String templateName, IDictionary<String, String> data, IDictionary<String, object[]> list = null)
        {
            Template template = _engine.GetTemplate(templateName);

            VelocityContext context = new VelocityContext();
            data.Keys.ToList().ForEach(k => context.Put(k, data[k]));

            if (list != null)
                context.Put(list.Keys.FirstOrDefault(), list.Values.FirstOrDefault());

            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            
            return writer.ToString();
        }
    }
}
