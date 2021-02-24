﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.EntityFrameworkCore.Bulk
{
    internal static class BatchOperationMethods
    {
        public static readonly MethodInfo s_CreateCommonTable_TSource_TTarget
            = new Func<IQueryable<object>,
                       List<object>,
                       IQueryable<object>>(
                BatchOperationExtensions.CreateCommonTable)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_Merge_TTarget_TSource_TJoinKey
            = new Func<DbSet<object>,
                       IQueryable<object>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object, object>>,
                       Expression<Func<object, object>>,
                       bool,
                       int>(
                BatchOperationExtensions.Merge)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_BatchDelete_TSource
            = new Func<IQueryable<object>,
                       int>(
                BatchOperationExtensions.BatchDelete)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_BatchUpdate_TSource
            = new Func<IQueryable<object>,
                       Expression<Func<object, object>>,
                       int>(
                BatchOperationExtensions.BatchUpdate)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_BatchUpdateJoinQueryable_TOuter_TInner_TKey
            = new Func<DbSet<object>,
                       IQueryable<object>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object, object>>,
                       Expression<Func<object, object, bool>>,
                       int>(
                BatchOperationExtensions.BatchUpdateJoin)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_BatchUpdateJoinReadOnlyList_TOuter_TInner_TKey
            = new Func<DbSet<object>,
                       IReadOnlyList<object>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object, object>>,
                       Expression<Func<object, object, bool>>,
                       int>(
                BatchOperationExtensions.BatchUpdateJoin)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_BatchInsertInto_TSource
            = new Func<IQueryable<object>,
                       DbSet<object>,
                       int>(
                BatchOperationExtensions.BatchInsertInto)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_Upsert_TTarget_TSource
            = new Func<DbSet<object>,
                       IEnumerable<object>,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object, object>>,
                       int>(
                BatchOperationExtensions.Upsert)
            .GetMethodInfo()
            .GetGenericMethodDefinition();


        public static MethodInfo s_UpsertOne_TTarget_TSource
            = new Func<DbSet<object>,
                       object,
                       Expression<Func<object, object>>,
                       Expression<Func<object, object, object>>,
                       int>(
                BatchOperationExtensions.Upsert)
            .GetMethodInfo()
            .GetGenericMethodDefinition();
    }
}