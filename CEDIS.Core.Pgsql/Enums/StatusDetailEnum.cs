using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Enums
{
    public enum StatusDetailEnum
    {
        Pending = 0,
        Complete = 1,
        Incomplete = 2,
        NoFind = 3,
        ErrorStock = 4,
        PendingProcess = 5
    }
}
