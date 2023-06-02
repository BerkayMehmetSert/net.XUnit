using Application.Test.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTestMockServices();
    }
}