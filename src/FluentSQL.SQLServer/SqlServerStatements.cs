﻿using FluentSQL.Default;

namespace FluentSQL.SQLServer
{
    public  class SqlServerStatements : Statements
    {
        public override string Format => "[{0}]";

        public override string ValueAutoIncrementingQuery => "SELECT SCOPE_IDENTITY();";
    }
}
