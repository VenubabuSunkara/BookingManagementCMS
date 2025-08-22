using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities;

public class DataTableRequestEntity
{
    public int draw { get; set; }
    public int start { get; set; }
    public int length { get; set; }
    public Search search { get; set; } = new();
    public List<Order> order { get; set; } = [];
    public List<Column> columns { get; set; } = [];

    public class Search
    {
        public string value { get; set; } = string.Empty;
        public string regex { get; set; } = string.Empty;
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; } = "desc";
    }

    public class Column
    {
        public string data { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; } = new();
    }
}
