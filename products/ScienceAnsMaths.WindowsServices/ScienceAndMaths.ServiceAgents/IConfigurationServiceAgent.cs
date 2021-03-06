﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.ServiceAgents
{
    public interface IConfigurationServiceAgent
    {
        ICanal LoadCanalConfiguration(string file);
    }
}
