using System;
using System.Collections.Generic;
using backend.DTO;

namespace backend.Models;

public partial class TblEula
{
    public int EulaId { get; set; }

    public string Version { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string Content { get; set; } = null!;

    public virtual ICollection<TblUserEula> TblUserEulas { get; set; } = new List<TblUserEula>();
    
    public static TblEula FromDto(EulaDto eulaDto)
    {
        return new TblEula()
        {
            Content= eulaDto.Content,
            Version= eulaDto.Version,
            UpdateDate = eulaDto.UpdateDate
        };
    }
}
