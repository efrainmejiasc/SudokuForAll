using Resources.Abstract;
using Resources.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SudokuForAll.StringResx
{
    public class Resources
    {
        private static System.IO.FileInfo pathResourceFile = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/StringResx/Resources.xml"));
        private static IResourceProvider resourceProvider = new XmlResourceProvider(pathResourceFile.ToString());

      
        public static string IngreseEmail
        {
            get
            {
                return resourceProvider.GetResource("IngreseEmail", CultureInfo.CurrentUICulture.Name) as String;
            }
        }

    }
}