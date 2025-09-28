using Booking.Application.DTOs;
using Booking.Application.Interfaces;
using Booking.Domain.Entities;
using Booking.Domain.Interfaces;

namespace Booking.Application.Services
{
    public class EmailTemplateService(IEmailTemplateRepository templateRepository) : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _templateRepository = templateRepository;

        public async Task<int> CreateAsync(EmailTemplateDto req, CancellationToken token)
        {
            return await _templateRepository.CreateAsync(new EmailTemplateEntity()
            {
                EmailBody = req.EmailBody,
                EmailSubject = req.EmailSubject,
                IsEnabled = req.IsEnabled,
                Name = req.Name,
                CreatedBy = req.CreatedBy,
                CreatedOn = req.CreatedOn,
                UpdatedBy = req.UpdatedBy,
                UpdatedOn = req.UpdatedOn,
            }, token);
        }

        public async Task<int> DeleteAsync(int TemplateId, CancellationToken token)
        {
            return await _templateRepository.DeleteAsync(TemplateId, token);
        }

        public async Task<bool> ExistsByNameAsync(string TemplateName, CancellationToken token)
        {
            return await _templateRepository.ExistsByNameAsync(TemplateName, token);
        }

        public async Task<EmailTemplateDto?> GetEmailTemplateByIdAsync(int TemplateId, CancellationToken token)
        {
            var template = await _templateRepository.GetEmailTemplateByIdAsync(TemplateId, token);
            if (template == null) return null;
            return new EmailTemplateDto()
            {
                CreatedOn = template.CreatedOn,
                EmailBody = template.EmailBody,
                EmailSubject = template.EmailSubject,
                IsEnabled = template.IsEnabled,
                Name = template.Name,
                Id = template.Id,
            };
        }

        public async Task<EmailTemplateTableDto> GetEmailTemplates(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var templates = await _templateRepository.GetEmailTemplates(SearchValue, Take, Skip, token);
            return new EmailTemplateTableDto()
            {
                TotalRecords = templates.TotalRecords,
                FilterRecords = templates.FilterRecords,
                EmailTemplatesDto = [.. templates.EmailEntities.Select(x => new EmailTemplateDto()
                {
                    Id=x.Id,
                    CreatedOn=x.CreatedOn,
                    EmailBody=x.EmailBody,
                    Name=x.Name,
                    EmailSubject=x.EmailSubject,
                    IsEnabled=x.IsEnabled,
                    CreatedBy=x.CreatedBy
                })]
            };
        }

        public async Task<int> UpdateAsync(EmailTemplateDto role, CancellationToken token)
        {
            return await _templateRepository.UpdateAsync(new EmailTemplateEntity()
            {
                EmailBody = role.EmailBody,
                EmailSubject = role.EmailSubject,
                Name = role.Name,
                Id = role.Id,
                IsEnabled = role.IsEnabled,
                UpdatedOn = role.UpdatedOn,
                UpdatedBy = role.UpdatedBy,
            }, token);
        }
    }
}
