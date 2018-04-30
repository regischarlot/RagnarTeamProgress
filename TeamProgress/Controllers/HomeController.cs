using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ext.Net.MVC;
using System.Web.Mvc;
using TeamProgress.Models;

namespace TeamProgress.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //
        // Ajax callback: EditLegRunner()
        public DirectResult EditLegRunner(int legId, int? legRunnerId, string field, string value)
        {
            using (Status p = new Status())
                p.Update(legId, legRunnerId, field, value);
            return this.Direct();
        }

        //
        // Ajax callback: EditRunner()
        public DirectResult EditRunner(int id, string name, string displayname, float pace, string cell, string email, string emergencycontact, int type)
        {
            using (Runner p = new Runner())
                p.Update(id, name, displayname, pace, cell, email, emergencycontact, type);
            return this.Direct();
        }

        /// <summary>
        ///     AjaxData()
        /// 
        /// </summary>
        public StoreResult AjaxData()
        {
            Status p = new Status();
            p.Load(1);
            return new StoreResult { Data = p, Total = 1 };
        }
    }
}