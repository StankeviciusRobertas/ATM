namespace ATM
{
    internal class Program
    {
        private static List<Users> users = new List<Users>();
        private static Users currentUser;
        static void Main(string[] args)
        {
            LoadUsersFromFile();

            Console.WriteLine("Įveskite kortelės numerį:");
            string cardId = Console.ReadLine();

            Console.WriteLine("Įveskite slaptažodį:");
            string password = Console.ReadLine();

            if (Login(cardId, password))
            {
                Console.WriteLine("Prisijungta sėkmingai!");

                while (true)
                {
                    DisplayOptions();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            ShowBalance();
                            break;
                        case "2":
                            ShowLastTransactions();
                            break;
                        case "3":
                            WithdrawMoney();
                            break;
                        case "4":
                            ExitProgram();
                            break;
                        default:
                            Console.WriteLine("Neteisingas pasirinkimas. Bandykite dar kartą.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Prisijungimas nepavyko. Programa išsijungs.");
            }
        }

        private static void LoadUsersFromFile()
        {
            string filePath = "C:\\Users\\rstak\\source\\Savarnkiskas darbas\\ATM\\ATM\\BankCard.txt"; // Provide the actual path to your user file

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    // Split the line using the pipe character as a delimiter
                    string[] values = line.Split('|');

                    // Ensure that the line has the expected number of elements
                    if (values.Length == 3)
                    {
                        // Create a new User object
                        Users user = new Users
                        {
                            CardId = values[0],
                            Pin = values[1],
                            Balance = decimal.Parse(values[2])
                        };

                        // Add the user to the list
                        users.Add(user);
                    }
                    else
                    {
                        Console.WriteLine($"Skipping invalid line: {line}");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the user file: {e.Message}");
            }
        }

        private static bool Login(string cardId, string password)
        {
            // Patikriname, ar vartotojas su tokiais duomenimis egzistuoja
            Users user = users.FirstOrDefault(u => u.CardId == cardId && u.Pin == password);

            if (user != null)
            {
                currentUser = user;
                return true;
            }

            return false;
        }

        private static void DisplayOptions()
        {
            Console.WriteLine("Pasirinkite veiksmą:");
            Console.WriteLine("1. Matyti turimus pinigus");
            Console.WriteLine("2. Peržiūrėti paskutines transakcijas");
            Console.WriteLine("3. Pinigų išsiėmimas");
            Console.WriteLine("4. Išeiti");
        }

        private static void ShowBalance()
        {
            Console.WriteLine($"Turimi pinigai: {currentUser.Balance} EUR");
        }

        private static void ShowLastTransactions()
        {
            // Čia galite įgyvendinti logiką, kuri rodo vartotojui jo paskutines transakcijas.
        }

        private static void WithdrawMoney()
        {
            Console.WriteLine("Įveskite išsiimamą sumą:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (amount > 0 && amount <= 1000 && currentUser.WithdrawMoney(amount))
            {
                Console.WriteLine($"Išsiimta sėkmingai. Likutis: {currentUser.Balance} EUR");
                // Čia galite įgyvendinti logiką, kuri užfiksuoja transakciją.
            }
            else
            {
                Console.WriteLine("Išsiėmimas nepavyko. Patikrinkite sumą ir savo likutį.");
            }
        }

        private static void ExitProgram()
        {
            // Čia galite įgyvendinti logiką, kuri išsaugo vartotojus į failą prieš baigiant programą.
            Environment.Exit(0);
        }
    }
}
    