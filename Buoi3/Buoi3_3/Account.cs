using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Buoi3_3
{
    public class Account : INotifyPropertyChanged
    {
        private string _mssv;
        private string _ten;
        private string _khoa;
        private float _diem;

        public string MSSV
        {
            get { return _mssv; }
            set { _mssv = value; OnPropertyChanged("MSSV"); }
        }

        public string Ten
        {
            get { return _ten; }
            set { _ten = value; OnPropertyChanged("Ten"); }
        }

        public string Khoa
        {
            get { return _khoa; }
            set { _khoa = value; OnPropertyChanged("Khoa"); }
        }

        public float Diem
        {
            get { return _diem; }
            set { _diem = value; OnPropertyChanged("Diem"); }
        }

        public static BindingList<Account> ListAccounts = new BindingList<Account>();

        // Constructor mặc định: Khởi tạo chuỗi rỗng để tránh NullReferenceException
        public Account()
        {
            _mssv = "";
            _ten = "";
            _khoa = "";
            _diem = 0;
        }

        // Constructor có tham số: Dùng ?? "" để đảm bảo không bao giờ nhận null
        public Account(string mssv, string ten, string khoa, float diem)
        {
            MSSV = mssv ?? "";
            Ten = ten ?? "";
            Khoa = khoa ?? "";
            Diem = diem;
        }

        // Hàm thêm mới có kiểm tra trùng và check null
        public static bool AddAccount(Account acc)
        {
            if (acc == null || string.IsNullOrEmpty(acc.MSSV)) return false;

            foreach (var item in ListAccounts)
            {
                // Nếu item trong list bị lỗi null thì bỏ qua
                if (item == null || item.MSSV == null) continue;

                // So sánh (không phân biệt hoa thường)
                if (item.MSSV.Trim().ToLower() == acc.MSSV.Trim().ToLower())
                {
                    return false; // Trùng MSSV -> Trả về false
                }
            }

            ListAccounts.Add(acc);
            return true; // Thêm thành công
        }

        public static void SaveToIni(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                for (int i = 0; i < ListAccounts.Count; i++)
                {
                    if (ListAccounts[i] == null) continue;
                    sw.WriteLine($"[Student_{i}]");
                    sw.WriteLine($"MSSV={ListAccounts[i].MSSV}");
                    sw.WriteLine($"Ten={ListAccounts[i].Ten}");
                    sw.WriteLine($"Khoa={ListAccounts[i].Khoa}");
                    sw.WriteLine($"Diem={ListAccounts[i].Diem}");
                    sw.WriteLine("");
                }
            }
        }

        public static void LoadFromIni(string filePath)
        {
            ListAccounts.Clear();
            if (!File.Exists(filePath)) return;

            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            Account currentAcc = null;

            foreach (string line in lines)
            {
                string trimLine = line.Trim();
                if (string.IsNullOrEmpty(trimLine)) continue;

                if (trimLine.StartsWith("[") && trimLine.EndsWith("]"))
                {
                    if (currentAcc != null) ListAccounts.Add(currentAcc);
                    currentAcc = new Account();
                }
                else if (currentAcc != null)
                {
                    string[] parts = trimLine.Split(new char[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        switch (key)
                        {
                            case "MSSV": currentAcc.MSSV = value; break;
                            case "Ten": currentAcc.Ten = value; break;
                            case "Khoa": currentAcc.Khoa = value; break;
                            case "Diem": float.TryParse(value, out float d); currentAcc.Diem = d; break;
                        }
                    }
                }
            }
            if (currentAcc != null) ListAccounts.Add(currentAcc);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}