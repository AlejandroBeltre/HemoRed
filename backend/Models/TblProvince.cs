using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblProvince
{
    public int ProvinceId { get; set; }

    public string ProvinceName { get; set; } = null!;

    public virtual ICollection<TblAddress> TblAddresses { get; set; } = new List<TblAddress>();

    public virtual ICollection<TblMunicipality> TblMunicipalities { get; set; } = new List<TblMunicipality>();
}
