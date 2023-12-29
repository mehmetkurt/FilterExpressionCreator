﻿using FS.FilterExpressionCreator.Enums;
using FS.FilterExpressionCreator.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace FS.FilterExpressionCreator.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="ValueFilter"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class ValueFilterExtensions
    {
        /// <summary>
        /// Create <see cref="ValueFilter"/> from filterSyntax.
        /// </summary>
        /// <param name="filterSyntax">The filter micro syntax to create the filter from.</param>
        public static ValueFilter[] Create(string filterSyntax)
        {
            var filters = SplitValues(filterSyntax);
            return filters.Select(ValueFilter.Create).ToArray();
        }

        /// <summary>
        /// Creates filter micro syntax string from <see cref="ValueFilter"/>.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <autogeneratedoc />
        public static string ToString(ValueFilter[] filters)
        {
            if (filters == null)
                return null;

            var filterStrings = filters
                .Where(x => x != null)
                .Select(x => x.ToString().Replace(",", "\\,"));

            return string.Join(",", filterStrings);
        }

        /// <summary>
        /// Humanizes the filter syntax.
        /// </summary>
        /// <typeparam name="TValue">The type of the filtered value.</typeparam>
        /// <param name="filterSyntax">The filter syntax.</param>
        /// <param name="valueName">Name of the value.</param>
        public static string HumanizeFilterSyntax<TValue>(this string filterSyntax, string valueName)
        {
            if (string.IsNullOrWhiteSpace(filterSyntax))
                return $"{valueName} is unfiltered";

            var filters = ValueFilterExtensions
                .Create(filterSyntax)
                .GroupBy(x => x.Operator)
                .Select(filterGroup =>
                {
                    var filterOperator = filterGroup.Key;
                    var operatorName = filterOperator.GetOperatorName<TValue>();

                    if (filterOperator == FilterOperator.IsNull || filterOperator == FilterOperator.NotNull)
                        return $"{valueName} {operatorName}";

                    var filterValues = filterGroup.Select(x => x.Value).ToArray();
                    var valuesButLast = filterValues[..^1];
                    var prefixValueList = string.Join("', '", valuesButLast);
                    //var concatKey = filter.Operator == FilterOperator.NotEqual ? "nor" : "or";
                    var valueList = !string.IsNullOrEmpty(prefixValueList)
                        ? $"'{prefixValueList}' or '{filterValues[^1]}'"
                        : $"'{filterValues[^1]}'";

                    return $"{valueName} {operatorName} {valueList}";
                });

            return string.Join(" or ", filters);
        }

        private static string GetOperatorName<TValue>(this FilterOperator filterOperator)
        {
            filterOperator = filterOperator != FilterOperator.Default ? filterOperator : GetDefaultOperator<TValue>();
            return filterOperator switch
            {
                FilterOperator.Contains => "contains",
                FilterOperator.EqualCaseSensitive => "is (case sensitive)",
                FilterOperator.EqualCaseInsensitive => "is",
                FilterOperator.NotEqual => "is not",
                FilterOperator.LessThanOrEqual => "is less than or equal to",
                FilterOperator.LessThan => "is less than",
                FilterOperator.GreaterThanOrEqual => "is greater than or equal to",
                FilterOperator.GreaterThan => "is greater than",
                FilterOperator.IsNull => "is null",
                FilterOperator.NotNull => "is not null",
                _ => throw new ArgumentOutOfRangeException(nameof(filterOperator))
            };
        }

        private static FilterOperator GetDefaultOperator<TValue>()
            => typeof(TValue) == typeof(string)
                ? FilterOperator.Contains
                : FilterOperator.EqualCaseInsensitive;

        private static IEnumerable<string> SplitValues(string filterSyntax)
        {
            if (filterSyntax == null)
                return null;

            return Regex
                .Split(filterSyntax, @"(?<!\\)[\|,]")
                .Select(element => element
                            .Replace(@"\|", @"|")
                            .Replace(@"\,", @",")
                            .Replace(@"\\", @"\")
                )
                .ToArray();
        }
    }
}
