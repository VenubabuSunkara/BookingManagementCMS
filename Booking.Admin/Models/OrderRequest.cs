namespace Booking.Web.Models
{
    public class OrderRequest
    {
        public int DriverId { get; set; }
        public int VehicleId { get; set; }
        public string OrderStatus { get; set; } = "InProgress";
        public DataTableAjaxPostModel SearchModel { get; set; } = new DataTableAjaxPostModel();
    }
}
