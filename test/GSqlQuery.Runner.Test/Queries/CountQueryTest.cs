﻿using GSqlQuery.Runner.Test.Models;
using GSqlQuery.SearchCriteria;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GSqlQuery.Runner.Test.Queries
{
    public class CountQueryTest
    {
        private readonly ColumnAttribute _columnAttribute;
        private readonly TableAttribute _tableAttribute;
        private readonly Equal<int> _equal;
        private readonly IStatements _statements;
        private readonly ClassOptions _classOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptions;
        private readonly ConnectionOptions<IDbConnection> _connectionOptionsAsync;

        public CountQueryTest()
        {
            _classOptions = ClassOptionsFactory.GetClassOptions(typeof(Test1));
            _columnAttribute = _classOptions.PropertyOptions.First(x => x.ColumnAttribute.Name == nameof(Test1.Id)).ColumnAttribute;
            _tableAttribute = _classOptions.Table;
            _equal = new Equal<int>(_tableAttribute, _columnAttribute, 1);
            _statements = new Statements();
            _connectionOptions = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMock());
            _connectionOptionsAsync = new ConnectionOptions<IDbConnection>(_statements, LoadGSqlQueryOptions.GetDatabaseManagmentMockAsync());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("query", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);

            Assert.NotNull(query);
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.Criteria);
            Assert.NotEmpty(query.Criteria);
            Assert.NotNull(query.DatabaseManagement);
            Assert.NotNull(query.Statements);
        }

        [Fact]
        public void Should_execute_the_query()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Execute();
            Assert.Equal(1, result);
        }


        [Fact]
        public void Throw_exception_if_DatabaseManagment_not_found()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            Assert.Throws<ArgumentNullException>(() => query.Execute(null));
        }

        [Fact]
        public void Should_execute_the_query1()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptions);
            var result = query.Execute(LoadGSqlQueryOptions.GetIDbConnection());
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Should_executeAsync_the_query()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            var result = await query.ExecuteAsync(CancellationToken.None);
            Assert.Equal(1, result);
        }


        [Fact]
        public async Task Throw_exceptionAsync_if_DatabaseManagment_not_found()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await query.ExecuteAsync(null, CancellationToken.None));
        }

        [Fact]
        public async Task Should_executeAsync_the_query1()
        {
            CountQuery<Test1, IDbConnection> query = new CountQuery<Test1, IDbConnection>("SELECT COUNT([Test1].[Id]) FROM [Test1];", _classOptions.PropertyOptions, new CriteriaDetail[] { _equal.GetCriteria(_statements, _classOptions.PropertyOptions) }, _connectionOptionsAsync);
            var result = await query.ExecuteAsync(LoadGSqlQueryOptions.GetIDbConnection(), CancellationToken.None);
            Assert.Equal(1, result);
        }
    }
}