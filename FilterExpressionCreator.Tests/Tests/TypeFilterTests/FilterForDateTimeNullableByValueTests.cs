﻿using FilterExpressionCreator.Enums;
using FilterExpressionCreator.Models;
using FilterExpressionCreator.Tests.Attributes;
using FilterExpressionCreator.Tests.Extensions;
using FilterExpressionCreator.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FilterExpressionCreator.Tests.Tests.TypeFilterTests
{
    [TestClass]
    public class FilterForDateTimeNullableByValueTests : TestBase<DateTime?>
    {
        [DataTestMethod]
        [FilterTestDataSource(nameof(_testCases), nameof(TestModelFilterFunctions))]
        public void FilterForDateTimeNullableByValue_WorksAsExpected(object testCase, TestModelFilterFunc<DateTime?> filterFunc)
        {
            switch (testCase)
            {
                case FilterTestCase<DateTime?, DateTime?> dateTimeTestCase:
                    dateTimeTestCase.Run(_testItems, filterFunc);
                    break;
                case FilterTestCase<DateTimeSpan?, DateTime?> dateTimeSpanTestCase:
                    dateTimeSpanTestCase.Run(_testItems, filterFunc);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported test case");
            }
        }

        private static readonly TestModel<DateTime?>[] _testItems = {
            new() { ValueA = null },
            new() { ValueA = new DateTime(1900, 01, 01) },
            new() { ValueA = new DateTime(2000, 01, 01) },
            new() { ValueA = new DateTime(2010, 01, 01) },
            new() { ValueA = new DateTime(2010, 06, 01) },
            new() { ValueA = new DateTime(2010, 06, 15) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 0, 0) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 0) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 30) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 30, 31) },
            new() { ValueA = new DateTime(2010, 06, 15, 12, 31, 00) },
            new() { ValueA = new DateTime(2010, 06, 15, 13, 00, 00) },
            new() { ValueA = new DateTime(2010, 06, 16) },
            new() { ValueA = new DateTime(2010, 07, 01) },
            new() { ValueA = new DateTime(2011, 01, 01) },
            new() { ValueA = new DateTime(2020, 01, 01) },
        };

        // ReSharper disable RedundantExplicitArrayCreation
        private static readonly object[] _testCases = {
            FilterTestCase.Create(1100, FilterOperator.Default, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x >= new DateTime(2010, 01, 01) && x < new DateTime(2011, 01, 01)),
            FilterTestCase.Create(1101, FilterOperator.Default, new DateTime?[] { new (2010, 06, 01) }, (DateTime? x) => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01)),
            FilterTestCase.Create(1102, FilterOperator.Default, new DateTime?[] { new (2010, 06, 15) }, (DateTime? x) => x >= new DateTime(2010, 06, 15) && x < new DateTime(2010, 06, 16)),
            FilterTestCase.Create(1103, FilterOperator.Default, new DateTime?[] { new (2010, 06, 15, 12, 00, 00) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 00, 00) && x < new DateTime(2010, 06, 15, 13, 00, 00)),
            FilterTestCase.Create(1104, FilterOperator.Default, new DateTime?[] { new (2010, 06, 15, 12, 30, 00) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create(1105, FilterOperator.Default, new DateTime?[] { new (2010, 06, 15, 12, 30, 30) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 30, 30) && x < new DateTime(2010, 06, 15, 12, 30, 31)),
            FilterTestCase.Create(1106, FilterOperator.Default, new DateTime?[] { new (2100, 01, 01) }, (DateTime? _) => NONE),
            FilterTestCase.Create(1107, FilterOperator.Default, new DateTimeSpan?[] { new (new DateTime(2010, 06, 01), new DateTime(2010, 06, 15, 12, 31, 00)) }, (DateTime? x) => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 06, 15, 12, 31, 00)),

            FilterTestCase.Create(1200, FilterOperator.Contains, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x >= new DateTime(2010, 01, 01) && x < new DateTime(2011, 01, 01)),
            FilterTestCase.Create(1201, FilterOperator.Contains, new DateTime?[] { new (2010, 06, 01) }, (DateTime? x) => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 07, 01)),
            FilterTestCase.Create(1202, FilterOperator.Contains, new DateTime?[] { new (2010, 06, 15) }, (DateTime? x) => x >= new DateTime(2010, 06, 15) && x < new DateTime(2010, 06, 16)),
            FilterTestCase.Create(1203, FilterOperator.Contains, new DateTime?[] { new (2010, 06, 15, 12, 00, 00) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 00, 00) && x < new DateTime(2010, 06, 15, 13, 00, 00)),
            FilterTestCase.Create(1204, FilterOperator.Contains, new DateTime?[] { new (2010, 06, 15, 12, 30, 00) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 30, 00) && x < new DateTime(2010, 06, 15, 12, 31, 00)),
            FilterTestCase.Create(1205, FilterOperator.Contains, new DateTime?[] { new (2010, 06, 15, 12, 30, 30) }, (DateTime? x) => x >= new DateTime(2010, 06, 15, 12, 30, 30) && x < new DateTime(2010, 06, 15, 12, 30, 31)),
            FilterTestCase.Create(1206, FilterOperator.Contains, new DateTime?[] { new (2100, 01, 01) }, (DateTime? _) => NONE),
            FilterTestCase.Create(1207, FilterOperator.Contains, new DateTimeSpan?[] { new (new DateTime(2010, 06, 01), new DateTime(2010, 06, 15, 12, 31, 00)) }, (DateTime? x) => x >= new DateTime(2010, 06, 01) && x < new DateTime(2010, 06, 15, 12, 31, 00)),

            FilterTestCase.Create(1300, FilterOperator.EqualCaseInsensitive, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x == new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1301, FilterOperator.EqualCaseInsensitive, new DateTime?[] { new (2010, 06, 15, 12, 30, 30) }, (DateTime? x) => x == new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create(1302, FilterOperator.EqualCaseInsensitive, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x == new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1400, FilterOperator.EqualCaseSensitive, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x == new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1401, FilterOperator.EqualCaseSensitive, new DateTime?[] { new (2010, 06, 15, 12, 30, 30) }, (DateTime? x) => x == new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create(1402, FilterOperator.EqualCaseSensitive, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x == new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1500, FilterOperator.NotEqual, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x != new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1501, FilterOperator.NotEqual, new DateTime?[] { new (2010, 06, 15, 12, 30, 30) }, (DateTime? x) => x != new DateTime(2010, 06, 15, 12, 30, 30)),
            FilterTestCase.Create(1502, FilterOperator.NotEqual, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x != new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1600, FilterOperator.LessThan, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x < new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1601, FilterOperator.LessThan, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x < new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1700, FilterOperator.LessThanOrEqual, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x <= new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1701, FilterOperator.LessThanOrEqual, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x <= new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1800, FilterOperator.GreaterThan, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x > new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1801, FilterOperator.GreaterThan, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x > new DateTime(2010, 01, 01)),

            FilterTestCase.Create(1900, FilterOperator.GreaterThanOrEqual, new DateTime?[] { new (2010, 01, 01) }, (DateTime? x) => x >= new DateTime(2010, 01, 01)),
            FilterTestCase.Create(1901, FilterOperator.GreaterThanOrEqual, new DateTimeSpan?[] { new (new DateTime(2010, 01, 01), new DateTime(2020, 01, 01)) }, (DateTime? x) => x >= new DateTime(2010, 01, 01)),

            FilterTestCase.Create(2000, FilterOperator.IsNull, (DateTime?[])null, (DateTime? x) => x == null),

            FilterTestCase.Create(2100, FilterOperator.NotNull, (DateTime?[])null, (DateTime? x) => x != null),
        };
        // ReSharper restore RedundantExplicitArrayCreation
    }
}
