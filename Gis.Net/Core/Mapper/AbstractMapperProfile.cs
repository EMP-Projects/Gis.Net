using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;

namespace Gis.Net.Core.Mapper;

/// <summary>
/// Provides a mapping profile to map between Data Transfer Objects (DTOs) and Models.
/// </summary>
/// <typeparam name="TDto">The type of the Data Transfer Object.</typeparam>
/// <typeparam name="TModel">The type of the Model.</typeparam>
/// <typeparam name="TRequest"></typeparam>
/// <remarks>
/// This abstract class extends AutoMapper's Profile class and is intended to be used as a base
/// for concrete mapping profiles. It automatically sets up bidirectional mapping between the
/// specified DTO and Model types.
/// </remarks>
public abstract class AbstractMapperProfile<TModel, TDto, TRequest> : Profile
    where TDto : class, IDtoBase
    where TModel : class, IModelBase
    where TRequest : class, IRequestBase
{
    /// <summary>
    /// Gets or sets the mapping expression to map from <typeparamref name="TModel"/>
    /// to <typeparamref name="TDto"/>.
    /// This property is typically configured in the constructor and can be used
    /// to customize the mapping behavior between the source and destination types.
    /// </summary>
    protected IMappingExpression<TModel, TDto> ModelToDtoMapper;
    
    /// <summary>
    /// Gets or sets the mapping expression to map from <typeparamref name="TDto"/>
    /// to <typeparamref name="TModel"/>.
    /// Similar to ModelToDtoMapper, this property is typically set up in the constructor
    /// and allows for detailed configuration of the mapping from the data transfer
    /// object back to the model.
    /// </summary>
    protected IMappingExpression<TDto, TModel> DtoToModelMapper;

    /// <summary>
    /// Represents a mapper for mapping from a request type to a DTO type.
    /// </summary>
    protected IMappingExpression<TRequest, TDto> RequestToDtoMapper;
    
    /// <summary>
    /// Initializes a new instance of the AbstractMapperProfile class.
    /// </summary>
    /// <remarks>
    /// The constructor configures the AutoMapper mapping for the specified DTO and Model types.
    /// The mapping is bidirectional, allowing for mapping from TModel to TDto and from TDto to TModel.
    /// </remarks>
    protected AbstractMapperProfile()
    {
        ModelToDtoMapper = CreateMap<TModel, TDto>();
        DtoToModelMapper = CreateMap<TDto, TModel>();
        RequestToDtoMapper = CreateMap<TRequest, TDto>();

        //DtoToModelMapper.ForAllMembers(o => o.Condition((source, target, sourceMbr, targetMbr) => targetMbr is not null));
        RequestToDtoMapper.ForAllMembers(o => o.Condition((source, target, sourceMbr, targetMbr) => targetMbr is not null));
    }
}