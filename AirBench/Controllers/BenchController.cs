using AirBench.FormModels;
using AirBench.Models;
using AirBench.Repository;
using AirBench.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AirBench.Controllers
{
    public class BenchController : BaseController
    {
        [Authorize]
        public ActionResult Create()
        {
            CreateBench bench = new CreateBench();
            return View("Create", bench);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateBench bench)
        {
            CustomPrincipal currentUser = (CustomPrincipal)User;
            BenchRepository repository = new BenchRepository(context);
            try
            {
                Bench newBench = new Bench(bench.Description, bench.NumberOfSeats, currentUser.User.Id, bench.Latitude, bench.Longitude);
                repository.Insert(newBench);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }
            return View("Create", bench);
        }

        [Authorize]
        public ActionResult Review(int id)
        {
            CreateReview myReview = new CreateReview();
            myReview.BenchId = id;
            return View("Review",myReview);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Review (CreateReview review)
        {
            CustomPrincipal currentUser = (CustomPrincipal)User;
            if (ModelState.IsValid)
            {
                BenchRepository repository = new BenchRepository(context);
                Review myReview = new Review(review.Rating, review.Description, currentUser.User.Id, review.BenchId);
                repository.InsertReview(myReview);
                return RedirectToAction("Index");
            }
            return View(review);
        }

        public ActionResult Index ()
        {
            BenchRepository repository = new BenchRepository(context);
            List<Bench> myBenches = repository.GetBenchList();
            List<BenchList> myBenchList = new List<BenchList>();
            foreach(var bench in myBenches)
            {
                var reviews = repository.GetReviewList(bench.Id);
                BenchList currentBench = new BenchList();
                currentBench.CreatorUserId = bench.CreatorUserId;
                currentBench.Description = bench.Description;
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
                    currentBench.Rating = sum / reviews.Count;
                }
                else
                {
                    currentBench.Rating = 0;
                }
                myBenchList.Add(currentBench);
            }

            return View("Index", myBenchList);
        }

        public ActionResult Details (int id)
        {
            BenchRepository repository = new BenchRepository(context);
            LoginRepository repo = new LoginRepository(context);
            Bench myBench = repository.GetById(id);
            User thisUser = repo.GetById(myBench.CreatorUserId);
            BenchDetails myBenchDetails = new BenchDetails();
            myBenchDetails.CreatorUserId = myBench.CreatorUserId;
            myBenchDetails.CreatorUserName = thisUser.UserName;
            myBenchDetails.Description = myBench.Description;
            myBenchDetails.Latitude = myBench.Latitude;
            myBenchDetails.Longitude = myBench.Longitude;
            myBenchDetails.NumberOfSeats = myBench.NumberOfSeats;
            myBenchDetails.reviews = repository.GetReviewList(id);
            myBenchDetails.Id = myBench.Id;

            return View("Details", myBenchDetails);
        }

        private void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                SqlException sqlException = ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
        }
    }
}