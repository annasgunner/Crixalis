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

        public DbSet<DbT1Karyawan> T1KaryawanDbSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //T1Karyawan
            modelBuilder.Entity<DbT1Karyawan>(entity =>
            {
                entity.ToTable("T1Karyawan");
                entity.HasKey(e => e.IdKaryawan);
            });
        }
    }
}
