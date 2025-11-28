using System;

namespace buoi1_lap3
{
    public class Student : Person
    {
        public float AverageScore { get; set; }
        public string Faculty { get; set; }

        public Student() { }
        public Student(string id, string name, float score, string faculty) : base(id, name)
        {
            this.AverageScore = score;
            this.Faculty = faculty;
        }

        public override void Input(ref int y)
        {
            base.Input(ref y);

            Show_Console.Print("Nhập Điểm TB:", 5, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(30, y);
            float score;
            while (!float.TryParse(Console.ReadLine(), out score))
            {
                y++;
                Show_Console.Print("Lỗi! Nhập lại điểm:", 5, y, ConsoleColor.Red);
                Console.SetCursorPosition(30, y);
            }
            AverageScore = score;
            y++;

            Show_Console.Print("Nhập Khoa:", 5, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(30, y);
            Faculty = Console.ReadLine();
            y++;
        }

        // CẬP NHẬT: In theo từng cột cố định
        public override void Show(ref int y)
        {
            // Định nghĩa các mốc tọa độ X cho từng cột
            int xType = 5;
            int xID = 10;
            int xName = 25;
            int xFaculty = 50;
            int xScore = 75;

            Show_Console.Print("SV", xType, y, ConsoleColor.Cyan);
            Show_Console.Print("| ID: " + this.ID, xID, y, ConsoleColor.Cyan);
            Show_Console.Print("| Tên: " + this.FullName, xName, y, ConsoleColor.Cyan);
            Show_Console.Print("| Khoa: " + this.Faculty, xFaculty, y, ConsoleColor.Cyan);
            Show_Console.Print("| ĐTB: " + this.AverageScore, xScore, y, ConsoleColor.Cyan);

            y++; // Xuống dòng
        }
    }
}
