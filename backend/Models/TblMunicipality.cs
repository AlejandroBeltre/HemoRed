using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblMunicipality
{
    public int MunicipalityId { get; set; }

    public int ProvinceId { get; set; }

    public string MunicipalityName { get; set; } = null!;

    public virtual TblProvince Province { get; set; } = null!;

    public virtual ICollection<TblAddress> TblAddresses { get; set; } = new List<TblAddress>();
}
