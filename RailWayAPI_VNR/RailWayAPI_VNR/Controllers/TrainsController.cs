using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RailWayAPI_VNR.Models;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace RailWayAPI_VNR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private readonly string connectionString;

        public TrainsController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("SqlServerDb");
        }

        [HttpPost]
        public IActionResult CreateTrain(TrainsDTO trainDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO Trains (TrainId, TrainName, StartLocation, EndLocation, StartTime, EndTime, Capacity, Distance, Price) 
                           VALUES (@TrainId, @TrainName, @StartLocation, @EndLocation, @StartTime, @EndTime, @Capacity, @Distance, @Price)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TrainId", trainDTO.TrainId);
                        command.Parameters.AddWithValue("@TrainName", trainDTO.TrainName);
                        command.Parameters.AddWithValue("@StartLocation", trainDTO.StartLocation);
                        command.Parameters.AddWithValue("@EndLocation", trainDTO.EndLocation);
                        command.Parameters.AddWithValue("@StartTime", trainDTO.StartTime);
                        command.Parameters.AddWithValue("@EndTime", trainDTO.EndTime);
                        command.Parameters.AddWithValue("@Capacity", trainDTO.Capacity);
                        command.Parameters.AddWithValue("@Distance", trainDTO.Distance);
                        command.Parameters.AddWithValue("@Price", trainDTO.Price);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return BadRequest("An exception occurred: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetTrains()
        {
            List<Trains> trains = new List<Trains>();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Trains";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Trains train = new Trains
                                    {
                                        TrainId = reader.GetInt32(0),
                                        TrainName = reader.GetString(1),
                                        StartLocation = reader.GetString(2),
                                        EndLocation = reader.GetString(3),
                                        StartTime = reader.GetTimeSpan(4),
                                        EndTime = reader.GetTimeSpan(5),
                                        Capacity = reader.GetInt32(6),
                                        Distance = reader.GetDecimal(7),
                                        Price = reader.GetDecimal(8)
                                    };
                                    trains.Add(train);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return BadRequest("An exception occurred: " + ex.Message);
            }
            return Ok(trains);
        }

        [HttpGet("{TrainId}")]
        public IActionResult GetTrains(int TrainId)
        {
            Trains trains = new Trains();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Trains WHERE TrainId=@TrainId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TrainId", TrainId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                trains.TrainId = reader.GetInt32(0);
                                trains.TrainName = reader.GetString(1);
                                trains.StartLocation = reader.GetString(2);
                                trains.EndLocation = reader.GetString(3);
                                trains.StartTime = reader.GetTimeSpan(4);
                                trains.EndTime = reader.GetTimeSpan(5);
                                trains.Capacity = reader.GetInt32(6);
                                trains.Distance = reader.GetDecimal(7);
                                trains.Price = reader.GetDecimal(8);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return BadRequest("An exception occurred: " + ex.Message);
            }
            return Ok(trains);
        }

        [HttpPut("{TrainId}")]
        public IActionResult UpdateTrains(int TrainId, TrainsDTO trainDTO)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE Trains SET TrainName=@TrainName, StartLocation=@StartLocation,
                            EndLocation=@EndLocation, StartTime=@StartTime, EndTime=@EndTime,
                            Capacity=@Capacity, Distance=@Distance, Price=@Price WHERE TrainId=@TrainId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TrainId", TrainId);
                        command.Parameters.AddWithValue("@TrainName", trainDTO.TrainName);
                        command.Parameters.AddWithValue("@StartLocation", trainDTO.StartLocation);
                        command.Parameters.AddWithValue("@EndLocation", trainDTO.EndLocation);
                        command.Parameters.AddWithValue("@StartTime", trainDTO.StartTime);
                        command.Parameters.AddWithValue("@EndTime", trainDTO.EndTime);
                        command.Parameters.AddWithValue("@Capacity", trainDTO.Capacity);
                        command.Parameters.AddWithValue("@Distance", trainDTO.Distance);
                        command.Parameters.AddWithValue("@Price", trainDTO.Price);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            return NotFound("No train record found with the specified TrainId.");
                        }
                    }
                }
                return Ok(trainDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return BadRequest("An exception occurred: " + ex.Message);
            }
        }
        [HttpDelete("{TrainId}")]
        public IActionResult DeleteTrains(int TrainId)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Trains WHERE TrainId=@TrainId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TrainId", TrainId);
                        
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return BadRequest("An exception occurred: " + ex.Message);
            }
            return Ok();
        }
    }
}