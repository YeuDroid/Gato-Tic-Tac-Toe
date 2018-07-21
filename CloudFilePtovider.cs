using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;

namespace Gato_Tic_Tac_Toe
{
    class CloudFilePtovider
    {

        public  static  string  ErrorTag = "ERROR";
        public static string GetZipFileFromCloud()
        {
            var url = @"http://netgar.000webhostapp.com/code.zip";
            return GetFileFromCloud(url);
        }
        public static string GetFileFromCloud()
        {
            var url = @"http://netgar.000webhostapp.com/code.js";
            return GetFileFromCloud(url);
        }

        public static string GetFileFromCloud(string url)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                var data = myWebClient.DownloadData(url);
                var txt = System.Text.Encoding.ASCII.GetString(data);

                return txt;
            }
            catch (Exception e)
            {
                return "ERROR";
            }
        }
        public string GetPath()
        { return ""; }

        public static string GetCacheFile()
        {
            try
            { }
            catch (Exception e)
            {
            }
            return "";
        }

        public static string GetEnebedFile(Context ctx)
        {
            var txt_raw = ctx.Resources.GetString(Resource.String.CODEJS);
            return Base64.Base64Decode(txt_raw);
        }
    }
    
}