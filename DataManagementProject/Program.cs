using System.Text.Json;

var options = new JsonSerializerOptions { IncludeFields = true };

List<UserData> AccountData = new List<UserData>();

List<SongData> Songs = new List<SongData>();

//Songs.Add(new SongData("pop", "TheWeekend", "Blinding Lights"));
//string jsonString2 = JsonSerializer.Serialize(Songs, options);
//File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt",jsonString2);

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
            register(UsernameInput, InputPassword);
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
        string Username = Account.Split(" ")[0];
        string Password = Account.Split(" ")[1];

        switch(Registering)
        {
            case true:
                if (Username == Input)
                {
                    return true;
                }
                break;
            case false:
                if (Username == Input && Password == InputPassword)
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
                string[] songdata = SongsToString.Split(" ");
                string song = " ";
                for(int index = 2; index < songdata.Length; index++)
                {
                    song += $" {songdata[index]}";
                }
                Console.WriteLine($"Genre: {songdata[0]} Artist: {songdata[1]} Song:{song}");
            }
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

void DisplayFavourite()
{

}
void SongSearch(string input) {

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