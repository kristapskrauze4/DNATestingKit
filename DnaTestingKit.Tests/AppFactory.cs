﻿using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetDocsShow.Tests.Integration;

internal class AppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? _serviceOverride;

    public AppFactory(Action<IServiceCollection>? serviceOverride = null)
    {
        _serviceOverride = serviceOverride;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        if (_serviceOverride is not null)
        {
            builder.ConfigureServices(_serviceOverride);
        }

        return base.CreateHost(builder);
    }
}
