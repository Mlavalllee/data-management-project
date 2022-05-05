
string[] SignInData;
List<string> AccountData = new List<string>();

start();

void start()
{
    bool Quit = false;

    // User Sign in / regristration 
    Console.WriteLine();
    Console.WriteLine("Already have an account? please sign in");
    Console.WriteLine("1: Sign In");
    Console.WriteLine("2: Register Account");
    Console.WriteLine("3: Quit");
    Console.Write("input: ");
    string input = Console.ReadLine();
    int temp = int.Parse(input);
    Console.Clear();

    switch (temp)
    {
        case 1:
            signIn();
            break;
        case 2:
            register();
            break;
        case 3:
            Quit = true;
            break;
        default:
            Console.WriteLine("sign in type not chosen, please try again");
            break;
    }
    if (Quit != true)
    {
        start();
    }
}

void register()
{
    Console.WriteLine("please input Username");
    Console.Write("input: ");
    string Input = Console.ReadLine();

    Console.Clear();
    Console.WriteLine("please input Password");
    Console.Write("input: ");
    string InputPassword = Console.ReadLine();
    Console.Clear();

    if (Input.Trim() != null && InputPassword.Trim() != null)
    {
        AccountData.Add(new string(Input));
        AccountData.Add(new string(InputPassword));
        File.AppendAllLines(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", AccountData);
        AccountData.Remove(Input);
        AccountData.Remove(InputPassword);
    }
}

void signIn()
{
    // read file in order to get a list of users and passwords
    SignInData = File.ReadAllLines(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
    // variable 
    bool SignInSuccessful = false;

    Console.WriteLine("please input Username");
    Console.Write("input: ");
    string input = Console.ReadLine();

    Console.WriteLine("please input Password");
    Console.Write("input: ");
    string InputPassword = Console.ReadLine();

    // find username, then if username is found, compare passwords
    for (int i = 0; i < SignInData.Length; i += 2)
    {
        if(SignInData[i] == input)
        {
            if(SignInData[i + 1] == InputPassword)
            {
                SignInSuccessful = true;
            }
            break;
        }
    }

    Console.Clear();
    if (SignInSuccessful == true) 
    {
        Console.WriteLine("sign in sucessful");
        menuLoop();
    }
    else {
        Console.WriteLine("Username or Password incorrect, please try again");
    }
}

void menuLoop()
{
    Console.WriteLine("");
    Console.WriteLine("1: Display all songs");
    Console.WriteLine("2: search for song(s) using keywords");
    Console.WriteLine("3: add to favourite list");
    Console.WriteLine("4: Display favourite list");
    Console.WriteLine("5: Sign out");
    Console.Write("Input: ");
    string input = Console.ReadLine();
    int temp = int.Parse(input);
    Console.Clear();
    switch (temp)
    {
        case 1:
            DisplayAll();
            break;
        case 2:
            Search();
            break;
        case 3:
            AddFavourite();
            break;
        case 4:
            DisplayFavourite();
            break;
        case 5:
            start();
            break;
    }
}
 
void DisplayAll()
{

}

void Search()
{

}
void  AddFavourite()
{

}

void DisplayFavourite()
{

}