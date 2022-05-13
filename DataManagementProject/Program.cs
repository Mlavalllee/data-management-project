using System.Text.Json;

var options = new JsonSerializerOptions { IncludeFields = true };

List<UserData> AccountData = new List<UserData>();

List<SongData> Songs = new List<SongData>();
Songs.Add(new SongData("Pop.","Future, Ft(Drake & Tems).", "Wait For U."));
Songs.Add(new SongData("Pop.", "Harry Styles.", "As It Was."));

string jsonString2 = JsonSerializer.Serialize(Songs, options);
File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt",jsonString2);

start();

void start()
{
    // read file in order to get a list of users and passwords
    string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
    AccountData = JsonSerializer.Deserialize<List<UserData>>(jsonStringFromFile, options);
    bool Quit = false;

    // User Sign in / regristration 
    Console.WriteLine();
    Console.WriteLine("Already have an account? please sign in");
    Console.WriteLine("1: Sign In");
    Console.WriteLine("2: Register Account");
    Console.WriteLine("3: Quit");
    Console.Write("input: ");
    string Input = Console.ReadLine();
    int num = int.Parse(Input);
    Console.Clear();

    if (num == 1 || num == 2)
    {
        Console.WriteLine("please input Username");
        Console.Write("input: ");
        string UsernameInput = Console.ReadLine().Trim();

        Console.Clear();
        Console.WriteLine("please input Password");
        Console.Write("input: ");
        string InputPassword = Console.ReadLine().Trim();
        Console.Clear();

        if(num == 1)
        {
            signIn(UsernameInput, InputPassword);
        }
        else
        {
            register(UsernameInput + ",", InputPassword + ",");
        }
    }
    else if (num == 3)
    {
        Quit = true;
    }
    else 
    {
        Console.WriteLine("sign in type not chosen, please try again");
    }

    if (Quit != true)
    {
        start();
    }
}

void register(string Username, string Password)
{
    // check is username is in use
    bool InUse = UserSearch(Username, Password, true);

    // if username is not in use create account
    if (InUse != true)
    {
        AccountData.Add(new UserData(Username, Password));
        string jsonString = JsonSerializer.Serialize(AccountData, options);
        File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", jsonString);
    }
    else
    {
        Console.WriteLine("Username already in use, choose another one");
    }
}

void signIn(string Username, string Password)
{
    // check Sign in info, if correct sign in, else return back to start menu
    bool SignInSuccessful = UserSearch(Username, Password, false);
    if(SignInSuccessful == true)
    {
        Console.WriteLine("Sign in Successful");
        menuLoop(Username);
    }
    else
    {
        Console.WriteLine("Username or password incorrect, try again");
    }
}

bool UserSearch(string Input, string InputPassword, bool Registering)
{
    for (int i = 0; i < AccountData.Count; i++)
    {
        string Account = AccountData[i].ToString();
        string[] AccountLogin = Account.Split(",");

        switch(Registering)
        {
            case true:
                if (AccountLogin[0] == Input)
                {
                    return true;
                }
                break;
            case false:
                if (AccountLogin[0] == Input && AccountLogin[1] == InputPassword)
                {
                    return true;
                }
                break;
        }
    }
    return false;
}

void menuLoop(string User)
{
    string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt");
    Songs = JsonSerializer.Deserialize<List<SongData>>(jsonStringFromFile, options);

    bool Loop = true;
    Console.WriteLine("");
    Console.WriteLine("1: Display all songs");
    Console.WriteLine("2: search for song(s) using keywords");
    Console.WriteLine("3: add to favourite list");
    Console.WriteLine("4: Remove from favourite list");
    Console.WriteLine("4: Display favourite list");
    Console.WriteLine("5: Sign out");
    Console.Write("Input: ");
    string input = Console.ReadLine();
    int num = int.Parse(input);
    Console.Clear();
    switch (num)
    {
        case 1:
            DisplayAll();
            for(int i = 0; i < Songs.Count; i++)
            {
                string SongsToString = Songs[i].ToString();
                string[] songdata = SongsToString.Split(".");
                Console.WriteLine($"Genre: {songdata[0]} Artist:{songdata[1]} Song:{songdata[2]}");
            }
            break;
        case 2:
            Search();
            break;
        case 3:
            AddFavourite();
            break;
        case 4:
            RemoveFavourite();
            break;
        case 5:
            DisplayFavourite();
            break;
        case 6:
            Loop = false;
            break;
    }
    if(Loop == true)
    {
        menuLoop(User);
    }
    // temp
    if(User == "")
    {

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

void RemoveFavourite()
{

}

void DisplayFavourite()
{

}
void SongSearch(string input) {

}

class SongData
{
    public string Genre;
    public string Artist;
    public string Title;
    public SongData(string Genre, string Artist, string Title) 
    {
        this.Genre = Genre;
        this.Artist = Artist;
        this.Title = Title;
    }

    public override string ToString()
    {
        return $"{this.Genre} {this.Artist} {this.Title}";
    }
}

class UserData
{
    public string User;
    public string Password;
    public string Favourites;
    public UserData(string User, string Password)
    {
        this.User = User;
        this.Password = Password;
        this.Favourites = Favourites;
    }

    public override string ToString()
    {
        return $"{this.User} {this.Password} {this.Favourites}";
    }
}