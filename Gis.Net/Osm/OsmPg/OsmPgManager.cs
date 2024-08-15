using Gis.Net.Core.Entities;
using Gis.Net.Osm.OsmPg;
using Gis.Net.Osm.OsmPg.Properties;
using Gis.Net.Osm.OsmPg.Vector;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetTopologySuite.IO.Converters;

namespace Gis.Net.Osm.OsmPg;

public static class OsmPgManager
{
    public static WebApplicationBuilder AddOsmPostGis<TContextDb>(
        this WebApplicationBuilder builder, 
        ConnectionPgSql connection)
        where TContextDb: DbContext, IOsmDbContext
    {
        
        builder.Services.AddPostGis<Osm2PgsqlDbContext>(connection, 
            builder.Environment.ApplicationName,
            builder.Environment.IsDevelopment());
        
        builder.Services.AddOsmPostGis<Osm2PgsqlDbContext, TContextDb>();
        return builder;
    }
    
    /// <summary>
    /// Configuring Serialize Features Collection
    /// 
    /// Solve Error: "Deserialization of reference types without parameterless constructor is not supported"
    /// when deserialize payload controller with NetTopology Geometries library 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddControllersToFeatureCollection(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options => {
            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new FeatureConverter());
            options.SerializerSettings.Converters.Add(new GeometryConverter());
            options.SerializerSettings.Converters.Add(new FeatureCollectionConverter());  
        });
            
        return services;
    }

    public static IServiceCollection AddOsmPostGis<TContextOsm, TContextDb>(this IServiceCollection services)
        where TContextOsm: DbContext, IOsm2PgsqlDbContext
        where TContextDb: DbContext, IOsmDbContext
    {
        services.AddControllersToFeatureCollection();

        services.AddScoped(typeof(IOsmPg<,>), typeof(OsmPg<,>));
        services.AddScoped<IOsmPgService, OsmService<TContextOsm>>();
        services.AddScoped<OsmPropertiesRepository<TContextDb>>();
        services.AddScoped<OsmPropertiesService<TContextDb>>();
        services.AddScoped<OsmVectorRepository<TContextDb>>();
        services.AddScoped<OsmVectorService<TContextDb>>();

        services.AddAutoMapper(
            typeof(OsmVectorMapper),
            typeof(OsmPropertiesMapper));

        return services;
    }
}