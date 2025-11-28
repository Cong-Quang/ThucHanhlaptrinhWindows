using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace buoi1_lap3
{
    public class Person
    {
        // Fields & Properties
        protected string id;
        protected string fullName;

        public string ID { get => id; set => id = value; }
        public string FullName { get => fullName; set => fullName = value; }

        public Person() { }
        public Person(string id, string fullName)
        {
            this.id = id;
            this.fullName = fullName;
        }

        // Phương thức Input có thể ghi đè (virtual)
        public virtual void Input(ref int y)
        {
            Show_Console.Print("Nhập Mã số:", 5, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(30, y);
            ID = Console.ReadLine();
            y++;

            Show_Console.Print("Nhập Họ tên:", 5, y, ConsoleColor.Yellow);
            Console.SetCursorPosition(30, y);
            FullName = Console.ReadLine();
            y++;
        }

        // Phương thức Show có thể ghi đè
        public virtual void Show(ref int y)
        {
            // Lớp con sẽ định nghĩa cách hiển thị cụ thể
        }
    }
}
