﻿using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        List<Dog> GetDogsByOwnerId(int ownerId);

        Dog GetDogById(int id);
       
        void AddDog(Dog newDog);
        void UpdateDog(Dog dog);
        void DeleteDog(int dogId);
    }
}
