using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Pengembalian
    {
        public int IdPengembalian { get; set; }
        [Required(ErrorMessage = "Harap masukkan tanggal pengembalian!")]
        public DateTime? TglPengembalian { get; set; }
        [Required(ErrorMessage = "Harap cantumkan ID Peminjaman")]
        public int? IdPeminjaman { get; set; }
        [Required(ErrorMessage = "Harap cantumkan ID Kondisi")]
        public int? IdKondisi { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Oleh Angka")]
        public int? Denda { get; set; }

        public virtual KondisiKendaraan IdKondisiNavigation { get; set; }
        public virtual Peminjaman IdPeminjamanNavigation { get; set; }
    }
}
