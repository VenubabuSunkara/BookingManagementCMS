using Scriban;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateBinder.Interface;

namespace TemplateBinder.Services
{
    public class TemplateParse : ITemplateParse
    {
        // Plan (pseudocode):
        // 1. Validate input parameters (template string and values dictionary).
        // 2. Create a new Scriban TemplateContext.
        // 3. Create a Scriban.Runtime.ScriptObject and populate it with key/value pairs from the dictionary.
        // 4. Push the ScriptObject as the global scope into the TemplateContext.
        // 5. Parse the template string using Scriban.Template.Parse.
        // 6. If parsing produced errors, aggregate and throw an InvalidOperationException with details.
        // 7. Render the parsed template asynchronously with the prepared TemplateContext and return the result.

        public async Task<string> ParseTemplateAsync(string template, Dictionary<string, string> values)
        {
            if (template is null) throw new ArgumentNullException(nameof(template));
            if (values is null) throw new ArgumentNullException(nameof(values));

            var templateContext = new TemplateContext();

            var scriptObject = new ScriptObject();
            foreach (var kv in values)
            {
                // Scriban keys must be valid identifiers; assuming caller provides usable keys.
                scriptObject.Add(kv.Key, kv.Value);
            }

            templateContext.PushGlobal(scriptObject);

            var parsedTemplate = Template.Parse(template);
            if (parsedTemplate.HasErrors)
            {
                var messages = string.Join("; ", parsedTemplate.Messages.Select(m => m.ToString()));
                throw new InvalidOperationException($"Template parsing failed: {messages}");
            }

            return await parsedTemplate.RenderAsync(templateContext);
        }
    }
}
