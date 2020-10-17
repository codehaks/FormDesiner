using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyApp.Models;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public IMongoCollection<DataForm> Movies { get; }

        public MovieController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("formsDb");
            Movies = db.GetCollection<DataForm>("forms");
        }
        //api/movie
        [HttpGet]
        public IActionResult Get()
        {


            var movieList = Movies.Find(FilterDefinition<DataForm>.Empty).ToList();

            return Ok(movieList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DataForm model)
        {
            Movies.InsertOne(model);
            return Ok();
        }

        [HttpDelete] // REST
        public IActionResult Remove([FromBody] DataForm model)
        {
            Movies.DeleteOne(m => m.Id == model.Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] DataForm model)
        {
            Movies.ReplaceOne(m => m.Id == model.Id, model);
            return Ok();
        }

    }
}
