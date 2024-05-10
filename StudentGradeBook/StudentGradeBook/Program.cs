using StudentGradeBook;

//Deklarowanie obiektu
var student = new StudentInFile("Jan Kowalski", 'M', 16);

student.GradeAdded += StudentGradeAdded;

void StudentGradeAdded(object sender, EventArgs args)
{
    Console.WriteLine("Dodano nowa ocene");
}

// Menu
var menuCount = 3;

while (true)
{
    // Powitanie
    Console.Clear();
    Console.WriteLine("Witamy w Programie Dziennik Ucznia");
    Console.WriteLine("============================================\n");
    Console.WriteLine("Menu: ");
    Console.WriteLine("1    - Dodaj ocene");
    Console.WriteLine("2    - Wyswietl oceny");
    Console.WriteLine("3    - Wyswietl statystyki ocen");
    Console.WriteLine("0, q - Wyjdz z aplikacji");
    var selection = ReadMenuSelection(menuCount);
    Console.Clear();

    if (selection == 0)
    {
        break;
    }

    try
    {
        switch (selection)
        {
            case 1:
                Console.WriteLine("Dodawanie oceny (A - F lub 1 - 6 +/-), wpisz \'q\' aby przerwac");
                Console.WriteLine("============================================\n");
                while (true)
                {
                    var input = ReadString("Podaj ocene");
                    if (input == "q")
                    {
                        break;
                    }
                    student.AddGrade(input);
                }

                break;

            case 2:
                Console.WriteLine("Wyswietlenie wszystkich ocen");
                Console.WriteLine("============================================\n");
                var grades = student.GetGrades();

                if (grades.Count == 0)
                {
                    Console.WriteLine("Dziennik ocen jest pusty.");
                }
                else
                {
                    int count = 0;
                    for (int i = 0; i < grades.Count; i++)
                    {
                        // Wyswietlenie ostatniej oceny bez przecinka
                        if (i < grades.Count - 1)
                        {
                            Console.Write($"{grades[i]},\t");
                        }
                        else
                        {
                            Console.Write($"{grades[i]}\n");
                        }

                        // Przejscie do nowej lini co 10 elementow
                        if (count == 10)
                        {
                            Console.Write("\n");
                            count = 0;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }
                break;

            case 3:
                Console.WriteLine("Wyswietlenie staystyk wszystkich ocen");
                Console.WriteLine("============================================\n");
                var statistics = student.GetStatistics();

                if (statistics.Average == 0)
                {
                    Console.WriteLine("Dziennik ocen jest pusty.");
                }
                else
                {
                    Console.WriteLine($"Srednia ocena szkolna:\t{statistics.AverageGrade}");
                    Console.WriteLine($"Srednia ocena:\t\t{statistics.Average.ToString("N2")}");
                    Console.WriteLine($"Najnizsza ocena:\t{statistics.Min.ToString("N2")}");
                    Console.WriteLine($"Najwyzsza ocena:\t{statistics.Max.ToString("N2")}\n");
                }
                break;

            default:
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Zlapano wyjatek: {e.Message}");

    }
    finally
    {
        ReadString("Wcisnij ENTER aby powrocic do menu");
    }
}

string ReadString(string input)
{
    while (true)
    {
        Console.Write($"{input}: ");
        var value = Console.ReadLine();
        value = value.Trim();

        if (value != null)
        {
            return value;
        }
        else
        {
            Console.WriteLine("Nic nie wrprowadzono. Sprobuj jeszcze raz!");
        }
    }
}
int ReadMenuSelection(int max)
{
    while (true)
    {
        var value = ReadString("Wybor");

        if (value == "q")
        {
            value = "0";
        }

        if (int.TryParse(value, out int selection) && selection >= 0 && selection <= max)
        {
            return selection;
        }
        else
        {
            Console.WriteLine("Zly wybor. Sprobuj jeszcze raz!");
        }
    }
}
