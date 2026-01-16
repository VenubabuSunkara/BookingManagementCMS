using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.DTOs.Pages
{
    public class PageConfigurationTableDto
    {
        public int TotalRecords { get; private set; }
        public int FilterRecords { get; private set; }
        public List<PageConfigurationDto> PageConfigurationDto { get; private set; }
        public PageConfigurationTableDto(int TotalRecords, int FilterRecords,  List<PageConfigurationDto> PageConfigurationDto)
        {
            this.TotalRecords = TotalRecords;
            this.FilterRecords = FilterRecords;
            this.PageConfigurationDto = PageConfigurationDto;
        }
    }
    public record PageConfigurationDto : BaseEntityDto
    {
        public string PageName { get; init; } = null!;
        public string PageContentData { get; init; } = null!;
        public bool IsActive { get; init; }
        public string Placeholder { get; init; } = null!;
        public PageConfigurationDto(int Id, DateTime CreatedOn, DateTime UpdateOn, string CreatedBy, string UpdatedBy, Guid ItemGuid,
            string PageName, string PageContentData, bool IsActive, string Placeholder)
            : base(Id, CreatedOn, UpdateOn, CreatedBy, UpdatedBy, ItemGuid)
        {
            this.PageName = PageName;
            this.PageContentData = PageContentData;
            this.IsActive = IsActive;
            this.Placeholder = Placeholder;
        }
    }
}
