using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paychoice.API.Client
{
    [Serializable]
    public enum ChargeStatus : int
    {
        Approved = 0,
        ApprovedWithErrors = 1,
        Dishonoured = 5,
        Error = 6,
        Processing = 9,
        Voided = 99
    }
}
