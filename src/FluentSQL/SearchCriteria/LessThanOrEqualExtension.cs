﻿using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class LessThanOrEqualExtension
    {
        /// <summary>
        /// Adds the criteria less than or equal to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns></returns>
        public static IAndOr<T> LessThanOrEqual<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr(expression);
            andor.Add(new LessThanOrEqual<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value));
            return andor;
        }

        /// <summary>
        /// Adds the criteria less than or equal to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>IAndOr</returns>
        public static IAndOr<T> AndLessThanOrEqual<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new LessThanOrEqual<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria less than or equal to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="andOr">Instance of IAndOr</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="value">Value for equality</param>
        /// <returns>IAndOr</returns>
        public static IAndOr<T> OrLessThanOrEqual<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, TProperties value) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new LessThanOrEqual<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), value, "OR"));
            return andOr;
        }
    }
}
