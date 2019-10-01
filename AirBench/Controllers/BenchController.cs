using AirBench.FormModels;
using AirBench.Models;
using AirBench.Repository;
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
        public ActionResult Create()
        {
            CreateBench bench = new CreateBench();
            return View("Create", bench);
        }

        [HttpPost]
        public ActionResult Create(CreateBench bench)
        {
            BenchRepository repository = new BenchRepository(context);
            try
            {
                Bench newBench = new Bench(bench.Description, bench.NumberOfSeats, 1, bench.Latitude, bench.Longitude);
                repository.Insert(newBench);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }
            return View("Create", bench);
        }

        public ActionResult Review(int id)
        {
            CreateReview myReview = new CreateReview();
            myReview.BenchId = id;
            return View("Review",myReview);
        }
        [HttpPost]
        public ActionResult Review (CreateReview review)
        {
            BenchRepository repository = new BenchRepository(context);
            Review myReview = new Review(review.Rating, review.Description, 1, review.BenchId);
            repository.InsertReview(myReview);
            return RedirectToAction("Details");
        }

        public ActionResult Index ()
        {
            BenchRepository repository = new BenchRepository(context);
            List<Bench> myBenches = repository.GetBenchList();
            return View("Index", myBenches);
        }

        public ActionResult Details (int id)
        {
            BenchRepository repository = new BenchRepository(context);
            Bench myBench = repository.GetById(id);
            return View("Details", myBench);
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