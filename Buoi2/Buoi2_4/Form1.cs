using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Buoi2_4
{
    public partial class Form1 : Form
    {
        public delegate void UpdateTotalDelegate();
        private UpdateTotalDelegate _updateTotalHandler;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _updateTotalHandler = new UpdateTotalDelegate(CalculateTotalMoney);

            CreateSampleData();

            _updateTotalHandler?.Invoke();
        }

        private void CreateSampleData()
        {
            var sampleData = new List<string[]>
            {
                new string[] { "1012345678", "Nguyễn Văn An", "Hà Nội", "5000000" },
                new string[] { "1023456789", "Trần Thị Bích", "TP.HCM", "12500000" },
                new string[] { "1034567890", "Lê Văn Cường", "Đà Nẵng", "3200000" },
                new string[] { "1045678901", "Phạm Thị Dung", "Cần Thơ", "8000000" },
                new string[] { "1056789012", "Hoàng Văn Em", "Hải Phòng", "1500000" },
                new string[] { "1067890123", "Ngô Thị Phương", "Huế", "20000000" },
                new string[] { "1078901234", "Đặng Văn Giàu", "Nha Trang", "4500000" },
                new string[] { "1089012345", "Bùi Thị Hoa", "Vinh", "600000" },
                new string[] { "1090123456", "Đỗ Văn Hùng", "Quảng Ninh", "9800000" },
                new string[] { "1101234567", "Lý Thị Khánh", "Đà Lạt", "11000000" }
            };

            foreach (var data in sampleData)
            {
                ListViewItem item = new ListViewItem((lsvAccount.Items.Count + 1).ToString());
                item.SubItems.Add(data[0]);
                item.SubItems.Add(data[1]);
                item.SubItems.Add(data[2]);
                item.SubItems.Add(data[3]);

                // Thêm vào ListView
                lsvAccount.Items.Add(item);
            }
        }

        // Phương thức tính tổng tiền (sẽ được gọi thông qua Delegate)
        private void CalculateTotalMoney()
        {
            decimal total = 0;
            // Duyệt qua tất cả các dòng trong ListView
            foreach (ListViewItem item in lsvAccount.Items)
            {
                // Cột số tiền nằm ở index 4 (subitem thứ 5)
                decimal balance;
                if (decimal.TryParse(item.SubItems[4].Text, out balance))
                {
                    total += balance;
                }
            }
            // Cập nhật lên giao diện (Định dạng N0: thêm dấu phẩy ngăn cách hàng nghìn)
            txtTotalMoney.Text = total.ToString("N0");
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu rỗng
                if (string.IsNullOrWhiteSpace(txtAccountID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtBalance.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra số tiền có hợp lệ không
                if (!decimal.TryParse(txtBalance.Text, out decimal balance))
                {
                    MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tìm xem số tài khoản đã tồn tại chưa
                int existingIndex = -1;
                for (int i = 0; i < lsvAccount.Items.Count; i++)
                {
                    // So sánh Mã TK (cột 1 - subitem index 1)
                    if (lsvAccount.Items[i].SubItems[1].Text == txtAccountID.Text)
                    {
                        existingIndex = i;
                        break;
                    }
                }

                if (existingIndex == -1)
                {
                    // --- THÊM MỚI ---
                    ListViewItem newItem = new ListViewItem((lsvAccount.Items.Count + 1).ToString()); // STT
                    newItem.SubItems.Add(txtAccountID.Text); // Mã TK
                    newItem.SubItems.Add(txtName.Text);      // Tên KH
                    newItem.SubItems.Add(txtAddress.Text);   // Địa chỉ
                    newItem.SubItems.Add(balance.ToString()); // Số tiền

                    lsvAccount.Items.Add(newItem);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // --- CẬP NHẬT ---
                    ListViewItem item = lsvAccount.Items[existingIndex];
                    item.SubItems[2].Text = txtName.Text;
                    item.SubItems[3].Text = txtAddress.Text;
                    item.SubItems[4].Text = balance.ToString();

                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Reset form nhập liệu và tính lại tiền
                ClearInputs();
                _updateTotalHandler?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Tìm dòng cần xóa dựa trên AccountID nhập vào
                int indexToDelete = -1;
                for (int i = 0; i < lsvAccount.Items.Count; i++)
                {
                    if (lsvAccount.Items[i].SubItems[1].Text == txtAccountID.Text)
                    {
                        indexToDelete = i;
                        break;
                    }
                }

                if (indexToDelete != -1)
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        lsvAccount.Items.RemoveAt(indexToDelete);
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại STT
                        UpdateSTT();

                        // Reset input và tính lại tiền
                        ClearInputs();
                        _updateTotalHandler?.Invoke();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy số tài khoản cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lsvAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvAccount.SelectedItems.Count > 0)
            {
                ListViewItem item = lsvAccount.SelectedItems[0];
                txtAccountID.Text = item.SubItems[1].Text;
                txtName.Text = item.SubItems[2].Text;
                txtAddress.Text = item.SubItems[3].Text;
                txtBalance.Text = item.SubItems[4].Text;
            }
        }

        // Nút thoát
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Hàm phụ: Xóa trắng ô nhập liệu
        private void ClearInputs()
        {
            txtAccountID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtBalance.Text = "";
            txtAccountID.Focus();
        }

        // Hàm phụ: Cập nhật lại số thứ tự (STT) sau khi xóa
        private void UpdateSTT()
        {
            for (int i = 0; i < lsvAccount.Items.Count; i++)
            {
                lsvAccount.Items[i].Text = (i + 1).ToString();
            }
        }
    }
}