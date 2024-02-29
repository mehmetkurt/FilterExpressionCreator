﻿using Newtonsoft.Json;
using Plainquire.Sort.Abstractions;
using Plainquire.Sort.Newtonsoft;
using Plainquire.Sort.Tests.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Plainquire.Sort.Tests.Services;

public delegate List<TEntity> EntitySortFunction<TEntity>(IEnumerable<TEntity> items, EntitySort<TEntity> sort, SortConfiguration? configuration = null, ISortInterceptor? interceptor = null);

[ExcludeFromCodeCoverage]
public static class EntitySortFunctions
{
    public static IEnumerable<EntitySortFunction<TEntity>> GetEntitySortFunctions<TEntity>() where TEntity : class
        => [
            SortDirectByLinq,
            SortNetCloneByLinq,
            SortNewtonCloneByLinq,
            SortDirectByEF,
            SortNetCloneByEF,
            SortNewtonCloneByEF
        ];

    public static IEnumerable<object> GetEntitySortFunctions(Type entityType)
        => (IEnumerable<object>)typeof(EntitySortFunctions)
            .GetMethod(nameof(GetEntitySortFunctions), BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly, [])!
            .MakeGenericMethod(entityType)
            .Invoke(null, [])!;

    private static List<TEntity> SortDirectByLinq<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        => testItems.OrderBy(sort, configuration, interceptor).ToList();

    private static List<TEntity> SortNetCloneByLinq<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        => testItems.OrderBy(sort.Clone(), configuration, interceptor).ToList();

    private static List<TEntity> SortNewtonCloneByLinq<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        => testItems.OrderBy(sort.NewtonsoftClone(), configuration, interceptor).ToList();

    private static List<TEntity> SortDirectByEF<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        where TEntity : class
        => testItems.OrderByEF(sort, configuration, interceptor);

    private static List<TEntity> SortNetCloneByEF<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        where TEntity : class
        => testItems.OrderByEF(sort.Clone(), configuration, interceptor);

    private static List<TEntity> SortNewtonCloneByEF<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        where TEntity : class
        => testItems.OrderByEF(sort.NewtonsoftClone(), configuration, interceptor);

    private static EntitySort<TEntity> NewtonsoftClone<TEntity>(this EntitySort<TEntity> sort)
    {
        var serializerSettings = new JsonSerializerSettings { Converters = JsonConverterExtensions.NewtonsoftConverters };
        var json = JsonConvert.SerializeObject(sort, serializerSettings);
        return JsonConvert.DeserializeObject<EntitySort<TEntity>>(json, serializerSettings)!;
    }

    private static List<TEntity> OrderByEF<TEntity>(this IEnumerable<TEntity> testItems, EntitySort<TEntity> sort, SortConfiguration? configuration, ISortInterceptor? interceptor)
        where TEntity : class
    {
        using var dbContext = new TestDbContext<TEntity>();
        dbContext.Set<TEntity>().AddRange(testItems);
        dbContext.SaveChanges();

        return dbContext.Set<TEntity>().OrderBy(sort, configuration, interceptor).ToList();
    }
}