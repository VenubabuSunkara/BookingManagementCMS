using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces;

public interface IPramotionRepository
{
    Task<IEnumerable<CouponCode>> GetAllPramotinsAsync(CancellationToken cancellationToken);
    Task<CouponCode?> GetPramotionByIdAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken);
    Task<bool> CreatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken);
    Task<bool> UpdatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken);
    Task<bool?> DeletePramotionAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken);
}
