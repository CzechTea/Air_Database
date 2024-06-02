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

    
}
