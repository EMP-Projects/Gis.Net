using AutoMapper;
using Gis.Net.Core.DTO;
using Gis.Net.Core.Entities;
using Gis.Net.Core.Exceptions;
using Gis.Net.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Gis.Net.Controllers;

public abstract class RootController<TModel, TDto, TQuery, TRequest, TContext> :
    RootReadOnlyController<TModel, TDto, TQuery, TRequest, TContext>
    where TModel : ModelBase
    where TDto : DtoBase
    where TQuery : QueryBase
    where TRequest : RequestBase
    where TContext : DbContext
{
    /// <inheritdoc />
    protected RootController(
        ILogger logger, 
        IConfiguration configuration, 
        IMapper mapper, 
        IServiceCore<TModel, TDto, TQuery, TRequest, TContext> service) : 
        base(logger, configuration, mapper, service)
    {
        
    }
    
    /// <summary>
    /// Update Record
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody] TRequest payload)
    {
        try
        {
            // per prima cosa gestisco l'eventuale 
            await ServiceCore.ValidateRequest(payload, ECrudActions.Insert);
            var body = Mapper.Map<TDto>(payload);
            var model = await ServiceCore.Insert(body);
            await ServiceCore.SaveContext(model!, ECrudActions.Insert);
            return SingleResult(body);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<TDto>(ex.Message);
        }
    }

    /// <summary>
    /// Update record
    /// </summary>
    /// <param name="dto">The resulting DTO, mapped from request</param>
    /// <param name="request">The request received by the client</param>
    /// <returns></returns>
    protected async Task<IActionResult> Update(TDto dto, TRequest request)
    {
        try
        {
            await ServiceCore.ValidateRequest(request, ECrudActions.Update);
            var model = await ServiceCore.Update(dto);
            var result = await ServiceCore.SaveContext(model!, ECrudActions.Update);
            return SingleResult(dto);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<TDto>(ex.Message);
        }
    }

    /// <summary>
    /// Modify record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="payload"></param>
    /// <returns></returns>
    [HttpPut("{id:long}")]
    public virtual async Task<IActionResult> Put(long id, [FromBody] TRequest payload)
    {
        try
        {
            await ServiceCore.ValidateRequest(payload, ECrudActions.Update);
            var body = Mapper.Map<TDto>(payload);
            body.Id = id;
            var model = await ServiceCore.Update(body);
            await ServiceCore.SaveContext(model!, ECrudActions.Update);
            return SingleResult(body);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return SingleResultWithError<TDto>(ex.Message);
        }
    }

    /// <summary>
    /// Endpoint for PATCH.
    /// </summary>
    /// <remarks>
    /// <para>With PATCH, the API changes only the fields wich value is not null</para>
    /// </remarks>
    /// <param name="id">id of the record that must be changed</param>
    /// <param name="payload">payload with only fields that need to be changed</param>
    /// <returns></returns>
    [HttpPatch("{id:long}")]
    public virtual async Task<IActionResult> Patch(long id, [FromBody] TRequest payload)
    {
        try
        {
            await ServiceCore.ValidateRequest(payload, ECrudActions.Update);
            var currentDto = await ServiceCore.Find(id);
            var newDto = Mapper.Map(payload, currentDto);
            newDto.Id = id;
            var model = await ServiceCore.Update(newDto);
            if (model is null)
                throw new InvalidParameter(nameof(id));
            await ServiceCore.SaveContext(model, ECrudActions.Update);
            return SingleResult(newDto);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete record By Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidParameter"></exception>
    [HttpDelete("{id:long}")]
    public virtual async Task<IActionResult> Delete(long id)
    {
        try
        {
            if (id == 0) throw new InvalidParameter(nameof(id));
            var model = await ServiceCore.Delete(id);
            await ServiceCore.SaveContext(model, ECrudActions.Delete);
            return Ok("Record cancellato con successo");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}