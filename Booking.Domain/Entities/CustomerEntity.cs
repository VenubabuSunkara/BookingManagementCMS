using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class CustomerDTableEntity
    {
        public int Total { get; set; }
        public int Filtered { get; set; }
        public IEnumerable<CustomerEntity> CustomerEntities { get; set; } = [];

    }

    public class CustomerEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public ICollection<BookingOrder> BookingOrders { get; set; } = [];
        //public ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
        //public ICollection<CustomerRelative> CustomerRelatives { get; set; } = new List<CustomerRelative>();

    }
}
