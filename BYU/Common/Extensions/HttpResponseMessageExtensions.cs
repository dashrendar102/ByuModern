using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        internal static string GetContentAsString(this HttpResponseMessage resp)
        {
            var task = resp.Content.ReadAsStringAsync();
            while (!task.IsCompleted) { }
            return task.Result;
        }
    }
}
