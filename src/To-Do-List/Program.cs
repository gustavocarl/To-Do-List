ShowMenu();

return;

static void ShowMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("======================");
        Console.WriteLine("Lista de Tarefas ");
        Console.WriteLine("======================");
        Console.WriteLine("1 - Adicionar Tarefa: ");
        Console.WriteLine("2 - Listar Tarefas: ");
        Console.WriteLine("3 - Remover Tarefa: ");
        Console.WriteLine("4 - Remover Arquivo: ");
        Console.WriteLine("======================");
        Console.WriteLine("0 - Sair");
        Console.WriteLine("======================");
        switch (Console.ReadLine()!)
        {
            case "0":
                Console.WriteLine("Saindo..."); ;
                Thread.Sleep(2000);
                break;

            case "1":
                AddTask();
                continue;
            case "2":
                LoadList();
                continue;
            case "3":
                RemoveTask();
                continue;
            case "4":
                RemoveFile();
                continue;
            default:
                Console.WriteLine("Opção inválida, pressione ENTER para continuar...");
                Console.ReadKey();
                continue;
        }

        break;
    }
}

static void AddTask()
{
    Console.WriteLine("Qual tarefa gostaria de adicionar: ");
    var taskName = "";
    do
    {
        taskName += Console.ReadLine();
        taskName += Environment.NewLine;
    } while (Console.ReadKey().Key != ConsoleKey.Escape);
    SaveList(taskName);
    Console.WriteLine("Tarefa adicionada com sucesso, pressione ENTER para continuar!");
    Console.ReadKey();
}

static void LoadList()
{
    Console.WriteLine("Qual arquivo deseja carregar: ");
    var path = Console.ReadLine()!;
    using StreamReader file = new(path);
    while (file.ReadLine() is { } line)
        Console.WriteLine(line);
    Console.WriteLine("Arquivo carregado com sucesso, pressione ENTER para continuar!");
    Console.ReadKey();
}

static void RemoveTask()
{
    Console.WriteLine("Qual arquivo deseja abrir: ");
    var path = Console.ReadLine()!;
    Console.WriteLine("Qual tarefa deseja apagar: ");
    var task = Console.ReadLine()!;

    var tempFilePath = Path.GetTempFileName();
    using StreamReader reader = new(path);
    using StreamWriter writer = new(tempFilePath);

    var taskFound = false;
    while (reader.ReadLine() is { } line)
    {
        if (string.Compare(line, task, StringComparison.OrdinalIgnoreCase) == 0)
        {
            Console.WriteLine($"Tarefa encontada e removida: {task}");
            taskFound = true;
        }
        else
            writer.WriteLine(line);
    }

    if (!taskFound)
        Console.WriteLine($"Tarefa não encontrada: {task}");

    writer.Close();
    reader.Close();

    File.Delete(path);
    File.Move(tempFilePath, path);

    Console.ReadLine();
}

static void RemoveFile()
{
    Console.WriteLine("Qual arquivo deseja remover: ");
    var path = Console.ReadLine()!;
    File.Delete(path);
    Console.WriteLine("Arquivo deletado com sucesso, pressione ENTER para continuar!");
    Console.ReadKey();
}

static void SaveList(string task)
{
    Console.Clear();
    Console.WriteLine("Onde deseja salvar o arquivo: ");
    var path = Console.ReadLine()!;
    using StreamWriter file = new(path);
    file.WriteLine(task);
    Console.WriteLine(task);
    Console.ReadKey();
}