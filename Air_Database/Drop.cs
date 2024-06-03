using MySql.Data.MySqlClient;

namespace Air_Database;

public class Drop
{
    private DBConnection dbConnection;

    public Drop(DBConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }
    
    public bool Delete_Airline(int id)
    {
        try
        {
            string query = @"
            DELETE FROM Airlines
            WHERE airline_id = @airlineId
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@airlineId", id);
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
    public bool Delete_Airplane(int id)
    {
        try
        {
            string query = @"
            DELETE FROM Airlines
            WHERE airplane_id = @airplaneId
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@airplaneId", id);
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
    public bool Delete_Airport(int id)
    {
        try
        {
            string query = @"
            DELETE FROM Airport
            WHERE airport_id = @airportId
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@airportId", id);
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
    public bool Delete_Flight(int id)
    {
        try
        {
            string query = @"
            DELETE FROM Flight
            WHERE flight_id = @flightId
        ";

            using (MySqlCommand command = new MySqlCommand(query, dbConnection.Connection))
            {
                command.Parameters.AddWithValue("@flightId", id);
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