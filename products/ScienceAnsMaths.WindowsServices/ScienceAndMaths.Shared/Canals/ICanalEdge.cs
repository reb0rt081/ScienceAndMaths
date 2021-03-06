﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public interface ICanalEdge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the canal edge.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the water level in the edge
        /// </summary>
        double? WaterLevel { get; set; }
    }
}
