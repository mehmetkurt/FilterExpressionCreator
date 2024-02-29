﻿using Plainquire.Filter.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plainquire.Filter;

/// <summary>
/// Extension methods used to handle MVC controller action parameters
/// </summary>
public static class ParameterExtensions
{
    /// <summary>
    /// Gets all properties filterable by <see cref="EntityFilter"/>.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <autogeneratedoc />
    public static IEnumerable<PropertyInfo> GetFilterableProperties(this Type type)
        => type.GetProperties().Where(x => x.PropertyType.IsFilterableProperty() && x.IsParameterFilterable());

    /// <summary>
    /// Gets the (MVC controller action) parameter name of the filter.
    /// </summary>
    /// <param name="member">The property to get the name for.</param>
    /// <param name="prefix">A prefix to use.</param>
    /// <autogeneratedoc />
    public static string GetFilterParameterName(this MemberInfo member, string? prefix = null)
    {
        var filterAttribute = member.GetCustomAttribute<FilterAttribute>();
        return $"{prefix ?? member.ReflectedType?.Name}{filterAttribute?.Name ?? member.Name}".LowercaseFirstChar();
    }

    /// <summary>
    /// Determines whether this parameter is visible as (MVC controller action) parameter.
    /// </summary>
    /// <param name="member">The member.</param>
    /// <autogeneratedoc />
    private static bool IsParameterFilterable(this MemberInfo member)
    {
        var filterAttribute = member.GetCustomAttribute<FilterAttribute>();
        return filterAttribute?.Filterable != false;
    }
}