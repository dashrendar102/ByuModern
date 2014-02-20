﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationLib.LearningSuite
{
    [DataContract]
    public class SystemAnnouncement
    {
        [DataMember]
        public string id;

        [DataMember]
        public string begindate;

        [DataMember]
        public string createdBy;

        [DataMember]
        public string dateupdated;

        [DataMember]
        public string expiredate;

        [DataMember]
        public string text;

        [DataMember]
        public string title;

        [DataMember]
        public string type;

        [DataMember]
        public string typeName;

        [DataMember]
        public string updatedBy;
    }
}
