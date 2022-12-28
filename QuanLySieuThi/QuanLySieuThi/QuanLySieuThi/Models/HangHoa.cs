namespace QuanLyCuaHangDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangHoa")]
    public partial class HangHoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangHoa()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            ChiTietPhieuNhapHangs = new HashSet<ChiTietPhieuNhapHang>();
            ChiTietPhieuXuatHangs = new HashSet<ChiTietPhieuXuatHang>();
        }

        [Key]
        [StringLength(20)]
        public string maHang { get; set; }

        [Required]
        [StringLength(20)]
        public string maNCC { get; set; }

        [Required]
        [StringLength(20)]
        public string maLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string tenHang { get; set; }

        public DateTime ngaySanXuat { get; set; }

        public DateTime? ngayHetHan { get; set; }

        [StringLength(200)]
        public string Anh { get; set; }

        public double soLuong { get; set; }

        public double giaTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhapHang> ChiTietPhieuNhapHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get; set; }

        public virtual LoaiHangHoa LoaiHangHoa { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
