﻿using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;

namespace GSqlQuery.SQLServer
{
    public interface ISqlBulkCopyExecute : IBulkCopyExecute, ISqlBulkCopy, IExecute<int, SqlConnection>
    {
        int Execute(SqlBulkCopyOptions sqlBulkCopyOptions);

        int Execute(SqlBulkCopyOptions sqlBulkCopyOptions, SqlTransaction sqlTransaction);

        Task<int> ExecuteAsync(SqlBulkCopyOptions sqlBulkCopyOptions, CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(SqlBulkCopyOptions sqlBulkCopyOptions, SqlTransaction sqlTransaction, CancellationToken cancellationToken = default);

    }
}
