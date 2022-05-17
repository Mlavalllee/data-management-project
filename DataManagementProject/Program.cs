using System.Text.Json;

var options = new JsonSerializerOptions { IncludeFields = true };

List<UserData> AccountData = new List<UserData>();

List<SongData> Songs = new List<SongData>();
Songs.Add(new SongData("pop.","future.", "wait for u."));
Songs.Add(new SongData("pop.", "harry styles.", "as it was."));

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

        if(UsernameInput != "" && InputPassword != "")
        {
            if (num == 1)
            {
                signIn(UsernameInput, InputPassword);
            }
            else
            {
                register(UsernameInput + ",", InputPassword + ",");
            }
        }
        else
        {
            Console.WriteLine("one or more of the fields are empty");
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
        AccountData.Add(new UserData(Username, Password, null + ","));
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
        switch (Registering)
        {
            case true:
                if (AccountLogin[0].Trim() == Input)
                {
                    return true;
                    Console.WriteLine("Test");
                }
                break;
            case false:
                if (AccountLogin[0].Trim() == Input && AccountLogin[1].Trim() == InputPassword)
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
    //load song data
    string jsonStringFromFile = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\songData.txt");
    Songs = JsonSerializer.Deserialize<List<SongData>>(jsonStringFromFile, options);

    //load User Data
    string jsonStringFromFile2 = File.ReadAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt");
    AccountData = JsonSerializer.Deserialize<List<UserData>>(jsonStringFromFile2, options);

    bool Loop = true;
    // Meny Loop
    Console.WriteLine();
    Console.WriteLine("1: Display all songs");
    Console.WriteLine("2: search for song(s) using keywords");
    Console.WriteLine("3: add to favourite list");
    Console.WriteLine("4: Remove from favourite list");
    Console.WriteLine("5: Display favourite list");
    Console.WriteLine("6: Sign out");
    Console.Write("Input: ");
    string Input = Console.ReadLine();
    int num = int.Parse(Input);
    Console.Clear();
    switch (num)
    {
        case 1:
            DisplayAll();
            break;
        case 2:
            Console.WriteLine("Search for song");
            Console.Write("Input: ");
            Input = Console.ReadLine().Trim().ToLower();
            if(Input != "")
            {
                Console.WriteLine("Search Started");
                Search(Input);
            }
            else
            {
                Console.WriteLine("field empty, Search not started");
            }
            break;
        case 3:

            Console.Write("Input:");
            Input = Console.ReadLine(); 
            AddFavourite(User, Input);
            break;
        case 4:
            RemoveFavourite(User);
            break;
        case 5:
            DisplayFavourite(User);
            break;
        case 6:
            Loop = false;
            break;
    }

    // save all userdata back to file
    string jsonString = JsonSerializer.Serialize(AccountData, options);
    File.WriteAllText(@"C:\Users\m.lavallee3\source\repos\DataManagementProject\users.txt", jsonString);

    if (Loop == true)
    {
        menuLoop(User);
    }
}
 
void DisplayAll()
{
    // loop and display all songs
    for (int i = 0; i < Songs.Count; i++)
    {
        string SongsToString = Songs[i].ToString();
        string[] songdata = SongsToString.Split(".");
        Console.WriteLine($"Genre: {songdata[0]} Artist:{songdata[1]} Song:{songdata[2]}");
    }
}

void Search(string Input)
{
    // variable
    bool found = false;
    // loop through all songs
    for (int i = 0; i < Songs.Count; i++)
    {
        string SongsToString = Songs[i].ToString();
        string[] songdata = SongsToString.Split(".");
        // check if Search term matches anything (I.E Genre, Artist, or Song name)
        if( Input == songdata[0].Trim() || Input == songdata[1].Trim() || Input == songdata[3].Trim())
        {
            Console.WriteLine($"Genre: {songdata[0]} Artist:{songdata[1]} Song:{songdata[2]}");
            found = true;
        }
    }
    if (found != true)
    {
        Console.WriteLine("Song(s) not found");
    }
}

void  AddFavourite(string User, string Input)
{
    string output = ", ";
    string song ="";
    // find song first, then add to account's list
    for (int index = 0; index < Songs.Count; index++)
    {
        string SongsToString = Songs[index].ToString();
        string[] songdata = SongsToString.Split(".");
        int SongPos = -1;
        if (Input == songdata[0].Trim() || Input == songdata[1].Trim() || Input == songdata[3].Trim())
        {
            SongPos = index;
        }
        if(SongPos != -1)
        {
            song = Songs[SongPos].ToString();
        }
        for (int i = 0; i < AccountData.Count ; i++)
        {
            string account = AccountData[i].ToString();
            string[] accountdata = account.Split(",");
            if (accountdata[0] == User && song != "")
            {
                output += song + ",";
                AccountData.RemoveAt(i);
                AccountData.Add(new UserData(User + ",", accountdata[1].Trim() + ",", output));
            }
        }
    }

}

void RemoveFavourite(string User)
{

}

void DisplayFavourite(string User)
{

}

// maybe
int SongSearch(string Input) 
{
    bool found = false;
    for (int i = 0; i < Songs.Count; i++)
    {
        string SongsToString = Songs[i].ToString();
        string[] songdata = SongsToString.Split(".");
        if (Input == songdata[0].Trim() || Input == songdata[1].Trim() || Input == songdata[3].Trim())
        {
            return i; 
        }
    }
    return -1;
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
    public UserData(string User, string Password, string Favourites)
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