using Application.Test.Mocks.FakeData;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Test.Extensions;

public static class TestExtensions
{
    public static void AddTestMockServices(this IServiceCollection services)
    {
        services.AddTransient<ProductFakeData>();
    }
}