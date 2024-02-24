﻿using Microsoft.Extensions.DependencyInjection;
using Schick.Plainquire.Filter.Filters;
using Schick.Plainquire.Filter.Swashbuckle.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Schick.Plainquire.Filter.Swashbuckle.Extensions;

/// <summary>
/// Extensions to register entity filter extensions to Swashbuckle.AspNetCore (https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
/// </summary>
/// <autogeneratedoc />
public static class SwaggerGenOptionsExtensions
{
    /// <summary>
    /// Replaces action parameters of type <see cref="EntityFilter{TEntity}"/> with filterable properties of type <c>TEntity</c>.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="xmlDocumentationFilePaths">Paths to XML documentation files. Used to provide parameter descriptions.</param>
    public static SwaggerGenOptions AddFilterSupport(this SwaggerGenOptions options, params string[] xmlDocumentationFilePaths)
    {
        options.OperationFilter<EntityFilterParameterReplacer>(new List<string>(xmlDocumentationFilePaths));
        options.OperationFilter<EntityFilterSetParameterReplacer>(new List<string>(xmlDocumentationFilePaths));
        return options;
    }
}