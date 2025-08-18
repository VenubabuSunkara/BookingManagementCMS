using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<PackageDTable> SearchPackage(int Skip, int Take, string searchKey = "");
    }
}
