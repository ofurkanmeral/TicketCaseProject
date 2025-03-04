namespace CaseTicket.Web.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(RequestDelegate _next, ILogger<GlobalExceptionHandler> _logger)
        {
            next = _next;
            logger = _logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message.ToString());
                throw;
            }
        }
    }
}
