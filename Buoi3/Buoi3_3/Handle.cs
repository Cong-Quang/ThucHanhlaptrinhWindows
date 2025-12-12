using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq; // Bắt buộc có để dùng chức năng Lọc (Where)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi3_3
{
    public partial class Handle : Form
    {
        public Handle()
        {
            InitializeComponent();
            dataGridView1.DataSource = Account.ListAccounts;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add formAdd = new Add();
            formAdd.ShowDialog();
            dataGridView1.Refresh();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = "INI Files|*.ini|All Files|*.*" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Account.LoadFromIni(dlg.FileName);
                dataGridView1.DataSource = null; // Reset datasource để refresh binding
                dataGridView1.DataSource = Account.ListAccounts;
                MessageBox.Show("Đã tải dữ liệu thành công!");
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = "INI Files|*.ini|All Files|*.*", FileName = "data.ini" };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Account.SaveToIni(dlg.FileName);
                MessageBox.Show("Đã lưu dữ liệu thành công!");
            }
        }

        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa hết dữ liệu hiện tại?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Account.ListAccounts.Clear();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Account.ListAccounts.Count)
            {
                Account selectedAcc = dataGridView1.Rows[e.RowIndex].DataBoundItem as Account;

                if (selectedAcc != null)
                {
                    Edit formEdit = new Edit(selectedAcc);
                    formEdit.ShowDialog();
                }
            }
        }

        private void FindKhoa()
        {
            try
            {
                string keyword = toolStripTextBox1.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(keyword) || keyword == "tìm kiếm khoa" || keyword == "tìm kiếm...")
                {
                    dataGridView1.DataSource = Account.ListAccounts;
                }
                else
                {
                    var filteredList = Account.ListAccounts
                        .Where(acc => acc.Khoa.ToLower().Contains(keyword))
                        .ToList();

                    dataGridView1.DataSource = filteredList;

                    if (filteredList.Count == 0)
                    {
                        MessageBox.Show($"Không tìm thấy sinh viên nào thuộc khoa: {toolStripTextBox1.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            FindKhoa();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text == "Tìm kiếm khoa")
            {
                toolStripTextBox1.Text = "";
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindKhoa();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }
    }
}