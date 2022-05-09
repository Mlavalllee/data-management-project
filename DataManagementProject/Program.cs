using System.Text.Json;


List<UserData> AccountData = new List<UserData>();
AccountData.Add(new UserData("User1", "123"));
List<SongData> Songs = new List<SongData>();


var options = new JsonSerializerOptions { IncludeFields = true };
string jsonString = JsonSerializer.Serialize(AccountData, options);
File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", jsonString);
string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
List<UserData>? AccountData2 = JsonSerializer.Deserialize<List<UserData>>(jsonStringFromFile, options);
if (AccountData2 != null)
{
    foreach (var UserData in AccountData2)
    {
        Console.WriteLine(UserData);
    }
}

//start();

void start()
{


    // read file in order to get a list of users and passwords
    bool Quit = false;

    // User Sign in / regristration 
    Console.WriteLine();
    Console.WriteLine("Already have an account? please sign in");
    Console.WriteLine("1: Sign In");
    Console.WriteLine("2: Register Account");
    Console.WriteLine("3: Quit");
    Console.Write("input: ");
    string Input = Console.ReadLine();
    int temp = int.Parse(Input);
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

}

void signIn()
{
    // variable 
    bool SignInSuccessful = false;

    Console.WriteLine("please input Username");
    Console.Write("input: ");
    string Input = Console.ReadLine();

    Console.WriteLine("please input Password");
    Console.Write("input: ");
    string InputPassword = Console.ReadLine();
}

void menuLoop()
{
    bool Loop = true;
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
            Loop = false;
            break;
    }
    if(Loop == true)
    {
        menuLoop();
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

class SongData
{
    public string Genre;
    public string Artist;
    public string Song;
    public SongData(string Genre, string Artist, string Song) 
    {
        this.Genre = Genre;
        this.Artist = Artist;
        this.Song = Song;
    }

    public override string ToString()
    {
        return $"Grenre; {this.Genre} Song: {this.Artist} - {this.Song}";
    }
}

class UserData
{
    public string User;
    public string Password;
    public UserData(string User, string Password)
    {
        this.User = User;
        this.Password = Password;
    }

    public override string ToString()
    {
        return $"{this.User} {this.Password}";
    }
}