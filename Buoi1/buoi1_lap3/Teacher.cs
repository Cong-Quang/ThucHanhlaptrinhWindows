using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap3
{
    public class Teacher : Person
    {
        public string Address { get; set; }

        public Teacher() { }
        public Teacher(string id, string name, string address) : base(id, name)
        {
            this.Address = address;
        }

        public override void Input(ref int y)
        {
            base.Input(ref y);

            Show_Console.Print("Nhập Địa chỉ:", 5, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(30, y);
            Address = Console.ReadLine();
            y++;
        }

        // CẬP NHẬT: In theo từng cột cố định
        public override void Show(ref int y)
        {
            // Định nghĩa các mốc tọa độ X giống Student để thẳng hàng
            int xType = 5;
            int xID = 10;
            int xName = 25;
            int xAddress = 50;

            Show_Console.Print("GV", xType, y, ConsoleColor.Green);
            Show_Console.Print("| ID: " + this.ID, xID, y, ConsoleColor.Green);
            Show_Console.Print("| Tên: " + this.FullName, xName, y, ConsoleColor.Green);
            Show_Console.Print("| Đ.Chỉ: " + this.Address, xAddress, y, ConsoleColor.Green);

            y++; // Xuống dòng
        }
    }
}
