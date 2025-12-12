using System;
using System.Windows.Forms;

namespace Buoi3_3
{
    public partial class Edit : Form
    {
        private Account _acc;

        public Edit(Account acc)
        {
            InitializeComponent();
            _acc = acc;
            LoadData();
        }

        private void LoadData()
        {
            if (_acc != null)
            {
                txtMSSV.Text = _acc.MSSV;
                txtTen.Text = _acc.Ten;
                txtDiemSo.Text = _acc.Diem.ToString();

                if (comboBox1.Items.Contains(_acc.Khoa))
                    comboBox1.SelectedItem = _acc.Khoa;
                else
                    comboBox1.SelectedIndex = 0;

                txtMSSV.ReadOnly = true; // Không cho sửa MSSV
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!float.TryParse(txtDiemSo.Text, out float diem))
                {
                    MessageBox.Show("Điểm phải là số!");
                    return;
                }

                _acc.Ten = txtTen.Text;
                _acc.Khoa = comboBox1.SelectedItem.ToString();
                _acc.Diem = diem;

                MessageBox.Show("Cập nhật thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}