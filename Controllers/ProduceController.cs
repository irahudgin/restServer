using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Admin.Controllers
{
    // need to add base controller name before controller executes
    [Route("[controller]")]
    [ApiController] // characteristics: what kind of controller
    public class ProduceController : ControllerBase
    {
        // Holds connection info from remote database to local
        private readonly IConfiguration _configuration;

        // STduentController Constructor
        public ProduceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPut]
        [Route("ProduceSaleUpdate")]

        public Response ProduceSaleUpdate(Produce produce)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();

            response = dbA.UpdateProduce(con, produce);

            return response;

        }

        [HttpPost]
        [Route("AddSale")]

        public Response AddSale(Sales sale)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();


            response = dbA.AddSale(con, sale);

            return response;
        }

        [HttpGet] // the GetAllStudents API is going to generate a get request
        [Route("GetProducebyProductId/{id}")] // give api name

        public Response GetProducebyProductId(int id)

        {
            Response response = new Response();


            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));

            DBApplication dBApplication = new DBApplication();

            response = dBApplication.GetProducebyProductId(con, id);

            return response;// returning response to client
        }



        [HttpGet] // the GetAllStudents API is going to generate a get request
        [Route("GetAllProduce")] // give api name

        public Response GetAllProduce()
        {
            Response response = new Response();


            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));

            DBApplication dBApplication = new DBApplication();

            response = dBApplication.GetAllProduce(con);

            return response;// returning response to client
        }

        [HttpGet] // generate get request to database
        [Route("GetProducebyId/{id}")]

        public Response GetProducebyId(int id)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();

            // call method to search student by id
            response = dbA.GetProducebyId(con, id);

            return response;
        }

        [HttpPost]
        [Route("AddProduce")]
        public Response AddProduce(Produce produce)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();


            response = dbA.AddProduce(con, produce);

            return response;
        }

        [HttpPut]
        [Route("UpdateProduce")]

        public Response UpdateProduce(Produce produce)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();

            response = dbA.UpdateProduce(con, produce);

            return response;

        }

        [HttpDelete]
        [Route("DeleteProducebyId/{id}")]

        public Response DeleteProducebyId(int id)
        {
            Response response = new Response();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetConnectionString("produceConnection"));
            DBApplication dbA = new DBApplication();

            response = dbA.DeleteProducebyId(con, id);

            return response;
        }

    }
}
