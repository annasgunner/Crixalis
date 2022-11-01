using libsCrixalis.Master;
using Grpc.Core;
using grpcCrixalis.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libsCrixalis.Protos;

namespace grpcCrixalis.Services
{
    public class KaryawanService : ReadKaryawan.ReadKaryawanBase
    {
        private readonly ILogger<KaryawanService> _logger;
        private readonly CrixalisDbContext _db;

        public KaryawanService(ILogger<KaryawanService> logger, CrixalisDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        #region 'Views'
        public override async Task<ReadAllKaryawanReply> GetAllKaryawan(ReadAllKaryawanRequest request, ServerCallContext context)
        {
            var listKaryawan = _db.T1Karyawan.AsEnumerable();

            ReadAllKaryawanReply hasil = new();
            hasil.ReadKaryawanReply.AddRange(listKaryawan.Adapt<IEnumerable<ReadKaryawanReply>>());

            return hasil;
        }

        public override async Task<ReadKaryawanReply> GetKaryawanById(ReadKaryawanByIdRequest request, ServerCallContext context)
        {
            var listKaryawan = _db.T1Karyawan
                                  .Where(w => w.IdKaryawan == Guid.Parse(request.IdKaryawan))
                                  .Select(k => new
                                  {
                                      k.IdKaryawan,
                                      k.NamaLengkap,
                                      k.NamaPanggilan,
                                      k.AlamatTinggal
                                  }).AsNoTracking().AsEnumerable().FirstOrDefault();

            ReadKaryawanReply hasil = new();
            hasil = listKaryawan.Adapt<ReadKaryawanReply>();

            return hasil;
        }
        #endregion

    }
}
