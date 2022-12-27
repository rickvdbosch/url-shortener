using System.Net;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using RvdB.UrlShortener.API.Entities;

namespace RvdB.UrlShortener.API
{
    public class Redirect
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public Redirect(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<Redirect>();
            _configuration = configuration;
        }

        #endregion

        [Function("Redirect")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{shortcode}")] HttpRequestData req,
            [TableInput("redirects", "redir", "{shortcode}")] RedirectEntity redirectEntity)
        {
            var redir = _configuration.GetValue<string>(Constants.SETTING_BASEURL);

            if (redirectEntity != null)
            {
                redir = redirectEntity.RedirectUrl;
            }

            var response = req.CreateResponse(HttpStatusCode.Redirect);
            response.Headers.Add("Location", redir);

            return response;
        }
    }
}
