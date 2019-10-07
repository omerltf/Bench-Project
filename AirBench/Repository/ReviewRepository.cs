using AirBench.Data;
using AirBench.Models;
using System.Data.Entity;
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
                    .Include(r => r.User)
                    .Where(r => r.BenchId == id)
                    .OrderByDescending(r => r.CreatedOn)
                    .ToList();
        }

        public void InsertReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }
    }
}