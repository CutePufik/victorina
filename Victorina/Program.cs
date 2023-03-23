


class myClass
{
    static String pathEasyQuestions = "./levels-file/easyLevel.txt";
    private static String pathMediumQuestions = "./levels-file/mediumLevel.txt";
    private static String pathHardQuestions = "./levels-file/hardLevel.txt";
    private static String level = ""; 
  
    
    static String[] allAnswersWithBool = File.ReadAllLines(askForChooseLevel()).Select(s => s.Split('?')[1]).ToArray();
    static String[] allAnswers = getAllAnswersArray(allAnswersWithBool);
    static String[] rigthAnswers = getRigthAnswersArray(allAnswersWithBool);
    static String[] questions = File.ReadAllLines(askForChooseLevel()).Select(s => s.Split('?')[0] + "?").ToArray();
    static int countOfRightAnswers = 0;
    static int indexOfCurrentAnswer = 0;
    


    static void Main(string[] args)
    {
        /// главная фича моего решения - оно работает

        printInfo(); 

        askForGame();

        askForChooseLevel();
        

        mainGame(); 

        printScore(); 

        askForRecordResult();

        writeTableOfWinners(); 

    }

   
     static String askForChooseLevel()
    {
        
        while (!(level.Equals("простая") || level.Equals("средняя") || level.Equals("сложная")))
        {
            Console.WriteLine("Выберите сложность: простая , средняя или сложная");
            level = Console.ReadLine().Trim().ToLower();
        }
        
        switch (level)
        {
            case "простая":
                return pathEasyQuestions;
            case "средняя":
                return pathMediumQuestions;
            case "сложная":
                return pathHardQuestions;
            default:
                throw new Exception();
                
        }
    }

    private static void askForRecordResult()
    {
        Console.WriteLine("Записываем Ваш результат?");
        var answer = Console.ReadLine().Trim().ToLower();
        if (answer != "да")
        {
            Console.WriteLine("Ну ладно :(");
            Console.WriteLine("Спасибо за игру. Всего хорошего!");
            Environment.Exit(0);
        }
    }

    private static void askForGame()
    {
        Console.WriteLine("Начнем игру?");
        var s = Console.ReadLine().Trim().ToLower();
        if (s != "да")
        {
            Console.WriteLine("Ну ладно :(");
            Environment.Exit(0);
        }
    }

    private static void writeTableOfWinners()
    {
        Console.WriteLine("Представьтесь");
        var name = Console.ReadLine().Trim();
        
        try
        {
            using (var fs = new FileStream("recordtable.txt", FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine($"имя : {name},  очков : {countOfRightAnswers}   сложность:  {level}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        Console.WriteLine("Записываю Ваш результат в таблицу лидеров");
    }

    

    private static void printScore()
    {
        Console.WriteLine($"Правильных ответов : {countOfRightAnswers}");
    }

    private static void mainGame()
    {
        int indexOfCurrentAnswer = 0;
        for (int i = 0; i < questions.Length; i++)
        {
            Console.WriteLine($"{i + 1} вопрос");
            Console.WriteLine(questions[i]);
            Print(allAnswers.Skip(indexOfCurrentAnswer).Take(4).ToArray());
            
            indexOfCurrentAnswer += 4;
            parseAnswer();
        }
    }

    private static void parseAnswer()
    {
        
        Console.WriteLine("Ваш ответ?");
        var answer = Console.ReadLine().Trim().ToLower();
        var rigthAnswer = rigthAnswers[indexOfCurrentAnswer].Trim().ToLower();
        
        if (answer == rigthAnswer)
        {
            Console.WriteLine("Правильно!");
            countOfRightAnswers++;
        }
        else
        {
            Console.WriteLine("Неправильно");
        }
        indexOfCurrentAnswer++;
    }


    /// <summary>
    /// массив верных ответов
    /// </summary>
    /// <param name="allAnswersWithBool"></param>
    /// <returns></returns>
    static String[] getRigthAnswersArray(String[] allAnswersWithBool)
    {
        var c = 0;
        String[] rightAnswers = new string[allAnswersWithBool.Length];
        for (int i = 0; i < allAnswersWithBool.Length; i++)
        {
            String[] str = allAnswersWithBool[i].Replace("||", "|").Split("|").Distinct().Skip(1).ToArray();
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j].Contains("True"))
                {
                    rightAnswers[c] = str[j].Split('=')[0];
                    c++;
                }
            }
        }

        return rightAnswers;
    }

    /// <summary>
    /// массив всех ответов для перечисление
    /// </summary>
    /// <param name="allAnswersWithBool"></param>
    /// <returns></returns>
    static String[] getAllAnswersArray(String[] allAnswersWithBool)
    {
        var c = 0;
        String[] allAnswers = new string[allAnswersWithBool.Length * 4];
        for (int i = 0; i < allAnswersWithBool.Length; i++)
        {
            String[] str = allAnswersWithBool[i].Replace("||", "|").Split("|").Distinct().Skip(1).ToArray();
            for (int j = 0; j < str.Length; j++)
            {
                allAnswers[c] = str[j].Split('=')[0];
                c++;
            }
        }

        return allAnswers;
    }


    /// преобразует файл в массив строк
    static String[] arrayOfFileStrings()
    {
        var path = "./bank.txt";
        String[] lines = File.ReadAllLines(path);
        return lines;
    }

    /// <summary>
    /// вывод правил
    /// </summary>
    static void printInfo()
    {
        string[] s =
        {
            "Добро пожаловать на нашу викторину! Сегодня мы приготовили для вас множество интересных вопросов,которые " +
            "позволят проверить вашу эрудицию и узнать что-то новое.", 
            "Давайте познакомимся с правилами игры!",
            "Правила игры для викторины:",
            "Викторина состоит из множества раундов, каждый из которых содержит вопрос и 4 варианта ответа.",
            "после выведения вопроса,вам будет предложено ответить на него",
            "регистр не играет роли,но следует давать ответ с верным написанием",
            "в конце каждого раунда будет показано: правильный ответ или нет",
            "по истечению вопросов будет выведен ваш результат и будет предложено записать Вас в таблицу с результатами",
            "Наслаждайтесь игрой и желаем удачи!"
        };
        for (int i = 0; i < s.Length; i++)
        {
            Console.WriteLine(s[i]);
        }

    }


    public static void Print<T>(T[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            Console.Write(arr[i]  + "  ");
        }

        Console.WriteLine();
    }
}