using System;
using System.Windows.Forms;

namespace Buoi3_3
{
    public partial class Add : Form
    {
        public Add()
        {
            InitializeComponent();
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMSSV.Text) ||
                    string.IsNullOrWhiteSpace(txtTen.Text) ||
                    string.IsNullOrWhiteSpace(txtDiemSo.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                    return;
                }

                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn Khoa!");
                    return;
                }

                if (!float.TryParse(txtDiemSo.Text, out float diem))
                {
                    MessageBox.Show("Điểm số phải là số!");
                    return;
                }

                string tenKhoa = comboBox1.SelectedItem.ToString();

                Account acc = new Account(
                    txtMSSV.Text.Trim(),
                    txtTen.Text.Trim(),
                    tenKhoa,
                    diem
                );

                bool isAdded = Account.AddAccount(acc);

                if (isAdded)
                {
                    MessageBox.Show("Thêm mới thành công!");
                    txtMSSV.Clear();
                    txtTen.Clear();
                    txtDiemSo.Clear();
                    txtMSSV.Focus();
                }
                else
                {
                    MessageBox.Show($"Mã số '{txtMSSV.Text}' đã tồn tại! Vui lòng kiểm tra lại.");
                    txtMSSV.SelectAll();
                    txtMSSV.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}