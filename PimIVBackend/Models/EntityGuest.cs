using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PimIVBackend.Models
{
    public class EntityGuest : Entity
    {
        public Gender EntityGender { get; set; }

        public enum Gender
        {
            NotDefined = 0,
            M = 1,
            F = 2
        }
    }
}
