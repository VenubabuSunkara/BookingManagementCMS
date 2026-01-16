using Booking.Domain.Entities;
using Booking.Domain.Interfaces;
using Booking.Infrastructure.Data;
using Booking.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Booking.Infrastructure.Repositories
{
    public class EmailTemplateRepository(BookingCmsContext context, IMemoryCache cache) : IEmailTemplateRepository
    {
        private readonly BookingCmsContext _context = context;
        private readonly IMemoryCache _cache = cache;

        public async Task<int> CreateAsync(EmailTemplateEntity req, CancellationToken token)
        {
            await _context.EmailTemplates.AddAsync(new EmailTemplate()
            {
                IsEnabled = req.IsEnabled,
                EmailSubject = req.EmailSubject,
                EmailBody = req.EmailBody,
                Name = req.Name,
                CreatedBy = req.CreatedBy,
                CreatedOn = req.CreatedOn,
                UpdatedBy = req.UpdatedBy,
                UpdatedOn = req.UpdatedOn,
            }, token);
            // Save and return new Id
            return await _context.SaveChangesAsync(token);
        }

        public async Task<int> DeleteAsync(int TemplateId, CancellationToken token)
        {
            return await _context.EmailTemplates.Where(x => x.Id == TemplateId).ExecuteDeleteAsync(token);
        }

        public async Task<bool> ExistsByNameAsync(string TemplateName, CancellationToken token)
        {
            return await _context.EmailTemplates.AsNoTracking().AnyAsync(x => x.Name == TemplateName, cancellationToken: token);
        }

        public async Task<EmailTemplateEntity?> GetEmailTemplateByIdAsync(int TemplateId, CancellationToken token)
        {
            return await _context.EmailTemplates.AsNoTracking().Select(x => new EmailTemplateEntity()
            {
                Id = x.Id,
                Name = x.Name,
                EmailSubject = x.EmailSubject,
                IsEnabled = x.IsEnabled,
                CreatedOn = x.CreatedOn,
                EmailBody = x.EmailBody
            }).FirstOrDefaultAsync(x => x.Id == TemplateId, token);
        }

        public async Task<EmailTemplateTableEntity> GetEmailTemplates(string SearchValue, int Take, int Skip, CancellationToken token)
        {
            var q = _context.EmailTemplates.AsNoTracking();
            var total = await q.CountAsync(token);

            if (!string.IsNullOrWhiteSpace(SearchValue))
            {
                q = q.Where(d => EF.Functions.Like(d.EmailSubject, $"%{SearchValue}%"));
            }
            int filtered = await q.CountAsync(token);

            // simple order by FullName default
            q = q.OrderByDescending(d => d.CreatedOn);
            var page = await q.Skip(Skip).Take(Take)
                .Select(d => new EmailTemplateEntity
                {
                    Id = d.Id,
                    Name = d.Name,
                    EmailSubject = d.EmailSubject,
                    EmailBody = d.EmailBody,
                    CreatedOn = d.CreatedOn,
                    IsEnabled = d.IsEnabled
                }).ToListAsync(token);

            var response = new EmailTemplateTableEntity
            {
                TotalRecords = total,
                FilterRecords = filtered,
                EmailEntities = [.. page]
            };
            return response;
        }

        public async Task<int> UpdateAsync(EmailTemplateEntity emailTempalte, CancellationToken token)
        {
            return await _context.EmailTemplates
           .Where(x => x.Id == emailTempalte.Id)
           .ExecuteUpdateAsync(s => s
               .SetProperty(t => t.Name, emailTempalte.Name)
               .SetProperty(t => t.EmailSubject, emailTempalte.EmailSubject)
               .SetProperty(t => t.EmailBody, emailTempalte.EmailBody)
               .SetProperty(t => t.IsEnabled, emailTempalte.IsEnabled)
               .SetProperty(t => t.UpdatedOn, emailTempalte.UpdatedOn)
               .SetProperty(t => t.UpdatedBy, emailTempalte.UpdatedBy), token);
        }
    }
}
