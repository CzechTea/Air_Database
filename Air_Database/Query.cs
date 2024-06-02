namespace Air_Database;

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Query
{
    private DBConnection dbConnection;

    public Query(DBConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public List<string[]> Show_Airlines()
    {
        List<string[]> airlines = new List<string[]>();

        try
        {
            string query = @"
                SELECT a.airline_id, a.name AS airline_name, a.country, ap.name AS primary_airport_name
                FROM Airlines a
                LEFT JOIN Airports ap ON a.primary_airport_id = ap.airport_id
            ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] airlineData = new string[4];
                        airlineData[0] = reader.GetInt32("airline_id").ToString();
                        airlineData[1] = reader.GetString("name");
                        airlineData[2] = reader.GetString("country");
                        airlineData[3] = reader.GetString("primary_airport_name");
                        airlines.Add(airlineData);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
        }

        return airlines;
    }

    public List<string[]> Show_Airplanes()
    {
        List<string[]> airplanes = new List<string[]>();

        try
        {
            string query = @"
            SELECT a.model, a.manufacturer, a.capacity, a.registration_number, al.name AS airline_name
            FROM Airplanes a
            INNER JOIN Airlines al ON a.airline_id = al.airline_id
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] airplaneData = new string[5];
                        airplaneData[0] = reader.GetString("model");
                        airplaneData[1] = reader.GetString("manufacturer");
                        airplaneData[2] = reader.GetInt32("capacity").ToString();
                        airplaneData[3] = reader.GetString("registration_number");
                        airplaneData[4] = reader.GetString("airline_name");
                        airplanes.Add(airplaneData);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
        }

        return airplanes;
    }

    public List<string[]> Show_Airports()
    {
        List<string[]> airports = new List<string[]>();

        try
        {
            string query = @"
            SELECT airport_id, name, city, country, IATA_code, ICAO_code
            FROM Airports
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] airportData = new string[6];
                        airportData[0] = reader.GetInt32("airport_id").ToString();
                        airportData[1] = reader.GetString("name");
                        airportData[2] = reader.GetString("city");
                        airportData[3] = reader.GetString("country");
                        airportData[4] = reader.GetString("IATA_code");
                        airportData[5] = reader.GetString("ICAO_code");
                        airports.Add(airportData);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
        }

        return airports;
    }

    public List<string[]> Show_Flights()
    {
        List<string[]> flights = new List<string[]>();

        try
        {
            string query = @"
            SELECT 
                f.flight_id,
                f.flight_number,
                al.name AS airline_name,
                ap.model AS airplane_model,
                dep.name AS departure_airport,
                arr.name AS arrival_airport,
                f.departure_time,
                f.arrival_time
            FROM Flights f
            INNER JOIN Airlines al ON f.airline_id = al.airline_id
            INNER JOIN Airplanes ap ON f.airplane_id = ap.airplane_id
            INNER JOIN Airports dep ON f.departure_airport_id = dep.airport_id
            INNER JOIN Airports arr ON f.arrival_airport_id = arr.airport_id
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] flightData = new string[8];
                        flightData[0] = reader.GetInt32("flight_id").ToString();
                        flightData[1] = reader.GetString("flight_number");
                        flightData[2] = reader.GetString("airline_name");
                        flightData[3] = reader.GetString("airplane_model");
                        flightData[4] = reader.GetString("departure_airport");
                        flightData[5] = reader.GetString("arrival_airport");
                        flightData[6] = reader.GetDateTime("departure_time").ToString();
                        flightData[7] = reader.GetDateTime("arrival_time").ToString();
                        flights.Add(flightData);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
        }

        return flights;
    }

    public bool Add_Airline(string name, string country, int primaryAirportId)
    {
        try
        {
            string query = @"
            INSERT INTO Airlines (name, country, primary_airport_id)
            VALUES (@name, @country, @AirportId)
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@country", country);
                command.Parameters.AddWithValue("@AirportId", primaryAirportId);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
            return false;
        }

    }

    public bool Add_Aircraft(string model, string manufacturer, int capacity, int airlineId, string registrationNumber)
    {
        try
        {
            string query = @"
            INSERT INTO Airplanes (model, manufacturer, capacity, airline_id, registration_number)
            VALUES (@model, @manufacturer, @capacity, @airlineId, @registrationNumber)
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@model", model);
                command.Parameters.AddWithValue("@manufacturer", manufacturer);
                command.Parameters.AddWithValue("@capacity", capacity);
                command.Parameters.AddWithValue("@airlineId", airlineId);
                command.Parameters.AddWithValue("@registrationNumber", registrationNumber);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
            return false;
        }
    }

    public bool Add_Airport(string name, string city, string country, string IATA, string ICAO)
    {
        try
        {
            string query = @"
            INSERT INTO Airports (name, city, country, IATA, ICAO)
            VALUES (@name, @city, @country, @IATA, @ICAO)
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@city", city);
                command.Parameters.AddWithValue("@country", country);
                command.Parameters.AddWithValue("@IATA", IATA);
                command.Parameters.AddWithValue("@ICAO", ICAO);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
            return false;
        }
    }




    public bool Add_Flight(string flightNumber, int airlineId, int airplaneId, int departureAirportId,
        int arrivalAirportId, DateTime departureTime, DateTime arrivalTime)
    {
        try
        {
            string query = @"
            INSERT INTO Flights (flight_number, airline_id, airplane_id, departure_airport_id, arrival_airport_id, departure_time, arrival_time)
            VALUES (@flightNumber, @airlineId, @airplaneId, @departureAirportId, @arrivalAirportId, @departureTime, @arrivalTime)
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@flightNumber", flightNumber);
                command.Parameters.AddWithValue("@airlineId", airlineId);
                command.Parameters.AddWithValue("@airplaneId", airplaneId);
                command.Parameters.AddWithValue("@departureAirportId", departureAirportId);
                command.Parameters.AddWithValue("@arrivalAirportId", arrivalAirportId);
                command.Parameters.AddWithValue("@departureTime", departureTime);
                command.Parameters.AddWithValue("@arrivalTime", arrivalTime);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sorry, an error has occurred: " + ex.Message);
            return false;
        }
    }
}




    

