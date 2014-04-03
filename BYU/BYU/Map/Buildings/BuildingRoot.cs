using Common.WebServices;
using Common.WebServices.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Buildings
{
    static class BuildingRoot
    {
        public async static Task<ByuBuilding[]> GetAllBuildings()
        {

            WebServiceSession session = await WebServiceSession.GetSession();

            ByuBuilding[] buildings = await BYUWebServiceHelper.GetObjectFromWebService<ByuBuilding[]>(string.Format(BYUWebServiceURLs.GET_BUILDINGS), authenticate: false, allowCache: true);
            return buildings;

        }
    }
}
