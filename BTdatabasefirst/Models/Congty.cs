using System;
using System.Collections.Generic;

namespace BTdatabasefirst.Models;

public partial class Congty
{
    public string Idcty { get; set; } = null!;

    public string? Tencty { get; set; }

    public virtual ICollection<Phongban> Phongbans { get; set; } = new List<Phongban>();
}
