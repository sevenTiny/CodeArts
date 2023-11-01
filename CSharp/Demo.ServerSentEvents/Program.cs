using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

namespace Demo.ServerSentEvents
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            services.AddRouting();
            services.AddControllers();

            // Add services to the container.
            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}