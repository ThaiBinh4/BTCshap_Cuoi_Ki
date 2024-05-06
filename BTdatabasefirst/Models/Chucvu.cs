using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class Chucvu
{
    public string Idcv { get; set; } = null!;

    public string? Tenchucvu { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
