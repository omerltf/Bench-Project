using AirBench.Data;
using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Repository
{
    public class BenchRepository
    {
        private Context context;

        public BenchRepository(Context context)
        {
            this.context = context;
        }

        public void InsertReview (Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }

        public void Insert(Bench bench)
        {
            context.Benches.Add(bench);
            context.SaveChanges();
        }

        public Bench GetById(int id)
        {
            return  context.Benches
                        .Where(b => b.Id == id)
                        .SingleOrDefault();
        }

        public List<Bench> GetBenchList()
        {
            return context.Benches.ToList();
        }
    }
}