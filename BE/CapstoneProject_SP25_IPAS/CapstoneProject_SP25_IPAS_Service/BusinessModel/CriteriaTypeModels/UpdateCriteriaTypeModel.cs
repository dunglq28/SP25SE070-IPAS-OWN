﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.CriteriaTypeModels
{
    public class UpdateCriteriaTypeModel
    {
        public int CriteriaTypeId { get; set; }

        public string? CriteriaTypeName { get; set; }

        public int? GrowthStageID { get; set; }
        public List<UpdateCriteriaModel>? ListUpdateCritera { get; set; } = new List<UpdateCriteriaModel>();
    }
}
