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
        public IMongoCollection<Movie> Movies { get; }

        public MovieController()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("mymoviesdb");
            Movies = db.GetCollection<Movie>("movies");
        }
        //api/movie
        [HttpGet]
        public IActionResult Get()
        {


            var movieList = Movies.Find(FilterDefinition<Movie>.Empty).ToList();

            return Ok(movieList);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Movie model)
        {
            Movies.InsertOne(model);
            return Ok();
        }

        [HttpDelete] // REST
        public IActionResult Remove([FromBody] Movie model)
        {
            Movies.DeleteOne(m => m.Id == model.Id);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] Movie model)
        {
            Movies.ReplaceOne(m => m.Id == model.Id, model);
            return Ok();
        }

    }
}
