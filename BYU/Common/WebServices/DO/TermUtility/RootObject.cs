﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.WebServices.DO.TermUtility
{
    [DataContract]
    internal class RootObject
    {
        [DataMember]
        public ControldateswsService ControldateswsService { get; set; }
    }
}
