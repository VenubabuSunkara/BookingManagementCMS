using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageScheduling.Interface
{
    public interface INotification
    {
        string Id { get; }
        string Subject { get; }
        string Body { get; }
        Dictionary<string, object> Metadata { get; }
    }
}
