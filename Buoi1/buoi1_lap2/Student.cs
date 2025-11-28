using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap2
{
    public class Student
    {
        private string studentID;
        private string fullName;
        private float averageScore;
        private string faculty;

        public string StudentID { get => studentID; set => studentID = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public float AverageScore { get => averageScore; set => averageScore = value; }
        public string Faculty { get => faculty; set => faculty = value; }

        public Student() { }
        public Student(string id, string name, float score, string faculty)
        {
            this.studentID = id;
            this.fullName = name;
            this.averageScore = score;
            this.faculty = faculty;
        }

        // Tham số 'y' là dòng bắt đầu in, dùng ref để cập nhật dòng sau khi in xong
        public void Input(ref int y)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int xLabel = 5;  // Cột hiển thị nhãn
            int xInput = 50; // Cột để người dùng nhập liệu

            // Nhập MSSV
            Show_Console.Print("Nhập MSSV:", xLabel, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(xInput, y);
            StudentID = Console.ReadLine();
            y++;

            // Nhập Tên
            Show_Console.Print("Nhập Họ tên Sinh viên:", xLabel, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(xInput, y);
            FullName = Console.ReadLine();
            y++;

            // Nhập Điểm (có kiểm tra lỗi)
            Show_Console.Print("Nhập Điểm TB:", xLabel, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(xInput, y);
            float score;
            // Vòng lặp kiểm tra nhập số
            while (!float.TryParse(Console.ReadLine(), out score))
            {
                y++;
                Show_Console.Print("Sai định dạng! Nhập lại điểm:", xLabel, y, ConsoleColor.Red);
                Console.SetCursorPosition(xInput + 5, y);
            }
            AverageScore = score;
            y++;

            // Nhập Khoa
            Show_Console.Print("Nhập Khoa:", xLabel, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(xInput, y);
            Faculty = Console.ReadLine();
            y++;
        }

        public void Show(ref int y)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // In thông tin sinh viên trên cùng 1 dòng
            string info = string.Format($"MSSV: {StudentID} | Họ Tên: {FullName} | Khoa: {Faculty} | ĐTB: {AverageScore}");

            // Tô màu khác nếu điểm cao (ví dụ >= 8)
            ConsoleColor color = (this.AverageScore >= 8) ? ConsoleColor.Green : ConsoleColor.White;

            Show_Console.Print(info, 5, y, color);
            y++; // Tăng y để sinh viên tiếp theo in ở dòng dưới
        }
    }
}
