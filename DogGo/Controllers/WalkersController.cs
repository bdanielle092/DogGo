﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Repositories;
using DogGo.Models;
using DogGo.Repository;
using DogGo.Models.ViewModels;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalkRepository walkRepository, INeighborhoodRepository neighborhoodRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
        // GET: WalkerController
        public ActionResult Index()
        {
            try
            {
                // if you are the current user it will show the walkers in your neighborhood
                int ownerId = GetCurrentUserId();
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(ownerId);

                return View(walkers);
            }
            catch
            {
                // if you are not login it will show you a list of walkers
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();
                return View(allWalkers);
             }
        }

        // GET: WalkerController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };
            return View(vm);
        }

        // GET: WalkerController/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods =
               _neighborhoodRepo.GetAll();
            WalkerFormViewModel vm = new WalkerFormViewModel()
            {
                Walker = new Walker(),
                Neighborhoods = neighborhoods
            };
            return View(vm);
        }
        // POST: DogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walker walker)
        {
            try
            {

                _walkerRepo.AddWalker(walker);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                
                return View(walker);
            }
        }


        // GET: WalkerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkerController/Delete/5
        public ActionResult Delete(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            return View(walker);
        }

        // POST: WalkerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walker walker)
        {
            try
            {
                _walkerRepo.DeleteWalker(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(walker);
            }
        }
    }
}
