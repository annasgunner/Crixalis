using Grpc.Core;
using grpcCrixalis.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcCrixalis.Utility
{
    public class ClsErrorHandling
    {
        public void CheckCancelRequest(ServerCallContext context, CrixalisDbContext db)
        {
            var contextCancellationToken = context.CancellationToken;

            if (contextCancellationToken.IsCancellationRequested)
            {
                db.Database.RollbackTransaction();
                var metadata = new Metadata { { "error", "Request cancelled by User!" } };
                throw new RpcException(new Status(StatusCode.Cancelled, "Request cancelled by User!"), metadata);
            }
        }

    }
}
