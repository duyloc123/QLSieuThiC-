namespace QuanLyCuaHangDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhapHang")]
    public partial class ChiTietPhieuNhapHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string maPhieuNhap { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string maHang { get; set; }

        [Required]
        [StringLength(20)]
        public string maNCC { get; set; }

        public double soLuongNhap { get; set; }

        public double thanhTien { get; set; }

        public double giaNhap { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual PhieuNhapHang PhieuNhapHang { get; set; }
    }
}
