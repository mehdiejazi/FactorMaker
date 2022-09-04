using Flurl;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Common
{
    public class MyServer
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public MyServer(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string MapPath(string path)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string ret = Url.Combine(contentRootPath, path);
            
            //string ret = Path.Combine(
            //    AppDomain.CurrentDomain.GetData("ContentRootPath").ToString(), path);

            return ret;
        }
    }
}
