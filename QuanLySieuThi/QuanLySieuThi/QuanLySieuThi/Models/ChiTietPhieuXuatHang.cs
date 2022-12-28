namespace QuanLyCuaHangDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuXuatHang")]
    public partial class ChiTietPhieuXuatHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string maPhieuXuat { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string maHang { get; set; }

        public double soLuongXuat { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual PhieuXuatHang PhieuXuatHang { get; set; }
    }
}
