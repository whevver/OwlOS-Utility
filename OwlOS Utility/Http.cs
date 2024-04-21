using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OwlOS_Utility
{
    internal class Http
    {
        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using WebClient webClient = new();
            return webClient.UploadValues(url, pairs);
        }
    }
}
