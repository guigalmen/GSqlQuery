﻿namespace GSqlQuery
{
    public class Join<T1, T2, T3> : Join<T1, T2>
        where T1 : class, new() where T2 : class, new() where T3 : class, new()
    {
        public T3 Table3 { get; set; }

        public Join()
        {

        }

        public Join(T1 table1, T2 table2, T3 table3) : base(table1, table2)
        {
            Table3 = table3;
        }
    }
}
