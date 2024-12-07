using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Postech.Fase3.Contatos.Delete.Infra.Ioc;
using Postech.Fase3.Contatos.Update.Service;
using Prometheus;
using Serilog;

var builder = Host
    .CreateDefaultBuilder(args)

    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure(app =>
        {
            app.UseRouting();
            app.UseMetricServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
            });
        });

        webBuilder.UseUrls("http://+:5001");
    })
    .ConfigureServices((hostContext, services) =>
    {

        services.AddHostedService<WkDeleteContato>();
        services.AdicionarDependencias();
        services.AdicionarDbContext(hostContext.Configuration);
    })
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console());



await builder.Build().RunAsync();