﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneProject_SP25_IPAS_Service.BusinessModel.SubProcessModel
{
    public class ProcessDataInSubProcessModel
    {
        public int ProcessDataId { get; set; }

        public string? ProcessDataCode { get; set; }

        public string? Input { get; set; }

        public DateTime? CreateDate { get; set; }

        public string? ResourceUrl { get; set; }

        public int? SubProcessId { get; set; }
    }
}
