using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public DogsController(IDogRepository 
            dogRepository)
        {
            _dogRepo = dogRepository;
        }
        // this is getting a user by Id  and parsing the id into a string
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        [Authorize]
        public ActionResult Index()
        {
            //this is using the helper to get user Id
            int ownerId = GetCurrentUserId();
            //this is getting the dog for that owner
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);
            //this is showing the owners dog
            return View(dogs);
        }

        // GET: DogsController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: DogsController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                return View(dog);
            }
        }

        // GET: DogsController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            int ownerId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);
            // if ownerId doesnt not match the dog ownerId it will return a 404 not found 
            if (dog == null || dog.OwnerId != ownerId)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: DogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(dog);
            }
        }

        // GET: DogsController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            int ownerId = GetCurrentUserId();
            Dog dog = _dogRepo.GetDogById(id);
            // if ownerId doesnt not match the dog ownerId it will return a 404 not found 
            if (dog.OwnerId != ownerId)
            {
                return NotFound();
            }
                return View(dog);

        }
            
        

        // POST: DogsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(dog);
            }
        }

    }
}
