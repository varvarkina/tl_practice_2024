using System.Text.RegularExpressions;

const int tryLimit = 5;
Dictionary<string, string> dictionary = BuildDictionary();
string userCommand = "";

PrintWelcomeMessage();

while ( userCommand != "4" )
{
    PrintMenu();
    userCommand = Console.ReadLine();
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
        case "4":
            Console.WriteLine( "Работа приложения завершена. Нажмите любую клавишу, чтобы закрыть это окно." );
            break;
        default:
            Console.WriteLine( @"Неизвестная команда. Введите цифры ""1"", ""2"", ""3"" или ""4""" );
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
    Console.WriteLine( "[4] Выйти из приложения" );
}

static Dictionary<string, string> BuildDictionary()
{
    var dictionary = new Dictionary<string, string>();
    string path = "Dictionary.txt";
    if ( File.Exists( path ) )
    {
        using ( StreamReader reader = new StreamReader( path ) )
        {
            string line;
            while ( ( line = reader.ReadLine() ) != null )
            {
                string[] words = line.Split( ' ' );
                dictionary.Add( words[ 0 ], words[ 1 ] );
            }
        }
    }

    return dictionary;
}

static string RusProcess()
{
    string word = Console.ReadLine();
    int count = 0;
    while ( string.IsNullOrWhiteSpace( word ) ||
        !Regex.IsMatch( word, @"^[ёЁа-яА-Я'-]+$" ) ||
        Regex.IsMatch( word, @"^['-]+$" ) )
    {
        count++;
        if ( count == tryLimit )
        {
            Console.WriteLine( "Вы 5 раз пытались ввести некорректное слово." );
            return null;
        }
        Console.Write( "Ошибка! Введите слово, состоящее только из букв " +
            "русского алфавита, тире и апострофа.\nРусский: " );
        word = Console.ReadLine();
    }

    return word.ToLower();
}

static string EngProcess()
{
    string word = Console.ReadLine();
    int count = 0;
    while ( string.IsNullOrWhiteSpace( word ) ||
        !Regex.IsMatch( word, @"^[a-zA-Z'-]+$" ) ||
        Regex.IsMatch( word, @"^['-]+$" ) )
    {
        count++;
        if ( count == tryLimit )
        {
            Console.WriteLine( "Вы 5 раз пытались ввести некорректное слово." );
            return null;
        }
        Console.Write( "Ошибка! Введите слово, состоящее только из букв " +
            "латинского алфавита, тире и апострофа.\nАнглийский: " );
        word = Console.ReadLine();
    }

    return word.ToLower();
}

void RusToEng()
{
    Console.Write( "Введите слово на русском языке.\nРусский: " );
    string word = RusProcess();
    if ( string.IsNullOrWhiteSpace( word ) )
    {
        return;
    }
    if ( dictionary.ContainsKey( word ) )
    {
        Console.WriteLine( $"Английский: {dictionary[ word ]}" );
    }
    else
    {
        Console.WriteLine( "Данного слова нет в словаре. Добавить в словарь?" );
    }
}

void EngToRus()
{
    Console.Write( "Введите слово на английском языке.\nАнглийский: " );
    string word = EngProcess();
    if ( string.IsNullOrWhiteSpace( word ) )
    {
        return;
    }
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

void AddWords()
{
    string path = "Dictionary.txt";

    Console.Write( "Введите слово на русском языке: " );
    string rusWord = RusProcess();
    if ( string.IsNullOrWhiteSpace( rusWord ) )
    {
        return;
    }
    while ( dictionary.ContainsKey( rusWord ) )
    {
        Console.Write( "Это слово уже есть в словаре!\n" +
            "Введите другое слово на русском языке: " );
        rusWord = RusProcess();
        if ( string.IsNullOrWhiteSpace( rusWord ) )
        {
            return;
        }
    }

    Console.Write( "Введите перевод этого слова на английский: " );
    string engWord = EngProcess();
    if ( string.IsNullOrWhiteSpace( engWord ) )
    {
        return;
    }
    while ( dictionary.ContainsValue( engWord ) )
    {
        Console.Write( "Это слово уже есть в словаре!\n" +
            "Введите другой перевод этого слова на английский: " );
        engWord = EngProcess();
        if ( string.IsNullOrWhiteSpace( engWord ) )
        {
            return;
        }
    }

    using ( StreamWriter writer = File.AppendText( path ) )
    {
        writer.WriteLine( $"{rusWord} {engWord}" );
    }
    Console.WriteLine( "Слово успешно добавлено в словарь!" );

    dictionary = BuildDictionary();
}