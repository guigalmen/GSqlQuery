﻿using GSqlQuery.Extensions;
using GSqlQuery.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    public class AndOrBase<T, TReturn> : WhereBase<T, TReturn>, IAndOr<T, TReturn>, ISearchCriteriaBuilder<T, TReturn>
        where T : class, new() where TReturn : IQuery
    {
        protected readonly Queue<ISearchCriteria> _searchCriterias = new Queue<ISearchCriteria>();
        private readonly IQueryBuilderWithWhere<T, TReturn> _queryBuilderWithWhere;

        protected IEnumerable<PropertyOptions> Columns { get; set; }

        public AndOrBase(IQueryBuilderWithWhere<T, TReturn> queryBuilderWithWhere, bool isColumns = true) : base()
        {
            _queryBuilderWithWhere = queryBuilderWithWhere ?? throw new ArgumentException(nameof(queryBuilderWithWhere));
            Columns = isColumns ? ClassOptionsFactory.GetClassOptions(typeof(T)).PropertyOptions : Enumerable.Empty<PropertyOptions>();
        }

        public void Add(ISearchCriteria criteria)
        {
            criteria.NullValidate(ErrorMessages.ParameterNotNull, nameof(criteria));
            _searchCriterias.Enqueue(criteria);
        }

        public virtual IEnumerable<CriteriaDetail> BuildCriteria(IStatements statements)
        {
            return _searchCriterias.Select(x => x.GetCriteria(statements, Columns)).ToArray();
        }

        public virtual TReturn Build()
        {
            return _queryBuilderWithWhere.Build();
        }
    }
}
