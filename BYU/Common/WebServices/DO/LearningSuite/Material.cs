using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class Material
    {
        [DataMember]
        public string id;

        [DataMember]
        public string author;

        [DataMember]
        public string description;

        [DataMember]
        public string displayOrder;

        [DataMember]
        public string edition;

        [DataMember]
        public string image;

        [DataMember]
        public string isbn;

        [DataMember]
        public string link;

        [DataMember]
        public string newPrice;

        [DataMember]
        public string publishedDate;

        [DataMember]
        public string publisher;

        [DataMember]
        public bool required;

        [DataMember]
        public string syllabusID;

        [DataMember]
        public string title;

        [DataMember]
        public string usedPrice;

        [DataMember]
        public string vendor;

        public static Material[] GetMaterials(string courseId)
        {
            return BYUWebServiceHelper.GetObjectFromWebService<Material[]>(string.Format(BYUWebServiceURLs.GET_MATERIALS, courseId));
        }
    }
}
