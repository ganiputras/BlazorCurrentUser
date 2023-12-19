
namespace WebApp.Ui.Services
{
    public class UserServiceMiddleware
    {
        private readonly RequestDelegate _next;

        public UserServiceMiddleware(RequestDelegate next)
        {
            this._next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUserService service)
        {
            service.SetUser(context.User);
            await _next(context);
        }
    }
}
