using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace RentalKendaraan.Models
{
    public partial class Peminjaman
    {
        public Peminjaman()
        {
            Pengembalians = new HashSet<Pengembalian>();
        }

        public int IdPeminjaman { get; set; }

        [Required(ErrorMessage = "Harap masukkan tanggal peminjaman")]
        public DateTime? TglPeminjaman { get; set; }
        [Required(ErrorMessage = "Harap cantumkan ID Kendaraan")]
        public int? IdKendaraan { get; set; }
        [Required(ErrorMessage = "Harap cantumkan ID Customer")]
        public int? IdCostumer { get; set; }
        [Required(ErrorMessage = "Harap cantumkan ID Jaminan")]
        public int? IdJaminan { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Oleh Angka")]
        public int? Biaya { get; set; }

        public virtual Customer IdCostumerNavigation { get; set; }
        public virtual Jaminan IdJaminanNavigation { get; set; }
        public virtual Kendaraan IdKendaraanNavigation { get; set; }
        public virtual ICollection<Pengembalian> Pengembalians { get; set; }
    }
}
