using Kurs.Admin.Models;
using Kurs.Admin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Kurs.Admin.Controllers
{
    public class StatisticController : Controller
    {
        IKursRepository Repository;
        public StatisticController(IKursRepository repository)
        {
            Repository = repository;
        }
        // GET: Statistic
        public ActionResult Index()
        {
            var devices = Repository.Devices().GroupBy(it => it.CategoryId).Select(it => new StatisticViewModel { CategoryId = it.First().CategoryId, SoldQuantity = it.Sum(d => (d.TotalCount - d.FreeCount)) });
            var stringBuilder = new StringBuilder();
            foreach(var device in devices)
            {
                var str = $@"{{ value: {device.SoldQuantity}, color: '#a3e1d4', highlight: '#1ab394',  label: '{Repository.FindCategoryById(device.CategoryId).Title}'}},";
                stringBuilder.Append(str);
            }
            HtmlString result = new HtmlString(stringBuilder.ToString());
            return View("Index", "", result);
        }
    }
}