using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IPackageRepository
    {
        Task<IEnumerable<TourPackageCategoryEntity>> GetTourPackageCategory();
    }
}
