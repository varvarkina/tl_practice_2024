using System.Text.RegularExpressions;

const int tryLimit = 5;
Dictionary<string, string> dictionary = BuildDictionary();

PrintWelcomeMessage();

while ( true )
{
    PrintMenu();
    string userCommand = Console.ReadLine();
    switch ( userCommand )
    {
        case "1":
            RusToEng();
            break;
        case "2":
            EngToRus();
            break;
        case "3":
            AddWords();
            break;
        default:
            Console.WriteLine( @"Неизвестная команда. Введите цифры ""1"", ""2"" или ""3""" );
            break;
    }
}

static void PrintWelcomeMessage()
{
    Console.Write( @"Чтобы воспользоваться словарем, нажмите ""Enter""" );
    while ( !string.IsNullOrWhiteSpace( Console.ReadLine() ) )
    {
        Console.Write( @"Чтобы воспользоваться словарем, нажмите ""Enter""" );
    }
}

void PrintMenu()
{
    Console.WriteLine( "Меню:" );
    Console.WriteLine( "[1] Перевести слово с русского на английский" );
    Console.WriteLine( "[2] Перевести слово с английского на русский" );
    Console.WriteLine( "[3] Добавить новое слово в словарь" );
}

static Dictionary<string, string> BuildDictionary()
{
    var dictionary = new Dictionary<string, string>();
    string path = "C:/study/tl_practice_2024/Dictionary/Dictionary/bin/Debug/net8.0/Dictionary.txt";
    if ( !File.Exists( path ) )
    {
        File.Create( path );
    }
    using ( StreamReader reader = new StreamReader( path ) )
    {
        string line;
        while ( ( line = reader.ReadLine() ) != null )
        {
            string[] words = line.Split( ' ' );
            dictionary.Add( words[ 0 ], words[ 1 ] );
        }
    }
    return dictionary;
}

static string RusProcess()
{
    string word = Console.ReadLine().ToLower();
    int count = 0;
    while ( string.IsNullOrWhiteSpace( word ) ||
        !Regex.IsMatch( word, @"^[ёЁа-яА-Я'-]+$" ) ||
        Regex.IsMatch( word, @"^['-]+$" ) )
    {
        count++;
        if ( count == tryLimit )
        {
            throw new Exception();
        }
        Console.Write( "Ошибка! Введите слово, состоящее только из букв " +
            "русского алфавита, тире и апострофа.\nРусский: " );
        word = Console.ReadLine();
    }
    return word;
}

static string EngProcess()
{
    string word = Console.ReadLine().ToLower();
    int count = 0;
    while ( string.IsNullOrWhiteSpace( word ) ||
        !Regex.IsMatch( word, @"^[a-zA-Z'-]+$" ) ||
        Regex.IsMatch( word, @"^['-]+$" ) )
    {
        count++;
        if ( count == tryLimit )
        {
            throw new Exception();
        }
        Console.Write( "Ошибка! Введите слово, состоящее только из букв " +
            "латинского алфавита, тире и апострофа.\nАнглийский: " );
        word = Console.ReadLine();
    }
    return word;
}

void RusToEng()
{
    Console.Write( "Введите слово на русском языке.\nРусский: " );
    try
    {
        string word = RusProcess();
        if ( dictionary.ContainsKey( word ) )
        {
            Console.WriteLine( $"Английский: {dictionary[ word ]}" );
        }
        else
        {
            Console.WriteLine( "Данного слова нет в словаре. Добавить в словарь?" );
        }
    }
    catch ( Exception )
    {
        return;
    }
}

void EngToRus()
{
    Console.Write( "Введите слово на английском языке.\nАнглийский: " );
    try
    {
        string word = EngProcess();
        if ( dictionary.ContainsValue( word ) )
        {
            foreach ( var translation in dictionary )
            {
                if ( translation.Value == word )
                {
                    Console.WriteLine( $"Русский: {translation.Key}" );
                }
            }
        }
        else
        {
            Console.WriteLine( "Данного слова нет в словаре. Добавить в словарь?" );
        }
    }
    catch ( Exception )
    {
        return;
    }
}

void AddWords()
{
    string path = "Dictionary.txt";
    Console.Write( "Введите слово на русском языке: " );
    try
    {
        string rusWord = RusProcess();
        while ( dictionary.ContainsKey( rusWord ) )
        {
            Console.Write( "Это слово уже есть в словаре!\n" +
                "Введите другое слово на русском языке: " );
            rusWord = RusProcess();
        }
        Console.Write( "Введите перевод этого слова на английский: " );
        string engWord = EngProcess();
        while ( dictionary.ContainsValue( engWord ) )
        {
            Console.Write( "Это слово уже есть в словаре!\n" +
                "Введите другой перевод этого слова на английский: " );
            engWord = EngProcess();
        }
        using ( StreamWriter writer = new StreamWriter( path, true ) )
        {
            writer.WriteLine( $"{rusWord} {engWord}" );
        }
        Console.WriteLine( "Слово успешно добавлено в словарь!" );
        dictionary = BuildDictionary();
    }
    catch ( Exception )
    {
        return;
    }
}