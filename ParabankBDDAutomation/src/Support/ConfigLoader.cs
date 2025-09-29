using Microsoft.Extensions.Configuration;

public static class ConfigLoader
{
    public static TestSettings Load()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var settings = new TestSettings();
        config.GetSection("TestSettings").Bind(settings);
        return settings;
    }
}
