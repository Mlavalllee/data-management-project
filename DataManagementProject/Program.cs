

string[] line = File.ReadAllLines(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");


start();

void start()
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
            signIn();
            break;
        case 2:
            register();
            break;
        default:
            Console.WriteLine("sign in type not chosen, please try again");
            start();
            break;
    }
}

void register()
{
    Console.WriteLine("please input Username");
    Console.Write("input: ");
    string Input = Console.ReadLine().Trim();

    Console.Clear();
    Console.WriteLine("please input Password");
    Console.Write("input: ");
    string InputPassword = Console.ReadLine().Trim();

    if (Input != null && InputPassword != null)
    {

    }
    start();
}

void signIn()
{
    bool userFound = false;
    bool correctPassword = false;
    Console.WriteLine("please input Username");
    Console.Write("input: ");
    string input = Console.ReadLine();

    Console.WriteLine("please input Password");
    Console.Write("input: ");
    string InputPassword = Console.ReadLine();
    for (int i = 0; i < line.Length; i += 2)
    {
        if(line[i] == input)
        {
            userFound = true;
            if(line[i + 1] == InputPassword)
            {
                correctPassword = true;
            }
            break;
        }
    }

    Console.Clear();
    if (userFound == true && correctPassword == true) 
    {
        Console.WriteLine("sign in sucessful");
        menuLoop();
    }
    else {
        Console.WriteLine("username or password incorrect, please try again");
        signIn();
    }
}

void menuLoop()
{
    //menuLoop();
}
 