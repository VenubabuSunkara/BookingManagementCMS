using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateBinder.Interface
{
    public interface ITemplateParse
    {
       Task<string> ParseTemplateAsync(string template, Dictionary<string, string> values);
    }
}
