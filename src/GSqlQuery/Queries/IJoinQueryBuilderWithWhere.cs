﻿using System.Linq.Expressions;
using System;

namespace GSqlQuery
{
    public interface IJoinQueryBuilderWithWhere<T1, T2, TReturn> : IQueryBuilderWithWhere<Join<T1, T2>, TReturn>
        where T1 : class, new()
        where T2 : class, new()
        where TReturn : IQuery
    {
        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> InnerJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> LeftJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> RightJoin<TJoin>() where TJoin : class, new();

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> InnerJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> LeftJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();

        IComparisonOperators<Join<T1, T2, TJoin>, JoinQuery<Join<T1, T2, TJoin>>> RightJoin<TJoin>(Expression<Func<TJoin, object>> expression)
            where TJoin : class, new();
    }

    public interface IJoinQueryBuilderWithWhere<T1, T2, T3, TReturn> : IQueryBuilderWithWhere<Join<T1, T2, T3>, TReturn>
        where T1 : class, new()
        where T2 : class, new()
        where T3 : class, new()
        where TReturn : IQuery
    { }
}
