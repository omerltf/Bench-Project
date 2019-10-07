using AirBench.Data;
using AirBench.FormModels;
using AirBench.Models;
using AirBench.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace AirBench.Controllers
{
    [RoutePrefix("api/Bench")]
    public class BenchWebApiController : ApiController
    {
        private IBenchApiRepository Repository;
        Context context = new Context();

        public BenchWebApiController()
        {
            Repository = new BenchApiRepository(context);
        }

        public BenchWebApiController(IBenchApiRepository repo)
        {
            Repository = repo;
        }
        [Route("Get")]
        async public Task<BenchResponse> Get()
        {
            List<BenchList> benchList = await Repository.GetBenchList();
            BenchResponse myResponse = new BenchResponse();
            myResponse.benchList = benchList;
            return myResponse;
        }
    }
}