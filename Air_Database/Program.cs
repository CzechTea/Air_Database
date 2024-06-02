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
    while (ans != "4")
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
                     result = query.Show_Airports();
                     if (result.Count > 0)
                     {
                         Console.WriteLine("Airports:");
                         foreach (string[] airport in result)
                         {
                             Console.WriteLine($"ID: {airport[0]}, Name: {airport[1]}, City: {airport[2]}, Country: {airport[3]}, IATA: {airport[4]}, ICAO: {airport[5]}");
                         }
                     }
                     else
                     {
                         Console.WriteLine("No airports have been found. Try adding one!");
                     }
                     break;
                 case "4":
                     result.Clear();
                     result = query.Show_Flights();
                     if (result.Count > 0)
                     {
                         Console.WriteLine("Flights:");
                         foreach (string[] flight in result)
                         {
                             Console.WriteLine($"ID: {flight[0]}, Flight Number: {flight[1]}, Airline: {flight[2]}, Airplane Model: {flight[3]}, Departure Airport: {flight[4]}, Arrival Airport: {flight[5]}, Departure Time: {flight[6]}, Arrival Time: {flight[7]}");
                         }
                     }
                     else
                     {
                         Console.WriteLine("No flights have been found. Try adding one!");
                     }
                     break;
                }
                break;
            case "2":
                ans = "";
                Console.WriteLine("What would you like to add?\n" +
                                  "1. Airline\n" +
                                  "2. Airplane\n" +
                                  "3. Airport\n" +
                                  "4. Flight");
                ans = Console.ReadLine();
                switch (ans)
                {
                    case "1":
                        ans = "";
                        Console.WriteLine("Enter name:");
                        string aName = Console.ReadLine();
                        Console.WriteLine("Enter country:");
                        string aCountry = Console.ReadLine();
                        Console.WriteLine("Enter primary airport ID:");
                        int primaryAId = int.Parse(Console.ReadLine());
                        bool success = query.Add_Airline(aName, aCountry, primaryAId);
                        if (success)
                        {
                            Console.WriteLine("Everything went well!");
                        }
                        else
                        {
                            Console.WriteLine("Airline could not be added because of an error.");
                        }
                        break;
                    case "2":
                        ans = "";
                        Console.WriteLine("Enter model of the (ATR-72):");
                        string aircraftModel = Console.ReadLine();
                        Console.WriteLine("Enter manufacturer (ATR):");
                        string aircraftManufacturer = Console.ReadLine();
                        Console.WriteLine("Enter seat capacity(72):");
                        int aircraftCapacity = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter airline ID (Who owns it):");
                        int aircraftAirlineId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter registration number (XX-000):");
                        string aircraftRegistrationNumber = Console.ReadLine();
                        success = query.Add_Aircraft(aircraftModel, aircraftManufacturer, aircraftCapacity, aircraftAirlineId, aircraftRegistrationNumber);
                        if (success)
                        {
                            Console.WriteLine("Aircraft added successfully :).");
                        }
                        else
                        {
                            Console.WriteLine("An error has stopped us from inserting.");
                        }
                        break;
                    case "3":
                        ans = "";
                        Console.WriteLine("Add Airport:");
                        Console.WriteLine("Enter name of the airport (In English):");
                        string airportName = Console.ReadLine();
                        Console.WriteLine("Enter city:");
                        string airportCity = Console.ReadLine();
                        Console.WriteLine("Enter country:");
                        string airportCountry = Console.ReadLine();
                        Console.WriteLine("Enter IATA code (3 letters):");
                        string airportIATA = Console.ReadLine();
                        Console.WriteLine("Enter ICAO code (4 letters):");
                        string airportICAO = Console.ReadLine();
                        success = query.Add_Airport(airportName, airportCity, airportCountry, airportIATA, airportICAO);
                        if (success)
                        {
                            Console.WriteLine("Airport added successfully :D.");
                        }
                        else
                        {
                            Console.WriteLine("The airport could not be added.");
                        }
                        break;
                    case "4":
                        ans = "";
                        Console.WriteLine("Enter flight number (2 letters and 3 numbers:");
                        string flightNumber = Console.ReadLine();
                        Console.WriteLine("Enter airline ID:");
                        int flightAirlineId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter airplane ID:");
                        int flightAirplaneId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter departure airport ID:");
                        int departureAirportId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter arrival airport ID:");
                        int arrivalAirportId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter departure time (yyyy-mm-dd hh:mm):");
                        DateTime departureTime = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter arrival time (yyyy-mm-dd hh:mm):");
                        DateTime arrivalTime = DateTime.Parse(Console.ReadLine());
                        success = query.Add_Flight(flightNumber, flightAirlineId, flightAirplaneId, departureAirportId, arrivalAirportId, departureTime, arrivalTime);
                        if (success)
                        {
                            Console.WriteLine("Flight added successfully >:3");
                        }
                        else
                        {
                            Console.WriteLine("We could not add flight...");
                        }
                
                        break;
                        
                }

                break;
                case "3":
                    
                break;
                case "4":
                    Console.WriteLine("Closing connection.\nGoodbye, thank you for your cooperation.");
                    dbCon.Close();
                    
                    break;
                
        }
    }
}
