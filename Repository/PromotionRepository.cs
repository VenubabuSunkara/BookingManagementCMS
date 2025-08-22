using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class PromotionRepository(IPromotionServive _pramotionServive) : IPromotionRepository
{
    public IQueryable<CouponCode> GetQuarablePramotionData()
    {
        return _pramotionServive.GetQuarablePramotionData();
    }
    /// <summary>
    /// Get all pramotions info
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<CouponCode>> GetAllPramotinsAsync(CancellationToken cancellationToken)
    {
        return await _pramotionServive.GetAllPramotinsAsync(cancellationToken);
    }
    /// <summary>
    /// Get single pramotion
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CouponCode?> GetPramotionByIdAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken)
    {
        return await _pramotionServive.GetPramotionByIdAsync(expression, cancellationToken);
    }
    /// <summary>
    /// Create new pramotion
    /// </summary>
    /// <param name="pramotions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> CreatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken)
    {
        return await _pramotionServive.CreatePramotionAsync(couponCode, cancellationToken);
    }
    /// <summary>
    /// Update the pramotion details
    /// </summary>
    /// <param name="pramotions"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> UpdatePramotionAsync(CouponCode couponCode, CancellationToken cancellationToken)
    {
        return await _pramotionServive.UpdatePramotionAsync(couponCode, cancellationToken);
    }
    /// <summary>
    /// Delete the pramotion
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeletePramotionAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken)
    {
        return await _pramotionServive.DeletePramotionAsync(expression, cancellationToken);
    }
    /// <summary>
    /// Get the promotion status
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> FindPramotionAsync(Expression<Func<CouponCode, bool>> expression, CancellationToken cancellationToken)
    {
        return await _pramotionServive.FindPramotionAsync(expression, cancellationToken);
    }
}
