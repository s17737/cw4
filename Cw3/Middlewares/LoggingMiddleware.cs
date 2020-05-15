using Cw3.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cw3.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IStudentsDbService service)
        {
            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string sciezka = httpContext.Request.Path; //"weatherforecast/cos"
                string querystring = httpContext.Request?.QueryString.ToString();
                string metoda = httpContext.Request.Method.ToString();
                string bodyStr = "";

                using (StreamReader reader
                 = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                }
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\Users\Paulina\Desktop\requestsLog.txt", true))
                {
                    file.WriteLine("sciezka: "+sciezka
                        + "{0}querystring: "+querystring
                        +"{0}metoda: "+metoda
                        +"{0}bodyStr: "+bodyStr);
                }
            }

            await _next(httpContext);
        }
    }
}
