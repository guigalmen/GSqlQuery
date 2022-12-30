﻿using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery.Extensions
{
    internal static class IAndOrExtension
    {
        internal static string GetCliteria<T, TReturn>(this IAndOr<T, TReturn> andOr, IStatements statements, ref IEnumerable<CriteriaDetail> criterias) where TReturn : IQuery
        {
            if (andOr != null)
            {
                criterias = criterias ?? andOr.BuildCriteria(statements);
                return string.Join(" ", criterias.Select(x => x.QueryPart));
            }

            return string.Empty;
        }
    }
}
