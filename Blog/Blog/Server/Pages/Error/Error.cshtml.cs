using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Server.Pages.Error
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        private readonly ILogger _logger;

        public ErrorModel(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("EventsLogger");
        }

        public void OnGet()
        {            
        }
    }
}