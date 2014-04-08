using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Buildings
{
    [DataContract]
    public class ByuBuilding
    {
        [DataMember(Name = "Acronym")]
        public string Acronym { get; set; }

        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "Latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "Longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "LegendKey")]
        public int LegendKey { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "ShortDescription")]
        public string ShortDescription { get; set; }
    }
}
