
string[,] Songs =
{
  // genre || songs
    {"pop", "test"},
    {"rap", "test"},
    {"rock", "test"},
    {"eurobeat", "test"},
    {"futurefunk", "test"},
    {"jazz", "test"},
    {"country", "test"},
    {"jpop", "test"},
    {"trap", "test"},
    {"edm", "test"},
    {"reggae", "test"},
    {"lofi", "test"},
    {"r&b", "test"},
    {"classic", "test"}
};

SignIn();

void SignIn()
{
    // User Sign in / regristration 
    Console.WriteLine();
    Console.WriteLine("Already have an account? please sign in");
    Console.WriteLine("1: Sign In");
    Console.WriteLine("2: Register Account");
    Console.Write("input: ");
    string input = Console.ReadLine();
    int num = int.Parse(input);
    Console.Clear();
    switch (num)
    {
        case 1:
            Console.WriteLine("");
            break;
        case 2:
            Console.WriteLine("");
            break;
        default:
            Console.WriteLine("sign in type not chosen, please try again");
            break;
    }
    SignIn();
}



void MenuLoop()
{
    MenuLoop();
}
