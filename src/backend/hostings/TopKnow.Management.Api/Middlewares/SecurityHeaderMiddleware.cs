using Microsoft.Extensions.Options;
using System.Net;
using TopKnow.Common.Configurations;

namespace TopKnow.Management.Api.Middlewares
{
	public class SecurityHeaderMiddleware
	{
		private readonly RequestDelegate requestDelegate;

		public SecurityHeaderMiddleware(RequestDelegate requestDelegate)
        {
			this.requestDelegate = requestDelegate;
		}

		public async Task Invoke(HttpContext context)
		{
			var header = context.Request.Headers.TryGetValue("top-know-security-header", out var value);

            if (header && !string.IsNullOrEmpty(value))
            {
				//appsettings.Development.json'dan okuma yapmanızı sağlar
				var securitySettings = context.RequestServices.GetService<IOptions<SecuritySettings>>();
                if (securitySettings.Value.HeaderKey == value)
                {
					await requestDelegate(context);
					return;
				}
            }

			context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
		}
    }
}
