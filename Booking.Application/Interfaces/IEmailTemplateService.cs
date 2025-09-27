using Booking.Application.DTOs;
using Booking.Domain.Entities;

namespace Booking.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        public Task<EmailTemplateTableDto> GetEmailTemplates(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<EmailTemplateDto?> GetEmailTemplateByIdAsync(int TemplateId, CancellationToken token);
        Task<bool> ExistsByNameAsync(string TemplateName, CancellationToken token);
        Task<int> CreateAsync(EmailTemplateDto req, CancellationToken token);
        Task<int> UpdateAsync(EmailTemplateDto role, CancellationToken token);
        Task<int> DeleteAsync(int TemplateId, CancellationToken token);
    }
}
