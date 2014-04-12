using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace Common
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

        public static string ExtractAndPrettifyHTMLText(string sourceHTML)
        {
            return RemoveTabs(RetrieveTextFromHTML(sourceHTML)).Trim();
        }

        public static string LimitToLengthOnWordBoundaries(string str, int maxLength)
        {
            if (str == null || str.Length <= maxLength)
            {
                return str;
            }
            int truncationIndex = Math.Min(maxLength, str.Length) - 1;
            while (truncationIndex > 0 && !Char.IsWhiteSpace(str[truncationIndex]))
            {
                truncationIndex--;
            }
            string result = str.Substring(0, truncationIndex + 1).Trim();
            result += "...";
            return result;
        }

        public static string[] GetWordsToLower(string str)
        {
            if (!String.IsNullOrWhiteSpace(str))
            {
                //Remove anything in parenthesis
                str = Regex.Replace(str, @"\(.*\)", String.Empty);
                str = str.Trim();
                return str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.ToLower()).ToArray();
            }
            else return new string[0];
        }
    }
}
