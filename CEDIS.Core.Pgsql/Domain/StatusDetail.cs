using CEDIS.Core.Pgsql.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CEDIS.Core.Pgsql.Domain
{
    public class StatusDetail
    {
        public StatusDetailEnum Id { get; set; }
        public string Name { get; set; }
    }
}
