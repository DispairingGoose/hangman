using System.ComponentModel.DataAnnotations.Schema;
using HangmanGame;

start:
Console.Clear();

try
{
    // Open the text file using a stream reader.
    using var reader = new StreamReader("words_alpha.txt");

    // Read the stream as a string.
    var text = reader.ReadToEnd();

    var words = text.Split('\n').Select(str => str.Trim()).ToArray();
    var state = new State(words);

    State.welcome_screen();
       
    state.Draw();
    state.Process();
}
catch (IOException e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}

Console.WriteLine("Press Enter to play again...");
Console.ReadLine();
goto start;