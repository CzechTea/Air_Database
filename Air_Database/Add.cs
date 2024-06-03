namespace Air_Database;

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class Add
{
    private DBConnection dbConnection;

    public Add(DBConnection dbConnection)
    {
        this.dbConnection = dbConnection;
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




    

