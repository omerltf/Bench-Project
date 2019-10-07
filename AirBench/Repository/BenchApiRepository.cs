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

        async public Task<List<BenchList>> GetBenchList()
        {
            ReviewRepository repo = new ReviewRepository(context);
            LoginRepository Repository = new LoginRepository(context);
            List<Bench> myBenches = await context.Benches.ToListAsync();
            List<BenchList> myBenchList = new List<BenchList>();
            foreach (var bench in myBenches)
            {
                User user = Repository.GetById(bench.CreatorUserId);
                var name = user.Name.Split(' ');
                var reviews = repo.GetReviewList(bench.Id);
                BenchList currentBench = new BenchList();
                currentBench.CreatorUserId = bench.CreatorUserId;
                if (!(name.Count() > 1))
                {
                    currentBench.Name = name[0];
                }
                else
                {
                    currentBench.Name = name[0] + ' ' + name[1].ToCharArray()[0] + '.';
                }

                //Add ellipses code here

                string description = bench.Description;
                string shortDescription = string.Empty;

                // Make sure that we have a description...
                if (!string.IsNullOrWhiteSpace(description))
                {
                    // Get a collection of the words in the description.
                    var words = description
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // If we have more than 10 words
                    // then take the first 10 and add "..." to the end
                    // otherwise just use the description as is. 
                    if (words.Length > 10)
                    {
                        shortDescription = string.Join(" ", words.Take(10)) + "...";
                    }
                    else
                    {
                        shortDescription = description;
                    }
                }


                currentBench.Description = shortDescription;
                currentBench.Latitude = bench.Latitude;
                currentBench.Longitude = bench.Longitude;
                currentBench.NumberOfSeats = bench.NumberOfSeats;
                currentBench.NumberOfReviews = reviews.Count;
                currentBench.Id = bench.Id;
                decimal sum = 0;
                foreach (var review in reviews)
                {
                    sum += (decimal)review.Rating;
                }
                if (reviews.Count != 0)
                {
                    currentBench.Rating = Math.Round((sum / reviews.Count),1);
                }
                else
                {
                    currentBench.Rating = 0;
                }
                myBenchList.Add(currentBench);
            }
            return myBenchList;

            //List<Bench> benches = await context.Benches.ToListAsync();
            //List<BenchList> benchResponses = new List<BenchList>();
            //foreach(var bench in benches)
            //{
            //    //BenchList myResponse = new BenchList();
            //    //myResponse.benchId = bench.Id;
            //    //myResponse.Description = bench.Description;
            //    //myResponse.Latitude = bench.Latitude;
            //    //myResponse.Longitude = bench.Longitude;
            //    //myResponse.NumberOfSeats = bench.NumberOfSeats;
            //    //benchResponses.Add(myResponse);





            //}
            //return benchResponses;
        }
    }
}