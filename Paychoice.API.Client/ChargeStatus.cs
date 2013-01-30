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
        Pending = 1,
        Errored = 2,
        Dishonoured = 3
    }
}