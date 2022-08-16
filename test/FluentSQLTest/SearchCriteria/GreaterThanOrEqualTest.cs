﻿using FluentSQL.Default;
using FluentSQL.Models;
using FluentSQL.SearchCriteria;
using FluentSQLTest.Extensions;
using FluentSQLTest.Models;

namespace FluentSQLTest.SearchCriteria
{
    public class GreaterThanOrEqualTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly IStatements _statements;
        private readonly QueryBuilder<Test1> _queryBuilder;

        public GreaterThanOrEqualTest()
        {
            _columnAttribute = new ColumnAttribute("Id");
            _tableAttribute = new TableAttribute("Test1");
            _statements = new FluentSQL.Default.Statements();
            _queryBuilder = new(new ClassOptions(typeof(Test1)), new List<string> { nameof(Test1.Id), nameof(Test1.Name), nameof(Test1.Create) },
                new FluentSQL.Default.Statements(), QueryType.Select);
        }

        [Fact]
        public void Should_create_an_instance()
        {
            GreaterThanOrEqual<int> equal = new(_tableAttribute, _columnAttribute, 1);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(1, equal.Value);
            Assert.Null(equal.LogicalOperator);
        }

        [Theory]
        [InlineData("AND", 4)]
        [InlineData("OR", 5)]
        public void Should_create_an_instance_1(string logicalOperator, int value)
        {
            GreaterThanOrEqual<int> equal = new(_tableAttribute, _columnAttribute, value, logicalOperator);

            Assert.NotNull(equal);
            Assert.NotNull(equal.Table);
            Assert.NotNull(equal.Column);
            Assert.Equal(value, equal.Value);
            Assert.NotNull(equal.LogicalOperator);
            Assert.Equal(logicalOperator, equal.LogicalOperator);
        }

        [Theory]
        [InlineData(null, 4, "Test1.Id >= @Param")]
        [InlineData("AND", 4, "AND Test1.Id >= @Param")]
        [InlineData("OR", 5, "OR Test1.Id >= @Param")]
        public void Should_get_criteria_detail(string logicalOperator, int value, string querypart)
        {
            GreaterThanOrEqual<int> equal = new(_tableAttribute, _columnAttribute, value, logicalOperator);
            var result = equal.GetCriteria(_statements);

            Assert.NotNull(result);
            Assert.NotNull(result.SearchCriteria);
            Assert.NotNull(result.SearchCriteria.Column);
            Assert.NotNull(result.SearchCriteria.Table);
            Assert.NotNull(result.ParameterDetails);
            Assert.NotEmpty(result.ParameterDetails);
            var parameter = result.ParameterDetails.ElementAt(0);
            Assert.Equal(value, parameter.Value);
            Assert.NotNull(parameter.Name);
            Assert.NotEmpty(parameter.Name);
            Assert.NotNull(result.QueryPart);
            Assert.NotEmpty(result.QueryPart);
            Assert.Equal(querypart, result.ParameterReplace());
        }

        [Fact]
        public void Should_add_the_equality_query()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.GreaterThanOrEqual(x => x.Id, 1);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void Should_add_the_equality_query_with_and()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.GreaterThanOrEqual(x => x.Id, 1).AndGreaterThanOrEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void Should_add_the_equality_query_with_or()
        {
            IWhere<Test1> where = new Where<Test1>(_queryBuilder);
            var andOr = where.GreaterThanOrEqual(x => x.Id, 1).OrGreaterThanOrEqual(x => x.IsTest, true);
            Assert.NotNull(andOr);
            var result = andOr.BuildCriteria();
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
        }
    }
}
