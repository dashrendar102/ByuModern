using Bing.Maps.VenueMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ByuMapEntity
    {

        internal ByuMapEntity(string name, string description = "", VenueEntity bingEntity = null)
        {
            Name = name;
            Description = description;

            BingEntity = bingEntity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public VenueEntity BingEntity { get; set; }
    }
}
