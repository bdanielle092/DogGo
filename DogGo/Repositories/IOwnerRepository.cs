using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();
      Owner GetOwnerById(int id);
      Owner GetOwnerByEmail(string email);
        //you have void in front because it 
      void  AddOwner(Owner owner);
     void UpdateOwner(Owner owner);
      void DeleteOwner(int ownerId);
        
    }
}
