﻿using GSqlQuery.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GSqlQuery
{
    public abstract class QueryBase : IQuery
    {
        private readonly IEnumerable<PropertyOptions> _columns;
        private readonly IEnumerable<CriteriaDetail> _criteria;
        private string _text;

        public string Text { get => _text; set => _text = value; }

        public IEnumerable<PropertyOptions> Columns => _columns;

        public IEnumerable<CriteriaDetail> Criteria => _criteria;

        public QueryBase(string text, IEnumerable<PropertyOptions> columns, IEnumerable<CriteriaDetail> criteria)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            text.NullValidate("", nameof(text));
            _text = text;
            _criteria = criteria ?? Enumerable.Empty<CriteriaDetail>();
        }
    }
}