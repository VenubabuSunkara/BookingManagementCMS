using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Booking.Infrastructure.Repositories
{
    public class PageConfigurationRepository(BookingCmsContext context) : IPageConfigurationRepository
    {
        private readonly BookingCmsContext _context = context;
        public async Task AddAsync(PageConfigurationEntity pageConfiguration, CancellationToken token)
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

        public async Task<IEnumerable<PageConfigurationEntity>> GetAllAsync(CancellationToken token)
        {
            var pageConfigDataList = await _context.PageContents.ToListAsync(token);
            return [.. pageConfigDataList.Select(pageConfigData =>
            PageConfigurationEntity.Create(
                pageConfigData.Id,
                new PageName(pageConfigData.PageName),
                new PageContent(pageConfigData.PageContentData),
                pageConfigData.CreateBy,
                pageConfigData.UpdatedBy,
                pageConfigData.CreatedOn,
                pageConfigData.UpdatedOn,
                pageConfigData.IsActive,
                pageConfigData.ItemGuid,
                pageConfigData.Placeholder
            ))];
        }

        public async Task<PageConfigurationTableEntity> GetAllAsync(int skip, int take, string search, CancellationToken token)
        {
            var q = _context.PageContents.AsNoTracking();
            var total = await q.CountAsync(token);
            int filtered = total;
            if (!string.IsNullOrWhiteSpace(search))
            {
                q = q.Where(d =>
                            d.PageName.Contains(search) ||
                            d.PageContentData.Contains(search)
                );
                filtered = await q.CountAsync(token);
            }
            q = q.OrderByDescending(d => d.CreatedOn);
            var pageConfigDataList = await q.Skip(skip).Take(take).ToListAsync(token);

            return new PageConfigurationTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                PageConfigurationEntities = [.. pageConfigDataList.Select(pageConfigData =>
                PageConfigurationEntity.Create(
                    pageConfigData.Id,
                    new PageName(pageConfigData.PageName),
                    new PageContent(pageConfigData.PageContentData),
                    pageConfigData.CreateBy,
                    pageConfigData.UpdatedBy,
                    pageConfigData.CreatedOn,
                    pageConfigData.UpdatedOn,
                    pageConfigData.IsActive,
                    pageConfigData.ItemGuid,
                    pageConfigData.Placeholder
                )
            )]
            };
        }

        public async Task<PageConfigurationEntity?> GetByIdAsync(int id, CancellationToken token)
        {
            var pageConfigData = await _context.PageContents.FirstOrDefaultAsync(x => x.Id == id, token);
            if (pageConfigData == null) return null;
            return PageConfigurationEntity.Create(
                pageConfigData.Id,
                new PageName(pageConfigData.PageName),
                new PageContent(pageConfigData.PageContentData),
                pageConfigData.CreateBy,
                pageConfigData.UpdatedBy,
                pageConfigData.CreatedOn,
                pageConfigData.UpdatedOn,
                pageConfigData.IsActive,
                pageConfigData.ItemGuid,
                pageConfigData.Placeholder);
        }


        public async Task UpdateAsync(PageConfigurationEntity pageConfiguration, CancellationToken token)
        {
           await _context.PageContents.Where(x => x.Id == pageConfiguration.Id.Value)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(pc => pc.PageName, pageConfiguration.Name.Value)
                    .SetProperty(pc => pc.PageContentData, pageConfiguration.Content.Value)
                    .SetProperty(pc => pc.UpdatedBy, pageConfiguration.UpdatedBy)
                    .SetProperty(pc => pc.UpdatedOn, pageConfiguration.UpdatedOn)
                    .SetProperty(pc => pc.IsActive, pageConfiguration.IsActive)
                    .SetProperty(pc => pc.Placeholder, pageConfiguration.Placeholder)
                , token);
        }
    }
}
