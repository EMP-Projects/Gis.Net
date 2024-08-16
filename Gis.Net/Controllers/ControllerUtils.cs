using Gis.Net.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Gis.Net.Controllers;

public class ControllerUtils : ControllerBase
{
    protected IActionResult ArrayResult<T>(IEnumerable<T> rows) where T : IDtoBase => Ok(new ArrayResult<T>(rows));
    protected IActionResult ArrayResultError<T>(string error) where T : IDtoBase => BadRequest(new ArrayResult<T>(error));
    protected IActionResult SingleResult<T>(T result) where T : IDtoBase => Ok(new SingleResult<T?>(result));
    protected IActionResult SingleResultWithError<T>(string error) where T : IDtoBase => Ok(new SingleResult<T>(error));

    protected IActionResult GenericResult(string key, object value)
    {
        GenericResult result = new();
        result.Result.Add(key, value);
        return Ok(result);
    }

    protected IActionResult GenericResult(string[] keys, object[] values)
    {
        GenericResult result = new();
        for (var k = 0; k < keys.Length; k++)
            result.Result.Add(keys[k], values[k]);
        return Ok(result);
    }
}