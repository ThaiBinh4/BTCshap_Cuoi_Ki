using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class Nhanvien
{
    public string Idnv { get; set; } = null!;

    public string? Tennhanvien { get; set; }

    public string? Idpb { get; set; }

    public string? Idcv { get; set; }

    public DateTime? Ngaysinh { get; set; }

    public string? Sdt { get; set; }
    public string? Avatar { get; set; }

    public virtual Chucvu? IdcvNavigation { get; set; }

    public virtual Phongban? IdpbNavigation { get; set; }

    public virtual ICollection<Luong> Luongs { get; set; } = new List<Luong>();
}
