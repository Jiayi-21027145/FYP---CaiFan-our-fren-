using Microsoft.AspNetCore.Mvc;
using FYP5.Models;
namespace FYP5.Controllers;
public class ChartController : Controller
{
    public IActionResult Bar()
    {
        PrepareData(1);
        ViewData["Chart"] = "bar";
        ViewData["Title"] = "Fitness Summary";
        ViewData["ShowLegend"] = "false";
        return View("Chart");
    }


    private void PrepareData(int x)
    {
        int[] dataAvg = new int[] { 0, 0, 0, 0, 0 };
       
        List<History> list = DBUtl.GetList<History>("SELECT * FROM History");
        foreach (History his in list)
        {
            dataAvg[CalcGrade(his.AverageCalories)]++;
         
        }

        string[] colors = new[] {"lightgreen", "pink"};
        string[] grades = new[] { "A", "B" };
        ViewData["Legend"] = "History";
        ViewData["Colors"] = colors;
        ViewData["Labels"] = grades;
        if (x == 1)
            ViewData["Data"] = dataAvg;
        
    }

    private int CalcGrade(int c)
    {
        if (c >= 700) return 1;
      
        else return 0;
    }

}