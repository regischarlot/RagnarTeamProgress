using System.Web.Mvc;
using Ext.Net.MVC;
using TeamProgress.Models;

namespace TeamProgress.Controllers
{
    public class HomeController : Controller
    {


        //
        // GET: /Home/Index/
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Home/Index/5
        [HttpPost]
        public ActionResult Index(FormCollection collection)
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
