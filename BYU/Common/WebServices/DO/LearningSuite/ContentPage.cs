using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.LearningSuite
{
    [DataContract]
    public class ContentPage
    {
        [DataMember]
        public string id;

        [DataMember]
        public ContentPage[] children;

        [DataMember]
        public string content;

        [DataMember]
        public string contentType;

        [DataMember]
        public string courseID;

        [DataMember]
        public string title;

        public async static Task<ContentPage[]> GetContentPages(string courseId)
        {
            return await BYUWebServiceHelper.GetObjectFromWebService<ContentPage[]>(BYUWebServiceURLs.GET_CONTENT_PAGES + courseId);
        }
    }
}
