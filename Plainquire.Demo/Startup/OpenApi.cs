﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Plainquire.Demo.Extensions;
using Plainquire.Demo.Routing;
using Plainquire.Filter.Swashbuckle;
using Plainquire.Page.Swashbuckle;
using Plainquire.Sort.Swashbuckle;
using System;
using System.IO;

namespace Plainquire.Demo.Startup;

internal static class OpenApi
{
    private const string OPEN_API_UI_ROUTE = "openapi/";
    private const string OPEN_API_SPEC = "openapi.json";

    internal static IApplicationBuilder RegisterOpenApiRoutes(this IApplicationBuilder applicationBuilder)
        => applicationBuilder
            .UseSwagger(c => c.RouteTemplate = $"{OPEN_API_UI_ROUTE}{{documentName}}/{OPEN_API_SPEC}")
            .UseSwaggerUI(c =>
            {
                c.RoutePrefix = OPEN_API_UI_ROUTE.Trim('/');
                c.SwaggerEndpoint($"{V1ApiController.API_VERSION}/{OPEN_API_SPEC}", $"API version {V1ApiController.API_VERSION}");
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableTryItOutByDefault();
                c.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
                c.InjectStylesheet("/css/swagger-ui/custom.css");
            });

    internal static IServiceCollection RegisterOpenApiController(this IServiceCollection services)
    {
        var filterExpressionDoc = Path.Combine(AppContext.BaseDirectory, "Plainquire.Filter.xml");
        var sortQueryableDoc = Path.Combine(AppContext.BaseDirectory, "Plainquire.Sort.xml");
        var pageQueryableDoc = Path.Combine(AppContext.BaseDirectory, "Plainquire.Page.xml");
        var plainquireDemoDoc = Path.Combine(AppContext.BaseDirectory, "Plainquire.Demo.xml");

        return services
            .AddSwaggerGenNewtonsoftSupport()
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc(V1ApiController.API_VERSION, new OpenApiInfo { Title = $"{AssemblyExtensions.GetProgramProduct()} API", Version = V1ApiController.API_VERSION });
                c.AddFilterSupport(filterExpressionDoc, plainquireDemoDoc);
                c.AddSortSupport(sortQueryableDoc, plainquireDemoDoc);
                c.AddPageSupport(pageQueryableDoc, plainquireDemoDoc);
                c.IncludeXmlComments(filterExpressionDoc);
                c.IncludeXmlComments(sortQueryableDoc);
                c.IncludeXmlComments(pageQueryableDoc);
                c.IncludeXmlComments(plainquireDemoDoc);
            });
    }
}