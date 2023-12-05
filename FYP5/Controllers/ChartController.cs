using Microsoft.AspNetCore.Mvc;

namespace FYP5.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Line()
        {
            PrepareData(0);
            ViewData["Chart"] = "line";
            ViewData["Title"] = "Total Users Per Month";
            ViewData["ShowLegend"] = "false";
            return View("Chart");
        }

        private void PrepareData(int x)
        {
            int[] totalUserData = new int[] {0, 0, 0, 0, 0, 0, 0};
            List<JiakUser> list = DBUtl.GetList<JiakUser>("SELECT * FROM JiakUser");

            foreach (JiakUser user in list)
            {

            }
        }
    }
}
