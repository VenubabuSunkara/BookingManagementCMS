using Booking.Application.DTOs;
using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Mappings
{
    public class DriverMapping
    {
        public static DriverDto ToDomain(Booking.Domain.Entities.Driver entity) =>
        new Booking.Application.DTOs.DriverDto
        {
            Id = entity.Id
        };

        public static Booking.Domain.Entities.Driver ToEntity(Booking.Infrastructure.Data.Models.Driver domain) =>
        new Booking.Domain.Entities.Driver
        {
            Id = domain.Id
        };
    }
}
