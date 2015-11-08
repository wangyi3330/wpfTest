using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
namespace Carrot.DBUtility
{
    public class UseSession : System.Web.SessionState.IReadOnlySessionState
    {
        static public string GetSession(string key)
        {
            try
            {
                return System.Web.HttpContext.Current.Session[key].ToString();
            }
            catch
            {
                return "00000000-0000-0000-0000-000000000000";
            }
        }
        static public void SetSession(string key, string value)
        {
            try
            {
                System.Web.HttpContext.Current.Session[key] = value;
            }
            catch
            {
                
            }
        }
    }
}
