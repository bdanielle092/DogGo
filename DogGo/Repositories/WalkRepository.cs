﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IConfiguration _config;
        
        public WalkRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection
                    (_config.GetConnectionString
                    ("DefaultConnection"));
            }
        }
        //This will get all the walks 
        public List<Walk> GetAllWalks()
        {
            //opening the connection to the data
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    // Selecting the data we want 
                    cmd.CommandText = @"
                         SELECT Id, [Date], Duration, WalkId, DogId 
                  FROM Walk ";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Walk> walks = new List<Walk>();
                    while (reader.Read())
                    {
                        Walk walk = new Walk
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };
                        walks.Add(walk);
                    }
                    reader.Close();
                    return walks;
                }
            }
        }

        public Walk GetWalkById(int id)
        {
           using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                      SELECT Id, [Date], Druation, WalkerId, DogId
                       FROM Walk
                       WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if(reader.Read())
                    {
                        Walk walk = new Walk()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                            Duration = reader.GetInt32(reader.GetOrdinal("Duration")),
                            WalkerId = reader.GetInt32(reader.GetOrdinal("WalkerId")),
                            DogId = reader.GetInt32(reader.GetOrdinal("DogId"))
                        };
                        reader.Close();
                        return walk;
                    }
                    reader.Close();
                    return null;
                }
            }
        }
        public void AddWalk(Walk walk)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     INSERT INTO Walk ([Date], Duration, WalkerId, DogId) 
                   OUTPUT INSERTED.ID
                    VALUES (@date, @duration, @walkerId, @dogId);
                 ";

                    cmd.Parameters.AddWithValue("@date", walk.Date);
                    cmd.Parameters.AddWithValue("@duration", walk.Duration);
                    cmd.Parameters.AddWithValue("@walkerId", walk.WalkerId);
                    cmd.Parameters.AddWithValue("@dogId", walk.DogId);
                    int id = (int)cmd.ExecuteScalar();

                    walk.Id = id;
                }
            }
        }
        public void UpdateWalk(Walk walk)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteWalk(int walkId)
        {
            throw new System.NotImplementedException();
        }
    }
}
