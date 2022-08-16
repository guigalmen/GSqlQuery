﻿using FluentSQL.Extensions;
using FluentSQL.Helpers;
using System.Linq.Expressions;

namespace FluentSQL.SearchCriteria
{
    public static class BetweenExtension
    {
        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> Between<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression, 
            TProperties initial, TProperties final) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr(expression);
            andor.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial,final));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> AndBetween<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression, 
            TProperties initial, TProperties final) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>
        /// <param name="final">Final value</param>
        /// <returns>Instance of IAndOr</returns>
        public static IAndOr<T> OrBetween<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial, TProperties final) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new Between2<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, final, "OR"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T> Between<T, TProperties>(this IWhere<T> where, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new()
        {
            IAndOr<T> andor = where.GetAndOr(expression);
            andor.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial));
            return andor;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator AND
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T> AndBetween<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "AND"));
            return andOr;
        }

        /// <summary>
        /// Adds the criteria between to the query with the logical operator OR
        /// </summary>
        /// <typeparam name="T">The type to query</typeparam>
        /// <typeparam name="TProperties">TProperties is property of T class</typeparam>
        /// <param name="where">Instance of IWhere</param>
        /// <param name="expression">Expression to evaluate</param>
        /// <param name="initial">Initial value</param>        
        /// <returns>IAndOr</returns>
        public static IAndOr<T> OrBetween<T, TProperties>(this IAndOr<T> andOr, Expression<Func<T, TProperties>> expression,
            TProperties initial) where T : class, new()
        {
            andOr.Validate(expression);
            andOr.Add(new Between<TProperties>(ClassOptionsFactory.GetClassOptions(typeof(T)).Table, expression.GetColumnAttribute(), initial, "OR"));
            return andOr;
        }
    }
}
