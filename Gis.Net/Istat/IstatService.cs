using Gis.Net.Istat.Models;
using Microsoft.EntityFrameworkCore;

namespace Gis.Net.Istat;

/// <inheritdoc />
public class IstatService<TContext> : IIStatService<TContext> where TContext : DbContext, IStatDbContext
{
    private readonly ILimits<TContext, LimitsItRegion> _region;
    private readonly ILimits<TContext, LimitsItProvince> _province;
    private readonly ILimits<TContext, LimitsItMunicipality> _municipality;

    /// <summary>
    /// Initializes a new instance of the <see cref="IstatService{TContext}"/> class.
    /// </summary>
    /// <param name="region">The region limits service.</param>
    /// <param name="province">The province limits service.</param>
    /// <param name="municipality">The municipality limits service.</param>
    public IstatService(
        ILimits<TContext, LimitsItRegion> region, 
        ILimits<TContext, LimitsItProvince> province, 
        ILimits<TContext, LimitsItMunicipality> municipality)
    {
        _region = region;
        _province = province;
        _municipality = municipality;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LimitsItRegion>> GetRegions(LimitsItRegion? queryParams)
    {
        return await _region.List(new IstatLimitOptions<LimitsItRegion>
        {
            OnBeforeQuery = query => 
            {
                if (queryParams == null) return query;
                if (queryParams.RegName != null) query = query.Where(x => x.RegName == queryParams.RegName);
                if (queryParams.RegIstatCode != null) query = query.Where(x => x.RegIstatCode == queryParams.RegIstatCode);
                if (queryParams.RegIstatCodeNum != null) query = query.Where(x => x.RegIstatCodeNum == queryParams.RegIstatCodeNum);
                if (queryParams.WkbGeometry != null) query = query.Where(x => x.WkbGeometry != null && x.WkbGeometry.Intersects(queryParams.WkbGeometry));
                return query;
            }
        });
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LimitsItProvince>> GetProvince(LimitsItProvince? queryParams)
    {
        return await _province.List(new IstatLimitOptions<LimitsItProvince>
        {
            OnBeforeQuery = query =>
            {
                if (queryParams == null) return query;
                if (queryParams.ProvName != null) query = query.Where(x => x.ProvName == queryParams.ProvName);
                if (queryParams.ProvIstatCode != null) query = query.Where(x => x.ProvIstatCode == queryParams.ProvIstatCode);
                if (queryParams.ProvIstatCodeNum != null) query = query.Where(x => x.ProvIstatCodeNum == queryParams.ProvIstatCodeNum);
                if (queryParams.WkbGeometry != null) query = query.Where(x => x.WkbGeometry != null && x.WkbGeometry.Intersects(queryParams.WkbGeometry));
                return query;
            }
        });
    }

    /// <summary>
    /// Retrieves a list of municipalities based on the specified query parameters.
    /// </summary>
    /// <param name="queryParams">The query parameters to filter the municipalities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of municipalities.</returns>
    public async Task<IEnumerable<LimitsItMunicipality>> GetMunicipalities(LimitsItMunicipality? queryParams)
    {
        return await _municipality.List(new IstatLimitOptions<LimitsItMunicipality>
        {
            OnBeforeQuery = query =>
            {
                if (queryParams == null) return query;
                if (queryParams.Name != null) query = query.Where(x => x.Name == queryParams.Name);
                if (queryParams.WkbGeometry != null) query = query.Where(x => x.WkbGeometry != null && x.WkbGeometry.Intersects(queryParams.WkbGeometry));
                if (queryParams.ComCatastoCode != null) query = query.Where(x => x.ComCatastoCode == queryParams.ComCatastoCode);
                if (queryParams.ComIstatCode != null) query = query.Where(x => x.ComIstatCode == queryParams.ComIstatCode);
                if (queryParams.ComIstatCodeNum != null) query = query.Where(x => x.ComIstatCodeNum == queryParams.ComIstatCodeNum);
                if (queryParams.NameDe != null) query = query.Where(x => x.NameDe == queryParams.NameDe);
                if (queryParams.NameSl != null) query = query.Where(x => x.NameSl == queryParams.NameSl);
                if (queryParams.NameIt != null) query = query.Where(x => x.NameIt == queryParams.NameIt);
                if (queryParams.MinintElettorale != null) query = query.Where(x => x.MinintElettorale == queryParams.MinintElettorale);
                if (queryParams.MinintFinloc != null) query = query.Where(x => x.MinintFinloc == queryParams.MinintFinloc);
                return query;
            }
        });
    }
}