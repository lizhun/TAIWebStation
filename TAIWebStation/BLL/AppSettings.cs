using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace BLL
{
    public class AppSettings
    {
        public static string PartnerId =>
         ConfigurationManager.AppSettings[nameof(PartnerId)];
        public static string Token =>
    ConfigurationManager.AppSettings[nameof(Token)];
        public static string TAIBaseUrl =>
    ConfigurationManager.AppSettings[nameof(TAIBaseUrl)];
        public static string DBBaseUrl =>
ConfigurationManager.AppSettings[nameof(DBBaseUrl)];
        public static string FrontServerBaseUrl =>
ConfigurationManager.AppSettings[nameof(FrontServerBaseUrl)];
    }
}
