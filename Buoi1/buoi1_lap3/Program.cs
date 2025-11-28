using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Khởi tạo danh sách riêng biệt để dễ quản lý theo yêu cầu bài toán
            List<Student> listStudents = InitStudents();
            List<Teacher> listTeachers = InitTeachers();

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                Console.Clear();
                int y = 2; // Dòng bắt đầu hiển thị nội dung

                switch (choice)
                {
                    case "1": AddStudent(listStudents, ref y); break;
                    case "2": AddTeacher(listTeachers, ref y); break;
                    case "3": ShowList(listStudents, "DANH SÁCH SINH VIÊN", ref y); break;
                    case "4": ShowList(listTeachers, "DANH SÁCH GIÁO VIÊN", ref y); break;
                    case "5": ShowCount(listStudents, listTeachers, ref y); break;
                    case "6": ShowStudentsByFaculty(listStudents, "CNTT", ref y); break;
                    case "7": ShowTeachersByAddress(listTeachers, "Quận 9", ref y); break;
                    case "8": ShowBestStudentInFaculty(listStudents, "CNTT", ref y); break;
                    case "9": ShowStudentRankingStats(listStudents, ref y); break;
                    case "0": exit = true; break;
                    default: Show_Console.Print("Sai lựa chọn!", 20, y, ConsoleColor.Red); break;
                }

                if (!exit)
                {
                    Show_Console.Print("Nhấn Enter để quay lại Menu...", 5, y + 2, ConsoleColor.White);
                    Console.ReadLine();
                }
            }
        }

        // --- KHU VỰC HÀM XỬ LÝ (MODULAR) ---

        static void ShowMenu()
        {
            Console.Clear();
            int y = 1;
            Show_Console.Print("QUẢN LÝ NHÂN SỰ TRƯỜNG HỌC", 20, y++, ConsoleColor.Magenta);
            Show_Console.Print("1. Thêm Sinh Viên", 20, y++, ConsoleColor.White);
            Show_Console.Print("2. Thêm Giáo Viên", 20, y++, ConsoleColor.White);
            Show_Console.Print("3. Xuất DS Sinh Viên", 20, y++, ConsoleColor.White);
            Show_Console.Print("4. Xuất DS Giáo Viên", 20, y++, ConsoleColor.White);
            Show_Console.Print("5. Tổng số lượng từng loại", 20, y++, ConsoleColor.White);
            Show_Console.Print("6. Xuất SV thuộc khoa CNTT", 20, y++, ConsoleColor.White);
            Show_Console.Print("7. Xuất GV địa chỉ 'Quận 9'", 20, y++, ConsoleColor.White);
            Show_Console.Print("8. SV điểm cao nhất khoa CNTT", 20, y++, ConsoleColor.White);
            Show_Console.Print("9. Thống kê xếp loại SV", 20, y++, ConsoleColor.White);
            Show_Console.Print("0. Thoát", 20, y++, ConsoleColor.White);
            Show_Console.Print("Chọn chức năng: ", 20, ++y, ConsoleColor.Green);
            Console.SetCursorPosition(36, y);
        }

        // 1. Thêm Sinh Viên
        static void AddStudent(List<Student> list, ref int y)
        {
            Show_Console.Print("THÊM SINH VIÊN", 20, y++, ConsoleColor.Yellow);
            Student sv = new Student();
            sv.Input(ref y);
            list.Add(sv);
            Show_Console.Print("Thêm thành công!", 5, y++, ConsoleColor.Green);
        }

        // 2. Thêm Giáo Viên
        static void AddTeacher(List<Teacher> list, ref int y)
        {
            Show_Console.Print("THÊM GIÁO VIÊN", 20, y++, ConsoleColor.Yellow);
            Teacher gv = new Teacher();
            gv.Input(ref y);
            list.Add(gv);
            Show_Console.Print("Thêm thành công!", 5, y++, ConsoleColor.Green);
        }

        // 3 & 4. Hàm hiển thị danh sách (Generic dùng chung được cho cả 2 list)
        static void ShowList<T>(List<T> list, string title, ref int y) where T : Person
        {
            Show_Console.Print($"{title}", 20, y++, ConsoleColor.Yellow);
            if (list.Count == 0) Show_Console.Print("Danh sách trống.", 5, y++, ConsoleColor.Gray);
            else
            {
                foreach (var item in list) item.Show(ref y);
            }
        }

        // 5. Số lượng từng danh sách
        static void ShowCount(List<Student> sv, List<Teacher> gv, ref int y)
        {
            Show_Console.Print("THỐNG KÊ SỐ LƯỢNG", 20, y++, ConsoleColor.Yellow);
            Show_Console.Print($"Tổng số Sinh viên: {sv.Count}", 5, y++, ConsoleColor.Cyan);
            Show_Console.Print($"Tổng số Giáo viên: {gv.Count}", 5, y++, ConsoleColor.Green);
        }

        static void ShowStudentsByFaculty(List<Student> list, string faculty, ref int y)
        {
            var result = list.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
            ShowList(result, $"SINH VIÊN KHOA {faculty}", ref y);
        }

        static void ShowTeachersByAddress(List<Teacher> list, string location, ref int y)
        {
            var result = list.Where(t => t.Address.ToLower().Contains(location.ToLower())).ToList();
            ShowList(result, $"GIÁO VIÊN Ở {location}", ref y);
        }

        static void ShowBestStudentInFaculty(List<Student> list, string faculty, ref int y)
        {
            Show_Console.Print($"SV ĐIỂM CAO NHẤT KHOA {faculty} ", 20, y++, ConsoleColor.Yellow);

            var facultyStudents = list.Where(s => s.Faculty.Equals(faculty, StringComparison.OrdinalIgnoreCase)).ToList();
            if (facultyStudents.Any())
            {
                float maxScore = facultyStudents.Max(s => s.AverageScore);
                var best = facultyStudents.Where(s => s.AverageScore == maxScore).ToList();
                foreach (var item in best) item.Show(ref y);
            }
            else
            {
                Show_Console.Print("Không có sinh viên nào.", 5, y++, ConsoleColor.Gray);
            }
        }

        static void ShowStudentRankingStats(List<Student> list, ref int y)
        {
            Show_Console.Print("THỐNG KÊ XẾP LOẠI", 20, y++, ConsoleColor.Yellow);

            var stats = list.GroupBy(s =>
            {
                if (s.AverageScore >= 9) return "Xuất sắc";
                if (s.AverageScore >= 8) return "Giỏi";
                if (s.AverageScore >= 7) return "Khá";
                if (s.AverageScore >= 5) return "Trung bình";
                if (s.AverageScore >= 4) return "Yếu";
                return "Kém";
            }).Select(g => new { Rank = g.Key, Count = g.Count() });

            foreach (var item in stats)
            {
                Show_Console.Print($"{item.Rank}: {item.Count} sinh viên", 5, y++, ConsoleColor.White);
            }
        }

        // --- DỮ LIỆU MẪU ---
        static List<Student> InitStudents()
        {
            return new List<Student> {
                new Student("SV1", "Nguyen Van A", 9.5f, "CNTT"),
                new Student("SV2", "Tran Thi B", 7.0f, "Kinh Te"),
                new Student("SV3", "Le Van C", 5.5f, "CNTT"),
                new Student("SV4", "Pham Van D", 8.2f, "CNTT") 
            };
        }

        static List<Teacher> InitTeachers()
        {
            return new List<Teacher> {
                new Teacher("GV1", "Thay Tuan", "Quận 1"),
                new Teacher("GV2", "Co Mai", "Quận 9, TP.HCM"),
                new Teacher("GV3", "Thay Hung", "Thủ Đức")
            };
        }
    }
}
