using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLib.LearningSuite
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
    }
}
