using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repositories
{
    public class PageConfigurationRepository(BookingCmsContext context) : IPageConfigurationRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task AddAsync(PageConfiguration pageConfiguration, CancellationToken token)
        {
            var pageContent = new Booking.Infrastructure.Data.Models.PageContent()
            {
                PageName = pageConfiguration.Name.Value,
                PageContentData = pageConfiguration.Content.Value,
                CreatedOn = pageConfiguration.CreatedOn,
                UpdatedOn = pageConfiguration.UpdatedOn,
                CreateBy = pageConfiguration.CreatedBy,
                UpdatedBy = pageConfiguration.UpdatedBy,
                IsActive = pageConfiguration.IsActive,
                Placeholder = pageConfiguration.Placeholder,
                ItemGuid = Guid.NewGuid()
            };
            await _context.PageContents.AddAsync(pageContent, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await _context.TourPackages.Where(u => u.ItemId == id).ExecuteDeleteAsync(token);
        }

        public Task<IEnumerable<PageConfiguration>> GetAllAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<PageConfiguration?> GetByIdAsync(int id, CancellationToken token)
        {
            var pageConfigData = await _context.TourPackages.FindAsync(u => u.ItemId == id);
            return PageConfiguration.Create(
             new PageName(pageConfigData.Name.Value),
             new Domain.Entities.PageContent(pageConfigData.Content.Value),
             pageConfigData.CreatedBy,
             pageConfigData.CreatedOn,
             pageConfigData.IsActive,
             pageConfigData.Placeholder
         );
        }


        public Task UpdateAsync(PageConfiguration pageConfiguration, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
