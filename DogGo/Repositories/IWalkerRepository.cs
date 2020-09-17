using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repository
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();

        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);

        Walker GetWalkerById(int id);
       
    }
}
