using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Khởi tạo danh sách và nạp dữ liệu mẫu
            List<Student> listStudents = InitMockData();

            // Thông báo nhỏ ở đầu
            Show_Console.Print($"Hệ thống đã khởi động với {listStudents.Count} sinh viên mẫu.", 2, 0, ConsoleColor.DarkGray);

            bool exit = false;
            while (!exit)
            {
                // 2. Hiển thị Menu
                ShowMenu();

                // 3. Nhập lựa chọn
                string choice = Console.ReadLine();

                // 4. Điều hướng xử lý
                switch (choice)
                {
                    case "1":
                        FeatureAddStudent(listStudents);
                        break;

                    case "2":
                        FeatureShowList(listStudents);
                        break;

                    case "0":
                        exit = true;
                        Show_Console.Print("Đang thoát chương trình...", 20, 10, ConsoleColor.Yellow);
                        break;

                    default:
                        Show_Console.Print("Lựa chọn không hợp lệ! Vui lòng chọn lại.", 20, 10, ConsoleColor.Red);
                        System.Threading.Thread.Sleep(1000); // Dừng 1 giây để đọc lỗi
                        break;
                }
            }
        }


        /// <summary>
        /// Hàm tạo dữ liệu giả lập ban đầu
        /// </summary>
        static List<Student> InitMockData()
        {
            return new List<Student>()
            {
                new Student("SV001", "Nguyễn Văn A", 8.5f, "CNTT"),
                new Student("SV002", "Trần Thị B", 6.0f, "Kinh Tế"),
                new Student("SV003", "Lê Văn C", 9.2f, "Ngôn Ngữ Anh"),
                new Student("SV004", "Phạm Thị D", 4.5f, "Quản Trị KD"),
                new Student("SV005", "Hoàng Văn E", 7.0f, "CNTT")
            };
        }

        /// <summary>
        /// Hàm vẽ giao diện Menu chính
        /// </summary>
        static void ShowMenu()
        {
            Console.Clear();
            int y = 2; // Vị trí dòng bắt đầu vẽ Menu

            Show_Console.Print("MENU QUẢN LÝ SINH VIÊN", 20, y++, ConsoleColor.Cyan);
            Show_Console.Print("1. Thêm sinh viên", 20, y++, ConsoleColor.White);
            Show_Console.Print("2. Hiển thị danh sách", 20, y++, ConsoleColor.White);
            Show_Console.Print("0. Thoát", 20, y++, ConsoleColor.White);

            y++; // Cách 1 dòng
            Show_Console.Print("Chọn chức năng: ", 20, y, ConsoleColor.Green);
            Console.SetCursorPosition(36, y); // Đặt con trỏ ngay sau dòng chữ để nhập
        }

        /// <summary>
        /// Chức năng 1: Thêm sinh viên mới vào danh sách
        /// </summary>
        static void FeatureAddStudent(List<Student> list)
        {
            Console.Clear();
            int y = 3; // Dòng bắt đầu hiển thị form

            Show_Console.Print("THÊM SINH VIÊN MỚI", 20, 1, ConsoleColor.Magenta);

            Student sv = new Student();

            sv.Input(ref y);

            list.Add(sv);

            Show_Console.Print("Thêm thành công! Nhấn Enter để quay lại.", 5, y + 1, ConsoleColor.Green);
            Console.ReadLine();
        }

        /// <summary>
        /// Chức năng 2: Hiển thị toàn bộ danh sách sinh viên
        /// </summary>
        static void FeatureShowList(List<Student> list)
        {
            Console.Clear();
            int y = 3; // Dòng bắt đầu hiển thị danh sách

            Show_Console.Print($"DANH SÁCH SINH VIÊN (Tổng: {list.Count})", 20, 1, ConsoleColor.Magenta);

            if (list.Count == 0)
            {
                Show_Console.Print("Danh sách hiện đang trống!", 5, y, ConsoleColor.DarkGray);
                y++;
            }
            else
            {
                Show_Console.Print("Thông tin chi tiết:", 5, y++, ConsoleColor.White);

                foreach (var item in list)
                {
                    item.Show(ref y);
                }
            }

            Show_Console.Print("Nhấn Enter để quay lại Menu.", 5, y + 2, ConsoleColor.Green);
            Console.ReadLine();
        }
    }
}
