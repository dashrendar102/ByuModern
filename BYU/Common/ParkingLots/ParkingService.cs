using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.ParkingLots
{

    [DataContract(Name = "ParkingService")]
    public class ParkingService
    {
        [DataMember(Name = "request")]
        public ParkingLotRequest request { get; set; }

        [DataMember(Name = "response")]
        public ParkingLotResponse response { get;  set; }
    }
}
