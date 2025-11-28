using System;
using System.Text;

namespace Lab01_GuessNumber
{
    class Program
    {
        public static string Print(string s, int x, int y, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(s ?? "Chưa bỏ chuỗi vào print");
            Console.ResetColor();
            return (s != null) ? s : "Chưa bỏ chuỗi vào print";
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int currentY = 0;

            Random random = new Random();
            int targetNumber = random.Next(100, 999);

            Print("=== CHƯƠNG TRÌNH ĐOÁN SỐ ===", 10, currentY++, ConsoleColor.Cyan);
            Print("Máy tính đã phát sinh một số từ 100 đến 999.", 0, currentY++, ConsoleColor.Gray);
            Print($"Bạn có 7 lần đoán {targetNumber}.", 0, currentY++, ConsoleColor.Gray);
            currentY++;

           
            string targetString = targetNumber.ToString();


            int attempt = 1;
            const int MAX_GUESS = 7;
            string guess = "";
            string feedback = "";

            while (feedback != "+++" && attempt <= MAX_GUESS)
            {
                Print($"Lần đoán thứ {attempt}: ", 0, currentY, ConsoleColor.Yellow);

                Console.SetCursorPosition(17, currentY);
                guess = Console.ReadLine();

                if (string.IsNullOrEmpty(guess) || guess.Length != 3)
                {
                    currentY++;
                    Print("Vui lòng nhập số có 3 chữ số!", 0, currentY++, ConsoleColor.Red);
                    continue;
                }

                feedback = GetFeedback(targetString, guess);

                currentY++;
                Print("Phản hồi từ máy tính: ", 0, currentY, ConsoleColor.White);

                if (feedback == "+++")
                    Print(feedback, 22, currentY, ConsoleColor.Green);
                else
                    Print(feedback, 22, currentY, ConsoleColor.Magenta);

                attempt++;
                currentY += 2;
            }

            if (feedback == "+++")
                Print($"Chúc mừng! Bạn đã đoán đúng số {targetNumber} sau {attempt - 1} lần.", 0, currentY, ConsoleColor.Green);
            else
            {
                Print("Người chơi thua cuộc!", 0, currentY++, ConsoleColor.Red);
                Print($"Số cần đoán là: {targetNumber}", 0, currentY, ConsoleColor.White);
            }

            Console.ReadLine();
        }

        static string GetFeedback(string target, string guess)
        {
            string feedback = "";
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == guess[i])
                    feedback += "+";
                else if (target.Contains(guess[i].ToString()))
                    feedback += "?";
            }
            return feedback;
        }
    }
}