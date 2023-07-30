﻿using GSqlQuery.Extensions;
using GSqlQuery.Queries;
using System.Collections.Generic;
using System.Reflection;

namespace GSqlQuery.Runner.Queries
{
    internal class JoinOrderByQueryBuilder<T, TDbConnection> :
        JoinOrderByQueryBuilder<T, OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>, JoinQuery<T, TDbConnection>>,
        IQueryBuilder<OrderByQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>>
        where T : class, new()
    {
        new public ConnectionOptions<TDbConnection> Options { get; }

        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
            IQueryBuilderWithWhere<T, JoinQuery<T, TDbConnection>, ConnectionOptions<TDbConnection>> queryBuilder)
            : base(selectMember, orderBy, queryBuilder, queryBuilder.Options.Statements)
        {
            Options = queryBuilder.Options;
        }

        public JoinOrderByQueryBuilder(ClassOptionsTupla<IEnumerable<MemberInfo>> selectMember, OrderBy orderBy,
           IAndOr<T, JoinQuery<T, TDbConnection>> andOr, ConnectionOptions<TDbConnection> options)
           : base(selectMember, orderBy, andOr, options.Statements)
        {
            Options = options;
        }

        public override OrderByQuery<T, TDbConnection> Build()
        {
            var query = CreateQuery(Options.Statements, out IEnumerable<ColumnAttribute> columns, out IEnumerable<CriteriaDetail> criteria);
            return new OrderByQuery<T, TDbConnection>(query, columns, criteria, Options);
        }
    }
}