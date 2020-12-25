﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.Bulk
{
    public interface IEnhancedQuerySqlGenerator
    {
        QuerySqlGenerator Self { get; }

        object CreateParameter(QueryContext context, TypeMappedRelationalParameter parInfo);

        IRelationalCommand GetCommand(SelectExpression selectExpression);

        IRelationalCommand GetCommand(SelectIntoExpression selectIntoExpression);

        IRelationalCommand GetCommand(UpdateExpression updateExpression);

        IRelationalCommand GetCommand(DeleteExpression deleteExpression);
    }

    public interface IEnhancedQuerySqlGeneratorFactory<TOldFactory> : IQuerySqlGeneratorFactory
    {
    }
}