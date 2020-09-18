using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using DogGo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepo;
      
        public WalksController(IWalkRepository walkRepository)
        {
            _walkRepo = walkRepository;
            
            
        }

        // GET: WalksController
        public ActionResult Index()
        {
            List<Walk> walks = _walkRepo.GetAllWalks();
            return View();
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);
            if(walk == null)
            {
                return NotFound();
            }
            return View(walk);
        }

        // GET: WalksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walk walk)
        {
            try
            {
                _walkRepo.AddWalk(walk);
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View(walk);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);
            if( walk == null)
            {
                return NotFound();
            }
            return View(walk);
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Walk walk)
        {
            try
            {
                _walkRepo.UpdateWalk(walk);
                return RedirectToAction("Index");

            }
            catch(Exception)
            {
                return View(walk);
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            Walk walk = _walkRepo.GetWalkById(id);
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Walk walk)
        {
            try
            {
                _walkRepo.DeleteWalk(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(walk);
            }
        }
    }
}
