﻿using FluentSQL.Helpers;
using FluentSQL.Models;

namespace FluentSQL.Default
{
    /// <summary>
    /// Query
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public abstract class Query<T> : QueryBase, IQuery<T> where T : class, new()
    {
        public IStatements Statements { get; }

        protected ClassOptions GetClassOptions()
        {
            return ClassOptionsFactory.GetClassOptions(typeof(T));
        }

        /// <summary>
        /// Create Query object 
        /// </summary>
        /// <param name="columns">Columns of the query</param>
        /// <param name="criteria">Query criteria</param>
        /// <param name="statements">Statements to use in the query</param>
        /// <param name="text">The Query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria, IStatements statements) :
            base(text,columns, criteria)
        {
            Statements = statements ?? throw new ArgumentNullException(nameof(statements));
        }
    }

    public abstract class Query<T, TDbConnection, TResult> : Query<T>, IQuery<T, TDbConnection, TResult> where T : class, new()
    {
        public IDatabaseManagement<TDbConnection> DatabaseManagment { get; }

        protected Query(string text, IEnumerable<ColumnAttribute> columns, IEnumerable<CriteriaDetail>? criteria,
            ConnectionOptions<TDbConnection> connectionOptions) : 
            base(text, columns, criteria, connectionOptions?.Statements!)
        {
            connectionOptions = connectionOptions ?? throw new ArgumentNullException(nameof(connectionOptions));
            DatabaseManagment = connectionOptions.DatabaseManagment;
        }

        public abstract TResult Execute();

        public abstract TResult Execute(TDbConnection dbConnection);

        public abstract Task<TResult> ExecuteAsync(CancellationToken cancellationToken = default);

        public abstract Task<TResult> ExecuteAsync(TDbConnection dbConnection, CancellationToken cancellationToken = default);
    }
}
