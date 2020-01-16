using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace DemoSite.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index(int? id)
        {
            var model = new Models.HomeModel();

            model.RegulationsJSON = await FetchRegulationData(id);

            return View(model);
        }

        public async Task<string> FetchRegulationData(int? id)
        {
            if(id == null)
            {
                id = 108;
            }

            var apiUrl = "https://rules.sos.ri.gov/regulations/api_get_rules_by_org_id_and_keyword/active/" + id.ToString();

            using (var hc = new HttpClient { Timeout = TimeSpan.FromMilliseconds(1000 * 20) })
            {
                var response = await hc.GetAsync(apiUrl);
                var res = await response.Content.ReadAsStringAsync();
     
                return res;
            }
        }


    }
}