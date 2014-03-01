using Bing.Maps.VenueMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class ByuMapEntity
    {

        internal ByuMapEntity(string name, string description = "", VenueEntity bingEntity = null)
        {
            Name = name;
            Description = description;

            BingEntity = bingEntity;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        internal VenueEntity BingEntity { get; private set; }
    }
}
