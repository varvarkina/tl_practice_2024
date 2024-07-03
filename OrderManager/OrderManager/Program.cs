string product, name, address;
int count;

while ( true )
{
    PlaceOrder();
    product = ReadProductName();
    count = ReadProductAmount();
    name = ReadUserName();
    address = ReadAddress();
    ConfirmOrder();
}
static void PlaceOrder()
{
    Console.Write( @"Чтобы оформить новый заказ, нажмите ""Enter""" );
    while ( Console.ReadLine() != "" )
    {
        Console.Write( @"Чтобы оформить новый заказ, нажмите ""Enter""" );
    }
}

static string ReadProductName()
{
    Console.Write( "Введите название товара: " );
    string product = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( product ) )
    {
        Console.WriteLine( "Ошибка! Введите валидное название товара." );
        product = Console.ReadLine();
    }
    return product;
}

static int ReadProductAmount()
{
    Console.Write( "Введите количество товара: " );
    int count;
    while ( ( !int.TryParse( Console.ReadLine(), out count ) ) || !( count >= 1 && count <= 1000 ) )
    {
        Console.WriteLine( "Ошибка! Введите корректное числовое значение не больше 1000." );
    }
    return count;
}

static string ReadUserName()
{
    Console.Write( "Введите имя пользователя: " );
    string name = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( name ) )
    {
        Console.WriteLine( "Ошибка! Введите корректное имя." );
        name = Console.ReadLine();
    }
    return name;
}

static string ReadAddress()
{
    Console.Write( "Введите адрес доставки: " );
    string address = Console.ReadLine();
    while ( string.IsNullOrWhiteSpace( address ) )
    {
        Console.WriteLine( "Ошибка! Введите корректный адрес." );
        address = Console.ReadLine();
    }
    return address;
}

void PrintMenu()
{
    Console.WriteLine( "Menu:" );
    Console.WriteLine( "[1] Подтвердить данные" );
    Console.WriteLine( "[2] Изменить название товара" );
    Console.WriteLine( "[3] Изменить количество товара" );
    Console.WriteLine( "[4] Изменить имя пользователя" );
    Console.WriteLine( "[5] Изменить адрес доставки" );
}

void ConfirmOrder()
{
    string userCommand = "";
    while ( userCommand != "1" )
    {
        Console.WriteLine( $"Здравствуйте, {name}, вы заказали {count} {product} на адрес {address}, все верно?" );
        PrintMenu();
        userCommand = Console.ReadLine();
        switch ( userCommand )
        {
            case "1":
                OrderSucceed();
                break;
            case "2":
                product = ReadProductName();
                break;
            case "3":
                count = ReadProductAmount();
                break;
            case "4":
                name = ReadUserName();
                break;
            case "5":
                address = ReadAddress();
                break;
            default:
                Console.WriteLine( "Ошибка! Некорректная команда." );
                break;
        }
    }
}

void OrderSucceed()
{
    var date = DateTime.Now.AddDays( 3 );
    Console.WriteLine( @$"{name}! Ваш заказ {product} в количестве {count} оформлен! Ожидайте доставку по адресу {address} к {date:dd/MM/yy}." );
}
