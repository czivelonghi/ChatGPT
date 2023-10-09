using System.Text;

namespace ChatGPT.Common
{
    public class ChatConsole
    {
        public static void WriteWelcome()
        {
            Console.WriteLine("ChatGPT Bot...press ESC to stop");
        }

        public static void WriteStreamWelcome()
        {
            Console.WriteLine("ChatGPT Streaming Bot...press ESC to stop");
        }

        public static void WritePrompt()
        {
            Console.WriteLine("");
            Console.Write("Enter a prompt: ");
        }

        public static void WriteBusy()
        {
            WriteLine("Fetching...");
        }

        public static void Write(string message)
        {
            Console.Write(message);
        }

        public static void WriteLine(string message) 
        {
            Console.WriteLine("");
            Console.WriteLine(message);
        }

        public static string ReadAssistantContent()
        {
            Console.WriteLine("");
            Console.Write("Enter assistant content (i.e. Im a helpful assistant or hit enter to use default): ");

            if (TryReadInput(out string assistantContent))
            {
                return assistantContent;
            }
            return "Im a helpful assistant";
        }

        public static bool TryReadInput(out string input) 
        {
            StringBuilder sb = new();

            do
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
                else if (keyInfo.Key == ConsoleKey.Enter)
                    break;
                else if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    Console.Write(" \b");
                    sb.Length--;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && sb.Length == 0)
                    Console.Write(" ");
                else
                    sb.Append(keyInfo.KeyChar);

            } while (true);

            input = sb.ToString();

            return input.Length > 0;
        }
    }
}
