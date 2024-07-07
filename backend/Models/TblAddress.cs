using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TblAddress
{
    public int AddressId { get; set; }

    public int MunicipalityId { get; set; }

    public int ProvinceId { get; set; }

    public string Street { get; set; } = null!;

    public int BuildingNumber { get; set; }

    public virtual TblMunicipality Municipality { get; set; } = null!;

    public virtual TblProvince Province { get; set; } = null!;

    public virtual ICollection<TblBloodBank> TblBloodBanks { get; set; } = new List<TblBloodBank>();

    public virtual ICollection<TblCampaign> TblCampaigns { get; set; } = new List<TblCampaign>();

    public virtual ICollection<TblOrganizer> TblOrganizers { get; set; } = new List<TblOrganizer>();

    public virtual ICollection<TblUser> TblUsers { get; set; } = new List<TblUser>();
}
