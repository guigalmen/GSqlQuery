﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GSqlQuery.Extensions;

[assembly: InternalsVisibleTo("GSqlQuery.Test, PublicKey=0024000004800000940000000602000000240000525341310004000001000100913cebd9950f6fcb7fb913297422ef8f3cbdec249d3bbba88346b2045500eeda9546b5fd977bc95be5efb2ca6a8f15a2907dc1bab80d177d2e43b77db77befe6ce26b647e89871a9fede8174dc504ac3322cf5952141cf5fbbdf789fc074bcced5cdc939120d2f67ac483495a97d4df9d3a5fe13f76e40840ee0d70b2dda4b9c")]
[assembly: InternalsVisibleTo("GSqlQuery.Runner, PublicKey=0024000004800000940000000602000000240000525341310004000001000100913cebd9950f6fcb7fb913297422ef8f3cbdec249d3bbba88346b2045500eeda9546b5fd977bc95be5efb2ca6a8f15a2907dc1bab80d177d2e43b77db77befe6ce26b647e89871a9fede8174dc504ac3322cf5952141cf5fbbdf789fc074bcced5cdc939120d2f67ac483495a97d4df9d3a5fe13f76e40840ee0d70b2dda4b9c")]

namespace GSqlQuery.Queries
{
    /// <summary>
    /// Select Query Builder
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    internal class SelectQueryBuilder<T> : QueryBuilderWithCriteria<T, SelectQuery<T>>, IQueryBuilderWithWhere<T, SelectQuery<T>> where T : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the SelectQueryBuilder class.
        /// </summary>
        /// <param name="options">Detail of the class to transform</param>
        /// <param name="selectMember">Selected Member Set</param>
        /// <param name="statements">Statements to build the query</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SelectQueryBuilder(IEnumerable<string> selectMember, IStatements statements)
            : base(statements)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            Columns = ClassOptionsFactory.GetClassOptions(typeof(T)).GetPropertyQuery(selectMember);
        }

        internal static string CreateQuery(bool isWhere, IStatements statements, IEnumerable<PropertyOptions> columns, string tableName, string criterias )
        {
            string result = string.Empty;

            if (!isWhere)
            {
                result = string.Format(statements.Select,
                    string.Join(",", columns.Select(x => x.ColumnAttribute.GetColumnName(tableName, statements))),
                    tableName);
            }
            else
            {
                result = string.Format(statements.SelectWhere,
                    string.Join(",", columns.Select(x => x.ColumnAttribute.GetColumnName(tableName, statements))),
                    tableName, criterias);
            }

            return result;
        }

        /// <summary>
        /// Build select query
        /// </summary>
        /// <returns>SelectQuery</returns>
        public override SelectQuery<T> Build()
        {
            var query = CreateQuery(_andOr != null, Statements, Columns, _tableName, _andOr != null ? GetCriteria() : "");
            return new SelectQuery<T>(query, Columns.Select(x => x.ColumnAttribute), _criteria, Statements);
        }

        /// <summary>
        /// Add where query
        /// </summary>
        /// <returns>IWhere</returns>
        public override IWhere<T, SelectQuery<T>> Where()
        {
            SelectWhere<T> selectWhere = new SelectWhere<T>(this);
            _andOr = selectWhere;
            return (IWhere<T, SelectQuery<T>>)_andOr;
        }
    }
}