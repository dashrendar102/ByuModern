﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceDemos
{
    public class JsonUtils
    {
        public static string prettifyJson(string unformattedJson)
        {
            if (string.IsNullOrEmpty(unformattedJson))
            {
                return "";
            }
            object parsedObj = JsonConvert.DeserializeObject(unformattedJson);
            string prettyJsonStr = JsonConvert.SerializeObject(parsedObj, Formatting.Indented);
            return prettyJsonStr;
        }

        public static string prettifyJson(WebResponse response)
        {
            if (response.ContentType.Contains("application/json"))
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                var unformattedJson = reader.ReadToEnd();
                return prettifyJson(unformattedJson);
            }
            return "";
        }
    }
}
