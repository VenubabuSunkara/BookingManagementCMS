using Entities;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class PramotionServive(BookingManagementCmsContext _context) : IPramotionServive
{
    /// <summary>
    /// Get all pramotions info
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCode>> GetAllPramotinsAsync(CancellationToken cancellationToken)
    {
        return await _context.CouponCodes.AsNoTracking().ToListAsync(cancellationToken);
    }
    /// <summary>
    /// Get single pramotion
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCode?> GetPramotionByIdAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes.FirstOrDefaultAsync(expression, cancellationToken);
    }
    /// <summary>
    /// Create new pramotion
    /// </summary>
    /// <param name="pramotions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CreatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken)
    {
        await _context.CouponCodes.AddAsync(couponCode, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    /// <summary>
    /// Update the pramotion details
    /// </summary>
    /// <param name="pramotions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> UpdatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes
                             .Where(x => x.Id == couponCode.Id)
                             .ExecuteUpdateAsync(pramotion => pramotion
                             .SetProperty(s => s.Name, couponCode.Name ?? string.Empty)
                             .SetProperty(s => s.Code, couponCode.Code ?? string.Empty)
                             .SetProperty(s => s.ValidityFrom, couponCode.ValidityFrom)
                             .SetProperty(s => s.ValidityTo, couponCode.ValidityTo)
                             .SetProperty(s => s.RangeMin, couponCode.RangeMin)
                             .SetProperty(s => s.RangeMax, couponCode.RangeMax)
                             .SetProperty(s => s.UpdatedBy, couponCode.UpdatedBy ?? default)
                             .SetProperty(s => s.UpdatedOn, DateTime.UtcNow)
                             .SetProperty(s => s.MediaUrl, couponCode.MediaUrl ?? string.Empty), cancellationToken) > 0;
    }
    /// <summary>
    /// Delete the pramotion
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool?> DeletePramotionAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.CouponCodes.Where(expression).ExecuteDeleteAsync(cancellationToken) > 0;
    }
}
