using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using ConsoleTables;
using System.Net.Http;


namespace OneListClient
{
    class Program
    {
        static async Task AddOneItem(string token, Item newItem) { }

        static async Task GetOneItem(string token, double id)
        {
            try
            {
                var client = new HttpClient();

                var responseAsStream = await client.GetStreamAsync($"https://one-list-api.herokuapp.com/items/{id}?access_token={token}");

                var item = await JsonSerializer.DeserializeAsync<Item>(responseAsStream);

                var table = new ConsoleTable("Description", "Created At", "Completed");

                table.AddRow(item.Text, item.CreatedAt, item.CompletedStatus);
                table.Write();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("I could not find that item");
            }
        }
        static async Task ShowAllItems(string token)
        {


            // Creating a variable for the client and add using system.Net.http
            var client = new HttpClient();

            // creating variable to GET a request to server and returns a string... 
            //also, we added an AWAIT keyword in front of Client. what AWAIT does is is make the code wait until the rest of out code runs to return us a string from the HTTP
            //We also, added the variable token made above to give us more power in our code
            var responseAsStream = await client.GetStreamAsync($"https://one-list-api.herokuapp.com/items?access_token={token}");

            //Creating a "deserializer" which mean that it converts our response from HTTP into a LIST if our class Item. 
            //SO we first get the return as a stream. A stream is a variable that knows how to read data from HTTP
            var items = await JsonSerializer.DeserializeAsync<List<Item>>(responseAsStream);

            //Creating a table so we can see it in terminal. Had to download a package for it
            var table = new ConsoleTable("Description", "Created At", "Completed At");

            //Back Into Csharp once we got a return of our HTTP into List and out class (Item)
            foreach (var item in items)
            {
                //The code below used to be the code before until we downloaded a package in our terminal to make it look better. var table = new ConsoleTable("Description", "Created At", "Completed At"); is what made the below code obsolete
                // Console.WriteLine($"The Task {item.Text} was created on {item.CreatedAt} and has a completion of {item.CompletedStatus}");

                //New Code compare to the older one above. Lets us use CREATE a table in  our terminal.
                // In the code below our item.Text, item.CreatedAt, item.CompletedStatus should be in the same place as Description, Created At, Completed At in our Var Table.
                table.AddRow(item.Text, item.CreatedAt, item.CompletedStatus);

            }

            //The code below allows us to print the code in our terminal
            table.Write();



        }



        static async Task Main(string[] args)
        {

            //Adding args from our MAIN Code ^^^ Allowing more control when it appears in our terminal by letting us choose different versions of that Http
            var token = "";

            //If statement is saying if args length is 0 print What List of Cohort would you like to check out?
            // 
            if (args.Length == 0)
            {

                Console.WriteLine("What List of Cohort would you like to check out? ");
                token = Console.ReadLine();

            }
            else
            {
                token = args[0];
            }


            var keepGoing = true;

            while (keepGoing)
            {
                Console.Clear();
                Console.Write("Get (A)ll todo, (C)reate, (O)ne Todo or (Q)uit: ");
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;
                    case "C":
                        Console.WriteLine("Enter the description of your new todo?: ");
                        var text = Console.ReadLine();

                        var newItem = new Item
                        {
                            Text = text
                        };

                        await AddOneItem(token, newItem);
                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();
                        break;

                    case "O":
                        Console.Write($"Enter the Id: ");
                        var id = Convert.ToDouble(Console.ReadLine());

                        await GetOneItem(token, id);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;
                    case "A":
                        await ShowAllItems(token);

                        Console.WriteLine($"Press ENTER to Continue");
                        Console.ReadLine();
                        break;

                    default:
                        break;
                }




            }



        }
    }
}
