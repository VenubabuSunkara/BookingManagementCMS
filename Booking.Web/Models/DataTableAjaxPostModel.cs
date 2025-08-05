namespace Booking.Web.Models
{
    public class DataTableAjaxPostModel
    {

        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; } = [];
        public Search search { get; set; } = new Search();
        public List<Order> order { get; set; } = [];
    }

    public class Column
    {
        public string data { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public bool? searchable { get; set; }
        public bool? orderable { get; set; }
        public Search search { get; set; } = new Search();
    }

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

}
