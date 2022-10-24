using MassTransit;
using Pantheon.Bases;
using System.ComponentModel.DataAnnotations;

namespace libsCrixalis.Master
{
    public class DbT1Karyawan : BaseModelMasterHeader
    {
        [Key]
        public Guid IdKaryawan { get; set; } = NewId.NextGuid();
        public Guid? IdDivisiPerusahaan { get; set; }
        public Guid? IdJabatan { get; set; }
        public Guid? IdJadwalKerjaKaryawan { get; set; }
        public string? NamaLengkap { get; set; }
        public string? NamaPanggilan { get; set; }
        public string? NomorIdentitas1 { get; set; }
        public string? NomorIdentitas2 { get; set; }
        public string? JenisKelamin { get; set; }
        public string? TempatLahir { get; set; }
        public DateTime? TanggalLahir { get; set; }
        public DateTime? TanggalRekrut { get; set; }
        public DateTime? TanggalBerhenti { get; set; }
        public string? AlamatAsal { get; set; }
        public Guid? IdKotaAsal { get; set; }
        public Guid? IdKecamatanAsal { get; set; }
        public Guid? IdKelurahanAsal { get; set; }
        public string? KodePosAsal { get; set; }
        public string? AlamatTinggal { get; set; }
        public Guid? IdKotaTinggal { get; set; }
        public Guid? IdKecamatanTinggal { get; set; }
        public Guid? IdKelurahanTinggal { get; set; }
        public string? KodePosTinggal { get; set; }
        public string? Telepon { get; set; }
        public string? Seluler1 { get; set; }
        public string? Seluler2 { get; set; }
        public string? Email { get; set; }
        public string? KomunikasiLain { get; set; }
        public string? NamaKerabat { get; set; }
        public string? HubunganKerabat { get; set; }
        public string? AlamatKerabat { get; set; }
        public string? TeleponKerabat { get; set; }
        public string? Rekening { get; set; }
        public string? BPJS { get; set; }
        public string? NPWP { get; set; }
        public string? DokumenJaminan { get; set; }
        public string? Keterangan { get; set; }
        public string? UserName { get; set; }
        public byte[]? UserPassword { get; set; }
        public byte[]? PasswordHint { get; set; }
        public bool? StatusLogin { get; set; }
        public string? NIK { get; set; }
        public byte[]? FingerPrint0 { get; set; }
        public byte[]? FingerPrint1 { get; set; }
        public byte[]? FingerPrint2 { get; set; }
        public byte[]? FingerPrint3 { get; set; }
        public byte[]? FingerPrint4 { get; set; }
        public byte[]? FingerPrint5 { get; set; }
        public byte[]? FingerPrint6 { get; set; }
        public byte[]? FingerPrint7 { get; set; }
        public byte[]? FingerPrint8 { get; set; }
        public byte[]? FingerPrint9 { get; set; }
        public string? CardIdNumber { get; set; }
        public string? CardIdPin { get; set; }
        public string? PKLink1 { get; set; }
        public string? PKLink2 { get; set; }
    }
}