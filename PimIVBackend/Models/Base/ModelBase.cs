using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Models.Base
{
    public abstract class ModelBase
    {
        public DateTime DateAdd { get; set; }
        public DateTime DateUp { get; set; }
    }
}
