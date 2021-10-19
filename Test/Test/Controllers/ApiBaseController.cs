using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ApiBaseController : ControllerBase
    {
    }
}