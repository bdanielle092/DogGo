﻿using DogGo.Models;
using DogGo.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkerRepository : IWalkerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public WalkerRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // Getting all the walkers
        public List<Walker> GetAllWalkers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id, Walker.[Name], ImageUrl, NeighborhoodId, Neighborhood.[Name] As neighborhoodName
                        FROM Walker
                        LEFT JOIN Neighborhood ON Walker.NeighborhoodId = Neighborhood.id
                    ";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Neighborhood neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                        };
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = neighborhood 
                        };

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }
        //getting walker by id
        public Walker GetWalkerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Walker.Id, Walker.[Name], ImageUrl, NeighborhoodId, Neighborhood.[Name] As neighborhoodName
                        FROM Walker
                        LEFT JOIN Neighborhood ON Walker.Neighborhoodid = Neighborhood.Id
                        WHERE Walker.Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Neighborhood neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                        };
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = neighborhood
                        };
                        //return walker if they have an info
                        reader.Close();
                        return walker;
                    }
                    else
                    //else retrun null
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
      
       }

        public List<Walker> GetWalkersInNeighborhood(int neighborhoodId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT Walker.Id, Walker.[Name], ImageUrl, NeighborhoodId, Neighborhood.[Name] As neighborhoodName
                   FROM Walker
                   Left JOIN Neighborhood ON Walker.NeighborhoodId = Neighborhood.Id
                   WHERE NeighborhoodId = @neighborhoodId
            ";

                    cmd.Parameters.AddWithValue("@neighborhoodId", neighborhoodId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Walker> walkers = new List<Walker>();
                    while (reader.Read())
                    {
                        Neighborhood neighborhood = new Neighborhood
                        {
                            Name = reader.GetString(reader.GetOrdinal("neighborhoodName"))
                        };
                        Walker walker = new Walker
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                            NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                            Neighborhood = neighborhood
                        };

                        walkers.Add(walker);
                    }

                    reader.Close();

                    return walkers;
                }
            }
        }
    }
}
