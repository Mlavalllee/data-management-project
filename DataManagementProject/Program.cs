using System.Text.Json;

var options = new JsonSerializerOptions { IncludeFields = true };

List<UserData> AccountData = new List<UserData>();

List<SongData> Songs = new List<SongData>();

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
                register(UsernameInput, InputPassword );
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
        switch (Registering)
        {
            // registering account, check if username is already in use, return true is username is in use
            case true:
                if (AccountData[i].User == Input)
                {
                    return true;
                }
                break;
            // signing in, check if username and password match, return true if they do
            case false:
                if (AccountData[i].User == Input && AccountData[i].Password == InputPassword)
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
    // Menu Loop
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
            FindUser(User, Input, "add");
            break;
        case 4:
            Console.Write("Input:");
            Input = Console.ReadLine();
            FindUser(User, Input, "remove");
            break;
        case 5:
            DisplayFavourite(User);
            break;
        case 6:
            Loop = false;
            break;
    }

    // save all user data back to file
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
        Console.WriteLine($"Genre: {Songs[i].Genre} Artist: {Songs[i].Artist} Song: {Songs[i].Title}");
    }
}

void Search(string Input)
{
    bool found = false;
    // loop through all songs
    for (int i = 0; i < Songs.Count; i++)
    {
        // check if Search term matches anything (I.E Genre, Artist, or Song name)
        if(Songs[i].Genre == Input || Songs[i].Artist == Input || Songs[i].Title == Input)
        {
            Console.WriteLine($"Genre: {Songs[i].Genre} Artist: {Songs[i].Artist} Song: {Songs[i].Title}");
            found = true;
        }
    }
    if (found != true)
    {
        Console.WriteLine("Song(s) not found");
    }
}


void FindUser(string User, string Input, string type)
{
    // find user's account
    for (int user = 0; user < AccountData.Count; user++)
    {
        // when account is found go to add or remove song
        if (AccountData[user].User == User)
        {
            if(type == "add")
            {
                AddFavourite(user, Input);
            }
            else
            {
                RemoveFavourite(user, Input);
            }
        }
    }
}

void  AddFavourite(int user, string Input)
{
    for (int song = 0; song < Songs.Count; song++)
    {
        // when song(s) is found add to favourites list
        if (Songs[song].Genre == Input || Songs[song].Artist == Input || Songs[song].Title == Input)
        {
            if (Songs[song].Genre == Input || Songs[song].Artist == Input || Songs[song].Title == Input)
            {
                AccountData[user].Favourites.Add(new SongData(
                Songs[song].Genre,
                Songs[song].Artist,
                Songs[song].Title
                ));
            }
        }
    }
}

void RemoveFavourite(int user, string Input)
{
    bool found = false;
    for (int song = 0; song < AccountData[user].Favourites.Count; song++)
    {
        // when song(s) is found remove from favourites list
        if (AccountData[user].Favourites[song].Genre == Input || AccountData[user].Favourites[song].Artist == Input || AccountData[user].Favourites[song].Title == Input)
        {
            AccountData[user].Favourites.RemoveAt(song);
            song--;
            found = true;
        }
    }
    if (found == false)
    {
        Console.WriteLine("no songs in favourites list");
    } 
    else
    {
        Console.WriteLine("song(s) removed");
    }
}

void DisplayFavourite(string User)
{
    for (int user = 0; user < AccountData.Count; user++)
    {
        if(AccountData[user].User == User)
        {
            for(int song = 0; song < AccountData[user].Favourites.Count; song++)
            Console.WriteLine($" Genre: {AccountData[user].Favourites[song].Genre} Artist: {AccountData[user].Favourites[song].Artist} Title: {AccountData[user].Favourites[song].Title}");
        }
    }
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
    public List<SongData> Favourites;
    public UserData(string User, string Password)
    {
        this.User = User;
        this.Password = Password;
        this.Favourites = new List<SongData>();
    }

    public override string ToString()
    {
        return $"{this.User} {this.Password} {this.Favourites}";
    }
}