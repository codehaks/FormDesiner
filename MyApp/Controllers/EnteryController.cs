using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnteryController : Controller
    {


        public IMongoCollection<DataForm> DataForms { get; }

        public EnteryController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("formsDb");
            DataForms = db.GetCollection<DataForm>("forms");
        }
        //api/movie
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            var form = DataForms.Find(f => f.Id == id).First();
            if (form.Enteries==null)
            {
                form.Enteries = new List<Entery>();
            }

            var enteries = form.Enteries.ToList();

            return Ok(enteries);
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnteryAddModel model)
        {
            var form = DataForms.Find(f => f.Id == model.FormId).First();

            if (form.Enteries == null)
            {
                form.Enteries = new List<Entery>();
            }

           
            form.Enteries.Add(new Entery
            {
                Name=model.Name
            });

            DataForms.ReplaceOne(f => f.Id == model.FormId, form);
            return Ok(model);
        }

        [HttpDelete] // REST
        public IActionResult Remove([FromBody] DataForm model)
        {
            DataForms.DeleteOne(m => m.Id == model.Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] DataForm model)
        {
            DataForms.ReplaceOne(m => m.Id == model.Id, model);
            return Ok();
        }
    }

    public class EnteryAddModel
    {
        public string FormId { get; set; }
        public string Name { get; set; }

    }

}
