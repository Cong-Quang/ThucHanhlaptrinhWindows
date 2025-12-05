using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi2_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int countNam = 0;
        public int countNu = 0;

        private Action OnDataChanged;

        private List<Student> add_MauData()
        {
            List<Student> danhSachSV = new List<Student>();
            string[] ho = { "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng" };
            string[] ten = { "Hùng", "Dũng", "Lan", "Mai", "Hương" };
            string[] nganh = { "CNTT", "Kế Toán", "QTKD" };
            Random rand = new Random();

            for (int i = 1; i <= 10; i++)
            {
                danhSachSV.Add(new Student
                {
                    maSinhVien = "SV" + i.ToString("D3"),
                    hoTen = ho[rand.Next(ho.Length)] + " " + ten[rand.Next(ten.Length)],
                    gioiTinh = rand.Next(0, 2),
                    diemTB = (float)Math.Round(rand.NextDouble() * 10, 1),
                    chuyenNgang = nganh[rand.Next(nganh.Length)]
                });
            }
            return danhSachSV;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbb_ChuyenNganh.SelectedIndex = 0;
            rdo_Nam.Checked = true;

            OnDataChanged += UpdateNoLogin;


            dataGrv.Rows.Clear();
            List<Student> students = add_MauData();
            foreach (var item in students)
            {
                int index = dataGrv.Rows.Add();
                dataGrv.Rows[index].Cells[0].Value = item.maSinhVien;
                dataGrv.Rows[index].Cells[1].Value = item.hoTen;
                dataGrv.Rows[index].Cells[2].Value = (item.gioiTinh == 1) ? "Nam" : "Nữ";
                dataGrv.Rows[index].Cells[3].Value = item.diemTB;
                dataGrv.Rows[index].Cells[4].Value = item.chuyenNgang;
            }

            OnDataChanged?.Invoke();

            dataGrv.CellClick += DataGrv_CellClick;
            txb_TongNam.Text = countNam.ToString();
            txb_TongNu.Text = countNu.ToString();
        }

        private void UpdateNoLogin()
        {
            countNam = 0;
            countNu = 0;
            txb_TongNam.Text = "0";
            txb_TongNu.Text = "0";

            foreach (DataGridViewRow row in dataGrv.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                string gioitinh = row.Cells[2].Value.ToString();
                if (gioitinh == "Nam") countNam++;
                else countNu++;
            }
          
        }

        private int FindRowIndex(Func<DataGridViewRow, bool> predicate)
        {
            for (int i = 0; i < dataGrv.Rows.Count; i++)
            {
                if (dataGrv.Rows[i].IsNewRow) continue;

                if (predicate(dataGrv.Rows[i]))
                    return i;
            }
            return -1;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txb_msv.Text)) return;

                string msv = txb_msv.Text;

                int index = FindRowIndex(row =>
                    row.Cells[0].Value != null &&
                    row.Cells[0].Value.ToString() == msv
                );


                if (index == -1)
                {
                    index = dataGrv.Rows.Add();
                    MessageBox.Show("Thêm mới thành công!");
                }
                else
                {
                    MessageBox.Show("Cập nhật thành công!");
                }

                dataGrv.Rows[index].Cells[0].Value = txb_msv.Text;
                dataGrv.Rows[index].Cells[1].Value = txb_HoTen.Text;
                dataGrv.Rows[index].Cells[2].Value = rdo_Nam.Checked ? "Nam" : "Nữ";
                dataGrv.Rows[index].Cells[3].Value = txb_DiemTB.Text;
                dataGrv.Rows[index].Cells[4].Value = cbb_ChuyenNganh.Text;

                ResetForm();

                OnDataChanged?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                int index = FindRowIndex(row =>
                    row.Cells[0].Value != null &&
                    row.Cells[0].Value.ToString() == txb_msv.Text
                );

                if (index == -1)
                {
                    MessageBox.Show("Không tìm thấy SV!");
                    return;
                }

                dataGrv.Rows.RemoveAt(index);
                MessageBox.Show("Xóa thành công!");
                ResetForm();

                OnDataChanged?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void DataGrv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dataGrv.Rows[e.RowIndex];
            if (row.Cells[0].Value != null)
            {
                txb_msv.Text = row.Cells[0].Value.ToString();
                txb_HoTen.Text = row.Cells[1].Value.ToString();
                string gt = row.Cells[2].Value.ToString();
                if (gt == "Nam") rdo_Nam.Checked = true; else rdo_Nu.Checked = true;
                txb_DiemTB.Text = row.Cells[3].Value.ToString();
                cbb_ChuyenNganh.Text = row.Cells[4].Value.ToString();
            }
        }

        private void ResetForm()
        {
            txb_msv.Clear();
            txb_HoTen.Clear();
            txb_DiemTB.Clear();
            rdo_Nam.Checked = true;
            cbb_ChuyenNganh.SelectedIndex = 0;
            txb_msv.Focus();
        }
    }
}
