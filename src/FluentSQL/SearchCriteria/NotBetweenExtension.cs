﻿using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class NotBetweenExtension
    {
        /// <summary>
        /// Adds the criteria not between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> NotBetween<T, TReturn, TProperties>(this IWhere<T, TReturn> where, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            IAndOr<T, TReturn> andor = where.GetAndOr(expression);
            andor.Add(new NotBetween<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final));
            return andor;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> AndNotBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class, new() where TReturn : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new NotBetween<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria not between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value </param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T, TReturn> OrNotBetween<T, TReturn, TProperties>(this IAndOr<T, TReturn> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class, new() where TReturn  : IQuery
        {
            andOr.Validate(expression);
            andOr.Add(new NotBetween<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "OR"));
            return andOr;
        }
    }
}
