using Booking.Application.DTOs.Tour;
using Booking.Domain.Entities;
using Booking.Domain.Entities.Tour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Interfaces
{
    public interface IPackageService
    {
        Task<PackageDataTableDto> GetPackages(int Skip, int Take, string searchKey = "", int CategoryId = 0);
        Task<int> SavePackage(TourPackageDto tourPackage, CancellationToken token);

    }
}
