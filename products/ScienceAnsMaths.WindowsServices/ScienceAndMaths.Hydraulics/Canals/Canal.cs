﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics.Canals
{
    public class Canal
    {
        public string Id { get; set; }

        public List<CanalEdge> CanelEdges;

        public List<CanalStretch> CanalStretches;
    }
}
