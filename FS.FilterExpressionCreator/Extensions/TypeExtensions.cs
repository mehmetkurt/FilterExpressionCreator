﻿using FS.FilterExpressionCreator.Models;
using System;
using System.Collections;
using FS.FilterExpressionCreator.Filters;
using FS.FilterExpressionCreator.PropertyFilterExpressionCreators;
using FS.FilterExpressionCreator.ValueFilterExpressionCreators;

namespace FS.FilterExpressionCreator.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns the underlying type when type is <see cref="Nullable{T}"/>; otherwise the type is returned.
        /// </summary>
        public static Type GetUnderlyingType(this Type type)
            => type != null
                ? Nullable.GetUnderlyingType(type) ?? type
                : null;

        /// <summary>
        /// Returns the underlying type when type is <see cref="Nullable{T}"/>; otherwise the type is returned.
        /// </summary>
        public static Type GetUnderlyingType(this object obj)
            => (obj?.GetType()).GetUnderlyingType();

        /// <summary>
        /// Determines whether the type implements <see cref="IsGenericIEnumerable(Type)"/>.
        /// </summary>
        /// <param name="type">The type to check.</param>
        public static bool IsGenericIEnumerable(this Type type)
            => type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type);

        /// <summary>
        /// Determines whether the given type is <see cref="EntityFilter{TEntity}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <autogeneratedoc />
        public static bool IsGenericEntityFilter(this Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(EntityFilter<>);

        /// <summary>
        /// Determines whether a type is filterable by <see cref="EntityFilter{TEntity}"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <autogeneratedoc />
        public static bool IsFilterableProperty(this Type type)
            => PropertyFilterExpressionCreator.CanCreateFilterFor(type);
    }
}
