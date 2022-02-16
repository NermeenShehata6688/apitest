using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Middleware
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public BasicAuthenticationMiddleware(RequestDelegate next, IConfiguration config )
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                string authHeader = httpContext.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    //Extract credentials
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    int index = usernamePassword.IndexOf(':');

                    var username = usernamePassword.Substring(0, index);
                    var password = usernamePassword.Substring(index + 1);

                    // DBAuth authenticator = new DBAuth(ConnectionString);
                    string savedUser = _config.GetSection("Autho").GetSection("UserName").Value;
                    string savedPass = _config.GetSection("Autho").GetSection("Password").Value;

                    if (savedUser== username&& savedPass== password)
                    {
                        await _next.Invoke(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401; //Unauthorized
                        return;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 401; //Unauthorized
                    return;
                }

            }
            catch (Exception)
            {
                httpContext.Response.StatusCode = 401; //Unauthorized
                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BasicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
