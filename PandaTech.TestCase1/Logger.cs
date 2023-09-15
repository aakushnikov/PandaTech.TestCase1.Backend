using System.Reflection;
using PandaTech.TestCase1.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace PandaTech.TestCase1;

public static class Logger
{
    public static void Initialize()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .MinimumLevel.Override("System", LogEventLevel.Debug)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProperty("Service", EnvSettings.AssemblyName)
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Debug,
                theme: ConsoleTheme.None
            )
            .WriteTo.File(
                $"{Assembly.GetExecutingAssembly().Location}.log",
                LogEventLevel.Debug)
            .CreateLogger();
    }
}