﻿using FluentSQL.Extensions;

namespace FluentSQL.SearchCriteria
{
    /// <summary>
    /// Represents the search criteria equal(=)
    /// </summary>
    /// <typeparam name="T">The type to query</typeparam>
    public class Equal<T> : Criteria, ISearchCriteria
    {
        protected virtual string RelationalOperator => "=";

        protected virtual string ParameterPrefix => "PE";

        /// <summary>
        /// Get equality value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">Table Attribute</param>
        /// <param name="columnAttribute">Column Attribute</param>
        /// <param name="value">Equality value</param>
        public Equal(TableAttribute table, ColumnAttribute columnAttribute, T value) : this(table,columnAttribute,value,null)
        {}

        /// <summary>
        /// Initializes a new instance of the Equal class.
        /// </summary>
        /// <param name="table">TableAttribute</param>
        /// <param name="columnAttribute">ColumnAttribute</param>
        /// <param name="value">Equality value</param>
        /// <param name="logicalOperator">Logical Operator</param>
        public Equal(TableAttribute table, ColumnAttribute columnAttribute,T value,string? logicalOperator) : base(table,columnAttribute, logicalOperator)
        {
            Value = value;
        }

        /// <summary>
        /// Get Criteria detail
        /// </summary>
        /// <param name="statements">Statements</param>
        /// <returns>Details of the criteria</returns>
        public override CriteriaDetail GetCriteria(IStatements statements)
        {
            string tableName = Table.GetTableName(statements);

            string parameterName = $"@{ParameterPrefix}{DateTime.Now.Ticks}";
            string criterion = string.IsNullOrWhiteSpace(LogicalOperator) ? 
                $"{tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName}" :
                $"{LogicalOperator} {tableName}.{Column.GetColumnName(tableName, statements)} {RelationalOperator} {parameterName}";

            return new CriteriaDetail(this, criterion, new ParameterDetail[] { new ParameterDetail(parameterName, Value)} );
        }
    }
}
