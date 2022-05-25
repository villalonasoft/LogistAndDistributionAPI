﻿using CEDIS.Core.Pgsql.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.HubConfig
{
    public class BranchOrderHub : Hub
    {
        public Task PropagationOrders(IEnumerable<BranchOrderViewDto> orders)
        {
            return Clients.All.SendAsync("ListOrders",orders);
        }
    }
}