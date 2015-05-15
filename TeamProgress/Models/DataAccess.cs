using System;
using System.Configuration;

namespace TeamProgress.Models
{
    public class DataAccess: IDisposable
    {
        /// <summary>
        ///    Desctructor
        ///
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        ///    getConnectionString()
        ///
        /// </summary>
        public string GetConnectionString()
        {
            Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/Ragnar");
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                return rootWebConfig.ConnectionStrings.ConnectionStrings["TeamProgressConnectionString"].ConnectionString;
            return string.Empty;
        }

    }
}