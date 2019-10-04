using AirBench.Data;
using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBench.Repository
{
    public class ReviewRepository
    {
        private Context context;

        public ReviewRepository (Context context)
        {
            this.context = context;
        }

        public List<Review> GetReviewList(int id)
        {
            return context.Reviews
                    .Where(r => r.BenchId == id)
                    .ToList();
        }

        public void InsertReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }
    }
}