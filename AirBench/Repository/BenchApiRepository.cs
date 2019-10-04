using AirBench.Data;
using AirBench.FormModels;
using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AirBench.Repository
{
    public class BenchApiRepository : IBenchApiRepository
    {
        private Context context;

        public BenchApiRepository (Context context)
        {
            this.context = context;
        }

        //async public Task<Bench> GetById(int id)
        //{
        //    Bench myBench = context.Benches
        //                    .Where(b => b.Id == id)
        //                    .SingleOrDefault();
        //    return myBench;
        //}

        async public Task<List<BenchInfo>> GetBenchList()
        {
            List<Bench> benches = await context.Benches.ToListAsync();
            List<BenchInfo> benchResponses = new List<BenchInfo>();
            foreach(var bench in benches)
            {
                BenchInfo myResponse = new BenchInfo();
                myResponse.Description = bench.Description;
                myResponse.Latitude = bench.Latitude;
                myResponse.Longitude = bench.Longitude;
                myResponse.NumberOfSeats = bench.NumberOfSeats;
                benchResponses.Add(myResponse);
            }
            return benchResponses;
        }
    }
}