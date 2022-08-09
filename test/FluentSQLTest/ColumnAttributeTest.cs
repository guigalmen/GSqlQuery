﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentSQLTest
{
    public class ColumnAttributeTest
    {
        [Theory]
        [InlineData("Default")]
        [InlineData("My")]
        public void Default_values_with_column_name_in_the_Constructor(string name)
        {
            ColumnAttribute column = new(name);

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.False(column.IsPrimaryKey);
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(0, column.Size);
            Assert.Equal(name, column.Name);
        }

        [Theory]
        [InlineData("Default",45)]
        [InlineData("My",69)]
        public void Default_values_with_column_name_and_size_in_the_Constructor(string name,int size)
        {
            ColumnAttribute column = new(name, size);

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.False(column.IsPrimaryKey);
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
        }

        [Theory]
        [InlineData("Default", 45,true)]
        [InlineData("My", 69,false)]
        public void Default_values_with_column_name_size_and_isprimarykey_in_the_Constructor(string name, int size,bool isPrimaryKey)
        {
            ColumnAttribute column = new(name, size,isPrimaryKey);

            Assert.NotNull(column);
            Assert.NotNull(column.Name);            
            Assert.False(column.IsAutoIncrementing);
            Assert.Equal(isPrimaryKey,column.IsPrimaryKey);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
        }

        [Theory]
        [InlineData("Default", 45, true,false)]
        [InlineData("My", 69, false,true)]
        public void Default_values_with_column_name_size_isprimarykey_and_isautoincrementing_in_the_Constructor(string name, int size, bool isPrimaryKey, bool isAutoIncrementing)
        {
            ColumnAttribute column = new(name, size, isPrimaryKey, isAutoIncrementing);

            Assert.NotNull(column);
            Assert.NotNull(column.Name);
            Assert.Equal(isAutoIncrementing, column.IsAutoIncrementing);
            Assert.Equal(isPrimaryKey, column.IsPrimaryKey);
            Assert.Equal(size, column.Size);
            Assert.Equal(name, column.Name);
        }
    }
}
