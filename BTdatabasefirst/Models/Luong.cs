using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class Luong
{
    public string Idnhanvien { get; set; } = null!;

    public string Thoigian { get; set; } = null!;

    public int? Luong1 { get; set; }

    public virtual Nhanvien IdnhanvienNavigation { get; set; } = null!;
}
