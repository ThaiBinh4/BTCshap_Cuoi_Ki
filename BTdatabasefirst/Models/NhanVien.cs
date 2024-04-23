using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class NhanVien
{
    public string Idnhanvien { get; set; } = null!;

    public string? Tennhanvien { get; set; }

    public int? Tuoi { get; set; }

    public string? Idphongban { get; set; }

    public virtual Phongban? IdphongbanNavigation { get; set; }
}
