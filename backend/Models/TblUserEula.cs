using System;
using System.Collections.Generic;
using backend.DTO;

namespace backend.Models;

public partial class TblUserEula
{
    public int EulaId { get; set; }

    public string UserDocument { get; set; } = null!;

    public bool AcceptedStatus { get; set; }

    public virtual TblEula Eula { get; set; } = null!;

    public virtual TblUser UserDocumentNavigation { get; set; } = null!;

    public static TblUserEula FromDto(UserEulaDto userEulaDto)
    {
        return new TblUserEula()    
        {
            AcceptedStatus = userEulaDto.AcceptedStatus,
            EulaId = userEulaDto.EulaID,
            UserDocument = userEulaDto.UserDocument
        };
    }
}
