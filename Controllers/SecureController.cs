using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogApi.Controllers;

public class SecureController : ApiControllerBase
{
    [HttpGet]
    [Authorize]
    [Route("Secure")]
    public ActionResult checkAuthorize()
    {
        return Ok("Your authorized");
    }
}