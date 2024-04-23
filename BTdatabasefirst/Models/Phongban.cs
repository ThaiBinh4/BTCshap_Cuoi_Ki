using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class Phongban
{
    public string Idphongban { get; set; } = null!;

    public string? Tenphongban { get; set; }

    public string? Idcongty { get; set; }

    public virtual Congty? IdcongtyNavigation { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
