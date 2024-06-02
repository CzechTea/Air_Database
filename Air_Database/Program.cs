using Air_Database;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;


bool ready = false;
string ans = null;
var dbCon = DBConnection.Instance();
Query query = new Query(dbCon);
Console.WriteLine("Hello, before we begin, we need to know informations about the database.");

do
{
    Console.WriteLine("What is the name of the database server?(If it's empty, it will be assumed as localhost)");
    dbCon.Server = Console.ReadLine();
    if (dbCon.Server == "")
    {
        dbCon.Server = "localhost";
    }


    Console.WriteLine("Got it.. What is the name of the database?");
    dbCon.DatabaseName = Console.ReadLine();
    while (dbCon.DatabaseName == "")
    {
        Console.WriteLine("If we want to work with the database, we should know the its name.");
        dbCon.Server = Console.ReadLine();
    }

    Console.WriteLine("What is your username of the database? (If empty, then it will be assumed as root)");

    dbCon.UserName = Console.ReadLine();
    if (dbCon.UserName == "")
    {
        dbCon.UserName = "root";

    }

    if (dbCon.UserName == "root")
    {
        Console.WriteLine("The username is root, therefore, the password is not needed.");
        dbCon.Password = "";
    }
    else
    {
        Console.WriteLine("Please enter your password");
        dbCon.Password = Console.ReadLine();
    }




    Console.WriteLine("Server name: " + dbCon.Server);
    Console.WriteLine("Database name: " + dbCon.DatabaseName);
    Console.WriteLine("User name: " + dbCon.UserName);
    Console.WriteLine("Password: " + dbCon.Password);
    Console.WriteLine("Is this correct? (If yes, type Y, if not, type N)");
    ans = Console.ReadLine();
    
        switch (ans.ToLower())
        {
            case "y":
                ready = true;
                break;
            case "n":
                Console.WriteLine("Ok, let's try it again.");
                break;
        }




    } while (!ready);


if (dbCon.IsConnect())
{
    Console.WriteLine("Now that we are connected, what would you like to do?");
    List<string[]> result = new List<string[]>();
    ans = "";
    while (ans != "exit")
    {
        Console.WriteLine("1. Show\n" +
                          "2. Insert\n" +
                          "3. Drop\n" +
                          "4. Exit");
        ans = Console.ReadLine();
        switch (ans)
        {
            case "1":
                ans = "";
                Console.WriteLine("What would you like to see?\n" +
                                  "1. Airlines\n" +
                                  "2. Airplanes\n" +
                                  "3. Airports\n" +
                                  "4. Flights");
                ans = Console.ReadLine();
                switch (ans)
                {
                 case "1":
                     result.Clear();
                     result = query.Show_Airlines();
                    if (result.Count > 0)
                    {
                        Console.WriteLine("Airlines:");
                        foreach (string[] airline in result)
                        {
                            Console.WriteLine($"ID: {airline[0]}, Name: {airline[1]}, Country: {airline[2]}, Hub Airport: {airline[3]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No airlines have been found. Try adding one!");
                    }
                    break;
                 
                 case "2":
                     result.Clear();
                 
                     result = query.Show_Airplanes();

                     if (result.Count > 0)
                     {
                         Console.WriteLine("Airplanes:");
                         foreach (string[] airplane in result)
                         {
                             Console.WriteLine($"Model: {airplane[0]}, Manufacturer: {airplane[1]}, Seat capacity: {airplane[2]}, Registration: {airplane[3]}, Airline: {airplane[4]}");                         }
                     }
                     else
                     {
                         Console.WriteLine("No airplanes have been found. Try adding one!");
                     }
                     break;
                 case "3":
                     result.Clear();


                     break;
                }
                
                    
                break;
        }
    }
}
