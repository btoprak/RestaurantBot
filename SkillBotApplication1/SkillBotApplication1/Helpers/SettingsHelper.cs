using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SkillBotApplication1
{
    public class SettingsHelper
    {
        private static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string Username
        {
            get
            {
                return GetSetting("AdminUsername");
            }
        }

        public static string Password
        {
            get
            {
                return GetSetting("AdminPassword");
            }
        }
    }
}