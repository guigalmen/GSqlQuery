﻿using GSqlQuery.Runner.Default;
using GSqlQuery.Runner.Models;
using GSqlQuery.Runner.Test.Models;
using System.Data.Common;

namespace GSqlQuery.Runner.Test.Default
{
    public class DeleteQueryBuilderTest
    {
        private readonly IStatements _statements;
        private readonly ConnectionOptions<DbConnection> _connectionOptions;
        public DeleteQueryBuilderTest()
        {
            _statements = new Statements();
            _connectionOptions = new ConnectionOptions<DbConnection>(_statements, LoadFluentOptions.GetDatabaseManagmentMock());
        }

        [Fact]
        public void Properties_cannot_be_null2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions);

            Assert.NotNull(queryBuilder);
            Assert.NotNull(queryBuilder.ConnectionOptions);
            Assert.NotNull(queryBuilder.ConnectionOptions.Statements);
            Assert.NotNull(queryBuilder.ConnectionOptions.DatabaseManagment);
            Assert.NotNull(queryBuilder.Columns);
            Assert.NotEmpty(queryBuilder.Columns);
        }

        [Fact]
        public void Throw_an_exception_if_nulls_are_passed_in_the_parameters2()
        {
            Assert.Throws<ArgumentNullException>(() => new DeleteQueryBuilder<Test1, DbConnection>(null));
        }

        [Fact]
        public void Should_return_an_implementation_of_the_IWhere_interface2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions);
            var where = queryBuilder.Where();
            Assert.NotNull(where);
        }

        [Fact]
        public void Should_return_an_delete_query2()
        {
            DeleteQueryBuilder<Test1, DbConnection> queryBuilder = new(_connectionOptions);
            var query = queryBuilder.Build();
            Assert.NotNull(query.Text);
            Assert.NotEmpty(query.Text);
            Assert.NotNull(query.Columns);
            Assert.NotEmpty(query.Columns);
            Assert.NotNull(query.DatabaseManagment);
            Assert.NotNull(query.Statements);
            Assert.Null(query.Criteria);
        }
    }
}
