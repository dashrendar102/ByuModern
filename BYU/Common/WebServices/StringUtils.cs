using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace Common.WebServices
{
    public static class StringUtils
    {
        public static string RemoveTabs(string str)
        {
            return str.Replace("\t", "");
        }

        public static string RemoveHTMLTags(string sourceHTML)
        {
            char[] array = new char[sourceHTML.Length];
            int arrayIndex = 0;
            bool inside = false;
            for (int i = 0; i < sourceHTML.Length; i++)
            {
                char let = sourceHTML[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        //http://stackoverflow.com/questions/4878452/remove-html-tags-in-string
        public static string RetrieveTextFromHTML(string sourceHTML)
        {
            string taglessHTML = RemoveHTMLTags(sourceHTML);
            return WebUtility.HtmlDecode(taglessHTML);
        }
    }
}
