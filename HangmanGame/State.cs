namespace HangmanGame;

public class State(string[] words)
{
    private readonly string _currentWord = words[Random.Shared.Next(0, words.Length)];
    private readonly List<char> _guessedLetters = [];
    private int _lives = 6;

    public static void welcome_screen()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Welcome to hangman.\n");
        Console.WriteLine($"Press Enter to continue... ");
        Console.ResetColor();
        Console.ReadLine();
    }
    
    public void Draw()
    {
        Console.Clear();
        
        var a = "qwertyuiopasdfghjklzxcvbnm".ToCharArray();
        
        for (var i = 0; i < a.Length; i++)
        {
            if (_guessedLetters.Contains(a[i]))
            {
                a[i] = ' ';
            }
        }

        var keyboard = $"""
                        +---+---+---+---+---+---+---+---+---+---+
                        | {a[0]} | {a[1]} | {a[2]} | {a[3]} | {a[4]} | {a[5]} | {a[6]} | {a[7]} | {a[8]} | {a[9]} |
                        +---+---+---+---+---+---+---+---+---+---+
                          | {a[10]} | {a[11]} | {a[12]} | {a[13]} | {a[14]} | {a[15]} | {a[16]} | {a[17]} | {a[18]} |
                          +---+---+---+---+---+---+---+---+---+
                              | {a[19]} | {a[20]} | {a[21]} | {a[22]} | {a[23]} | {a[24]} | {a[25]} |
                              +---+---+---+---+---+---+---+
                        """;
        
            string[] hangmenIterations =
            [

                @"

             --------

             |      |   

             |      O   

             |     /|\  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |     /|  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |     / \  

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |     /   

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |      |  

             |         

             |__________          

 

             ",

                @"

             --------

             |      |   

             |      O   

             |       

             |        

             |__________          

 

             ",

                @"

             --------

             |      |   

             |         

             |       

             |      

             |__________          

 

             ",
                
                @"

             --------

             |          

             |         

             |       

             |      

             |__________          

 

             "

            ];
        
            var picture = hangmenIterations[_lives];
            Console.WriteLine(picture);
            
            Console.WriteLine(keyboard);

            foreach (var letter in _currentWord)
            {
                if (!_guessedLetters.Contains(letter))
                {
                    Console.Write("_");
                    
                }
                else
                {
                    Console.Write(letter);
                }
            }
            
            Console.WriteLine();
    }

    public void Process()
    {
        while (true)
        {
            Draw();
            
            Console.WriteLine("Enter your guess: ");

            var playerInput = Console.ReadLine()!.ToLower();

            if (playerInput.Length != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please only write 1 letter");
                Console.ResetColor();
                Console.ReadLine();
                continue;
            }

            var input = playerInput[0];

            if (!"qwertyuiopasdfghjklzxcvbnm".Contains(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please only write letters");
                Console.ResetColor();
                Console.ReadLine();
                continue;
            }

            if (_guessedLetters.Contains(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a new word");
                Console.ResetColor();
                Console.ReadLine();
                continue;
            }

            var valid = _currentWord.Any(t => input == t);

            if (!valid)
            {
                _lives--;

                Console.Beep(1000, 200);
                Console.Beep(800, 200);
            }
            else
            {
                Console.Beep(1180, 200);
                Console.Beep(1500, 200);
            }

            _guessedLetters.Add(input);

            if (!HasLives())
            {
                Console.WriteLine("Game Over");
                Console.WriteLine("The word was");
                Console.WriteLine(_currentWord);
            }
            else if (HasWon())
            {
                Console.WriteLine("You won!");
            }
            else
            {
                continue;
            }

            break;
        }
    }

    public bool HasLives()
    {
        return _lives > 0;
    }

    public bool HasWon()
    {
        return _currentWord.All(c => _guessedLetters.Any(a => c == a));
    }
}