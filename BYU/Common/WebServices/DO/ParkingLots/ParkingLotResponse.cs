using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.ParkingLots
{
    public class ParkingLotResponse
    {
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "ParkingLotType")]
        public object ParkingLotType { get; set; }

        [DataMember(Name = "PolygonPoints")]
        public string PolygonPoints { get; set; }

        [DataMember(Name = "TypeID")]
        public int TypeID { get; set; }
    }
}
