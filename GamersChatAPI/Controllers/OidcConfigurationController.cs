using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace GamersChatAPI.Controllers
{
    public class OidcConfigurationController : Controller
    {
        private readonly ILogger<OidcConfigurationController> _logger;

        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider,
            ILogger<OidcConfigurationController> logger)
        {
            _logger = logger;
            ClientRequestPrametersProvider = clientRequestParametersProvider;
        }

        public IClientRequestParametersProvider ClientRequestPrametersProvider { get; }
        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            var parameters = ClientRequestPrametersProvider.GetClientParameters(HttpContext, clientId);
            return Ok(parameters);
        }
    }
}
