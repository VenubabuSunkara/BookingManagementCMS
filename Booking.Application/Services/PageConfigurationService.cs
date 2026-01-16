using Booking.Application.DTOs.Pages;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class PageConfigurationService(IPageConfigurationRepository pageConfigurationRepository) : IPageConfigurationService
    {
        private readonly IPageConfigurationRepository _pageConfigurationRepository = pageConfigurationRepository;
        public async Task AddAsync(PageConfigurationDto pageConfiguration, CancellationToken token)
        {
            await _pageConfigurationRepository.AddAsync(PageConfigurationEntity.Create(
                 pageConfiguration.Id,
                 new PageName(pageConfiguration.PageName),
                 new PageContent(pageConfiguration.PageContentData),
                 pageConfiguration.CreatedBy,
                 pageConfiguration.UpdatedBy,
                 pageConfiguration.CreatedOn,
                 pageConfiguration.UpdateOn,
                 pageConfiguration.IsActive,
                 pageConfiguration.ItemGuid,
                 pageConfiguration.Placeholder
                 ), token);
        }

        public async Task DeleteAsync(int id, CancellationToken token)
        {
            await _pageConfigurationRepository.DeleteAsync(id, token);
        }

        public async Task<IEnumerable<PageConfigurationDto>> GetAllAsync(CancellationToken token)
        {
            var pageData = await _pageConfigurationRepository.GetAllAsync(token);
            // Using constructor-based mapping
            return pageData.Select(pc => new PageConfigurationDto(
                Id: pc.Id.Value,
                CreatedOn: pc.CreatedOn,
                UpdateOn: pc.UpdatedOn,
                CreatedBy: pc.CreatedBy,
                UpdatedBy: pc.UpdatedBy,
                ItemGuid: pc.ItemGuid,
                PageName: pc.Name.Value,
                PageContentData: pc.Content.Value,
                IsActive: pc.IsActive,

                Placeholder: pc.Placeholder
            ));
        }

        public async Task<PageConfigurationTableDto> GetAllAsync(int skip, int take, string search, CancellationToken token)
        {
            var pageData = await _pageConfigurationRepository.GetAllAsync(skip, take, search, token);
            return new PageConfigurationTableDto(
                TotalRecords: pageData.TotalRecords,
                FilterRecords: pageData.FilterRecords,
                PageConfigurationDto: [.. pageData.PageConfigurationEntities.Select(pc => new PageConfigurationDto(
                    Id: pc.Id.Value,
                    CreatedOn: pc.CreatedOn,
                    UpdateOn: pc.UpdatedOn,
                    CreatedBy: pc.CreatedBy,
                    UpdatedBy: pc.UpdatedBy,
                    ItemGuid: pc.ItemGuid,
                    PageName: pc.Name.Value,
                    PageContentData: pc.Content.Value,
                    IsActive: pc.IsActive,
                    Placeholder: pc.Placeholder
                ))]
            );
        }

        public async Task<PageConfigurationDto?> GetByIdAsync(int id, CancellationToken token)
        {
            var pageContentrecord = await _pageConfigurationRepository.GetByIdAsync(id, token);
            if (pageContentrecord == null) return null;
            return new PageConfigurationDto(pageContentrecord.Id.Value, pageContentrecord.CreatedOn, pageContentrecord.UpdatedOn,
                pageContentrecord.CreatedBy, pageContentrecord.UpdatedBy, pageContentrecord.ItemGuid, pageContentrecord.Name.Value,
                pageContentrecord.Content.Value, pageContentrecord.IsActive, pageContentrecord.Placeholder);
        }

        public async Task UpdateAsync(PageConfigurationDto pageConfiguration, CancellationToken token)
        {
            await _pageConfigurationRepository.UpdateAsync(PageConfigurationEntity.Create(
                pageConfiguration.Id,
                new PageName(pageConfiguration.PageName),
                new PageContent(pageConfiguration.PageContentData),
                pageConfiguration.CreatedBy,
                pageConfiguration.UpdatedBy,
                pageConfiguration.CreatedOn,
                pageConfiguration.UpdateOn,
                pageConfiguration.IsActive,
                pageConfiguration.ItemGuid,
                pageConfiguration.Placeholder
                ), token);
        }
    }
}
