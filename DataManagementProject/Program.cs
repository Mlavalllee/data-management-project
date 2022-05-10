using System.Text.Json;

var options = new JsonSerializerOptions { IncludeFields = true };

List<UserData> AccountData = new List<UserData>();
//string jsonString = JsonSerializer.Serialize(AccountData, options);
//File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", jsonString);
string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
AccountData = JsonSerializer.Deserialize<List<UserData>>(jsonStringFromFile, options);

List<SongData> Songs = new List<SongData>();
Songs.Add(new SongData("pop", "TheWeekend", "Blinding Lights"));
string jsonString2 = JsonSerializer.Serialize(Songs, options);
File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt",jsonString2);
string jsonStringFromFile2 = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt");
List<SongData>? Songs2 = JsonSerializer.Deserialize<List<SongData>>(jsonStringFromFile2, options);

start();

void start()
{
    // read file in order to get a list of users and passwords
    string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
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

    int temp = UserData.CheckUser(Input);

    string jsonString = JsonSerializer.Serialize(AccountData, options);
    File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", jsonString);
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
        return $"{this.Genre} {this.Artist} {this.Song}";
    }
}

class UserData
{
    public string User;
    public string Password;
    private List<string> Usernames = new List<string>();
    public UserData(string User, string Password)
    {
        this.User = User;
        this.Password = Password;
    }

    public int CheckUser(string Input)
    {
        bool alreadyExists = false;
        foreach (var user in Usernames)
        {
            if (user == Input)
            {
                alreadyExists = true;
                break;
            }
        }
        if(alreadyExists == true)
        {
            return 1;
        } 
        else
        {
            return -1;
        }
    }
    public override string ToString()
    {
        return $"{this.User} {this.Password}";
    }
}