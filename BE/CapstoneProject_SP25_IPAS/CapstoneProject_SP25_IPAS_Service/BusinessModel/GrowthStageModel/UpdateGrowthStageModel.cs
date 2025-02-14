﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.GrowthStageModel
{
    public class UpdateGrowthStageModel
    {
        public int GrowthStageId { get; set; }

        public string? GrowthStageName { get; set; }

        public DateTime? MonthAgeStart { get; set; }
        public DateTime? MonthAgeEnd { get; set; }
    }
}
