namespace Air_Database;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DBConnection
{
    private DBConnection()
    {
    }

    public string Server { get; set; }
    public string DatabaseName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public MySqlConnection Connection { get; set;}

    private static DBConnection _instance = null;
    public static DBConnection Instance()
    {
        if (_instance == null)
            _instance = new DBConnection();
        return _instance;
    }
    
    public bool IsConnect()
    {
        try
        {
            string connectionString = $"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password}";
            Connection = new MySqlConnection(connectionString);

            Connection.Open();

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                Console.WriteLine("We have a connection to MySQL database.");
                return true;
            }
            else
            {
                Console.WriteLine("Unfortunately, we are not connected to MySQL database");
                return false;
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("We're sorry, but an database error has occurred: " +ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("That did not end well, An internal error has occured:"+ ex.Message);
            return false;
        }
    }

    
    public void Close()
    {
        if (Connection != null)
        {
            Connection.Close();
            Console.WriteLine("Connection closed.");
        }
        
    }        
    
    
    }
