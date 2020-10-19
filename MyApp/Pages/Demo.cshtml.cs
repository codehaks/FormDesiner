using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using MyApp.Models;

namespace MyApp.Pages
{
    public class DemoModel : PageModel
    {
        public IMongoCollection<DataForm> DataForms { get; }

        public DemoModel()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("formsDb");
            DataForms = db.GetCollection<DataForm>("forms");
        }

        public string Id { get; set; }
        public List<Entery> Enteries { get; set; }
        public void OnGet(string id)
        {
            Id = id;

            var form = DataForms.Find(f => f.Id == id).First();
            if (form.Enteries == null)
            {
                form.Enteries = new List<Entery>();
            }

            Enteries = form.Enteries.ToList();

        }
    }
}
