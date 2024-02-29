﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Plainquire.Filter.Tests.Extensions;
using Plainquire.Filter.Tests.Models;
using Plainquire.Filter.Tests.Services;
using System.Diagnostics.CodeAnalysis;

namespace Plainquire.Filter.Tests.Tests.TypeFilter;

[TestClass, ExcludeFromCodeCoverage]
public class FilterForShortNullableBySyntaxTests
{
    [DataTestMethod]
    [FilterTestDataSource(nameof(_testCases))]
    public void FilterForShortNullableBySyntax_WorksAsExpected(FilterTestCase<short?, short?> testCase, EntityFilterFunc<TestModel<short?>> filterFunc)
        => testCase.Run(_testItems, filterFunc);

    private static readonly TestModel<short?>[] _testItems =
    [
        new() { ValueA = -9 },
        new() { ValueA = -5 },
        new() { ValueA = -0 },
        new() { ValueA = +5 },
        new() { ValueA = +9 },
        new() { ValueA = null }
    ];

    private static readonly FilterTestCase<short?, short?>[] _testCases =
    [
        FilterTestCase.Create<short?>(1100, "null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1101, "", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1102, "-5", x => x == -5),
        FilterTestCase.Create<short?>(1103, "-10", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1104, "+5", x => x == +5),

        FilterTestCase.Create<short?>(1200, "~5", x => x is +5 or -5),
        FilterTestCase.Create<short?>(1201, "~-5", x => x == -5),
        FilterTestCase.Create<short?>(1202, "~3", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1203, "~0", x => x == 0),

        FilterTestCase.Create<short?>(1300, "=null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1301, "=", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1302, "=-5", x => x == -5),
        FilterTestCase.Create<short?>(1303, "=-10", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1304, "=+5", x => x == +5),

        FilterTestCase.Create<short?>(1400, "==null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1401, "==", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1402, "==-5", x => x == -5),
        FilterTestCase.Create<short?>(1403, "==-10", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1404, "==+5", x => x == +5),

        FilterTestCase.Create<short?>(1500, "!null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1501, "!", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1502, "!-5", x => x != -5),
        FilterTestCase.Create<short?>(1503, "!-10", _ => TestItems.ALL),
        FilterTestCase.Create<short?>(1504, "!+5", x => x != +5),

        FilterTestCase.Create<short?>(1600, "<null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1601, "<", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1602, "<-5", x => x < -5),
        FilterTestCase.Create<short?>(1603, "<-10", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1604, "<+5", x => x < +5),

        FilterTestCase.Create<short?>(1700, "<=null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1701, "<=", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1702, "<=-5", x => x <= -5),
        FilterTestCase.Create<short?>(1703, "<=-10", _ => TestItems.NONE),
        FilterTestCase.Create<short?>(1704, "<=+5", x => x <= +5),

        FilterTestCase.Create<short?>(1800, ">null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1801, ">", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1802, ">-5", x => x > -5),
        FilterTestCase.Create<short?>(1803, ">-10", x => x >= -10),
        FilterTestCase.Create<short?>(1804, ">+5", x => x > +5),

        FilterTestCase.Create<short?>(1900, ">=null", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1901, ">=", new FilterExpressionException("Unable to parse given filter value")),
        FilterTestCase.Create<short?>(1902, ">=-5", x => x >= -5),
        FilterTestCase.Create<short?>(1903, ">=-10", x => x >= -10),
        FilterTestCase.Create<short?>(1904, ">=+5", x => x >= +5),

        FilterTestCase.Create<short?>(2000, "ISNULL", x => x == null),

        FilterTestCase.Create<short?>(2100, "NOTNULL", x => x != null)
    ];
}