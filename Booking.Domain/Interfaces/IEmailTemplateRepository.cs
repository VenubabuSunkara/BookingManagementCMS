using Booking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces
{
    public interface IEmailTemplateRepository
    {
        public Task<EmailTemplateTableEntity> GetEmailTemplates(string SearchValue, int Take, int Skip, CancellationToken token);
        Task<EmailTemplateEntity?> GetEmailTemplateByIdAsync(int TemplateId, CancellationToken token);
        Task<bool> ExistsByNameAsync(string TemplateName, CancellationToken token);
        Task<int> CreateAsync(EmailTemplateEntity req, CancellationToken token);
        Task<int> UpdateAsync(EmailTemplateEntity role, CancellationToken token);
        Task<int> DeleteAsync(int TemplateId, CancellationToken token);
    }
}
