using System;
using System.Drawing;
using System.Drawing.Text; // Dùng để load Font hệ thống
using System.IO;           // Dùng để xử lý file
using System.Windows.Forms;

namespace Buoi3_2
{
    public partial class Form1 : Form
    {
        // Biến lưu đường dẫn file hiện tại đang mở
        private string currentFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }

        // 1. Sự kiện khi Form Load: Khởi tạo dữ liệu cho ComboBox Font và Size
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load danh sách Font
            foreach (FontFamily fontFamily in new InstalledFontCollection().Families)
            {
                cmbFont.Items.Add(fontFamily.Name);
            }
            cmbFont.SelectedItem = "Tahoma"; // Font mặc định

            // Load danh sách Size
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int s in sizes)
            {
                cmbSize.Items.Add(s);
            }
            cmbSize.SelectedItem = 14; // Size mặc định

            // Thiết lập Font mặc định cho RichTextBox
            richTextBox1.Font = new Font("Tahoma", 14);
        }

        // 2. Chức năng Tạo văn bản mới
        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            currentFilePath = ""; // Reset đường dẫn file

            // Reset Font về mặc định
            richTextBox1.Font = new Font("Tahoma", 14);
            cmbFont.SelectedItem = "Tahoma";
            cmbSize.SelectedItem = 14;
        }

        // 3. Chức năng Mở tập tin
        private void mởTệpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf|Text Files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                try
                {
                    if (Path.GetExtension(currentFilePath).ToLower() == ".txt")
                    {
                        // Load file txt thuần
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        // Load file rtf có định dạng
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                    }
                    MessageBox.Show("Mở tập tin thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 4. Chức năng Lưu tập tin
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Nếu chưa có đường dẫn (file mới), mở hộp thoại Save As
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Rich Text Format (*.rtf)|*.rtf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
                }
            }
            else
            {
                // Nếu đã có đường dẫn, lưu đè lên file cũ
                richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("Lưu văn bản thành công!", "Thông báo");
            }
        }

        // 5. Chức năng Thoát
        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;

            // Nếu đang chọn văn bản, lấy font hiện tại đưa vào dialog
            if (richTextBox1.SelectionFont != null)
            {
                fontDlg.Font = richTextBox1.SelectionFont;
            }

            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.SelectionFont = fontDlg.Font;
                richTextBox1.SelectionColor = fontDlg.Color;
            }
        }


        // Hàm hỗ trợ đổi kiểu font
        private void ChangeFontStyle(FontStyle style)
        {
            // Kiểm tra xem người dùng có chọn văn bản không hoặc vị trí con trỏ
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newStyle;

                // Nếu kiểu style đã có thì bỏ đi (toggle), chưa có thì thêm vào
                if (currentFont.Style.HasFlag(style))
                {
                    newStyle = currentFont.Style & ~style; // Bỏ style
                }
                else
                {
                    newStyle = currentFont.Style | style; // Thêm style
                }

                richTextBox1.SelectionFont = new Font(currentFont, newStyle);
            }
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Bold);
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Italic);
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ChangeFontStyle(FontStyle.Underline);
        }

        // 8. Xử lý thay đổi Font từ ComboBox trên ToolStrip
        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbFont.SelectedItem != null)
            {
                string fontName = cmbFont.SelectedItem.ToString();
                float fontSize = richTextBox1.SelectionFont.Size;
                FontStyle fontStyle = richTextBox1.SelectionFont.Style;

                richTextBox1.SelectionFont = new Font(fontName, fontSize, fontStyle);
            }
        }

        // 9. Xử lý thay đổi Size từ ComboBox trên ToolStrip
        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                string fontName = richTextBox1.SelectionFont.FontFamily.Name;
                float newSize = float.Parse(cmbSize.SelectedItem.ToString());
                FontStyle fontStyle = richTextBox1.SelectionFont.Style;

                richTextBox1.SelectionFont = new Font(fontName, newSize, fontStyle);
            }
        }
    }
}