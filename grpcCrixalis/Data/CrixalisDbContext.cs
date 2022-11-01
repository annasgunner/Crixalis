using libsCrixalis.Master;
using Microsoft.EntityFrameworkCore;
using Pantheon.Bases.BaseBlazorServer.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpcCrixalis.Data
{
    public class CrixalisDbContext : pthDbContext
    {
        public CrixalisDbContext(DbContextOptions options) : base(options) { }

        public DbSet<DbT0Jabatan> T0Jabatan { get; set; }
        public DbSet<DbT1Karyawan> T1Karyawan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
