using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Buoi2_3
{
    public partial class Form1 : Form
    {
        // Khai báo Delegate dùng để tính toán và cập nhật tiền
        public delegate void UpdateTotalHandler();

        // Biến delegate
        private UpdateTotalHandler _updateTotalDelegate;

        // Danh sách lưu trữ các Button ghế để dễ quản lý
        private List<Button> seatButtons = new List<Button>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo các ghế ngồi
            InitializeSeats();

            // Gán phương thức CalculateTotal cho delegate
            _updateTotalDelegate = new UpdateTotalHandler(CalculateTotal);
        }

        /// <summary>
        /// Hàm tạo động 20 ghế ngồi (4 hàng x 5 cột)
        /// </summary>
        private void InitializeSeats()
        {
            int rows = 4;
            int cols = 5;
            int seatWidth = 60;
            int seatHeight = 40;
            int gap = 10;
            int startX = 25;
            int startY = 20;

            int seatNumber = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Button btn = new Button();
                    btn.Text = seatNumber.ToString();
                    btn.Name = "btnSeat" + seatNumber;
                    btn.Size = new Size(seatWidth, seatHeight);
                    btn.Location = new Point(startX + j * (seatWidth + gap), startY + i * (seatHeight + gap));
                    btn.BackColor = Color.White;
                    btn.Font = new Font(btn.Font, FontStyle.Bold);

                    // Gắn sự kiện Click chung cho tất cả các ghế
                    btn.Click += new EventHandler(this.BtnSeat_Click);

                    // Thêm vào GroupBox và List quản lý
                    grpSeats.Controls.Add(btn);
                    seatButtons.Add(btn);

                    seatNumber++;
                }
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi click vào một ghế
        /// </summary>
        private void BtnSeat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackColor == Color.Yellow)
            {
                MessageBox.Show("Ghế này đã được bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Toggle màu sắc: Trắng <-> Xanh
            if (btn.BackColor == Color.White)
            {
                btn.BackColor = Color.Blue;
            }
            else if (btn.BackColor == Color.Blue)
            {
                btn.BackColor = Color.White;
            }

            // Gọi Delegate để tính lại tiền (nếu muốn tính real-time)
            // Nếu bài tập yêu cầu chỉ tính khi nhấn nút "Chọn", có thể bỏ dòng này.
            // Tuy nhiên, việc dùng delegate ở đây minh họa kỹ thuật tốt.
            _updateTotalDelegate?.Invoke();
        }

        /// <summary>
        /// Hàm tính tổng tiền dựa trên các ghế đang chọn (Màu Xanh)
        /// </summary>
        private void CalculateTotal()
        {
            double total = 0;

            foreach (Button btn in seatButtons)
            {
                if (btn.BackColor == Color.Blue) // Chỉ tính ghế đang chọn
                {
                    int seatNum = int.Parse(btn.Text);
                    total += GetSeatPrice(seatNum);
                }
            }

            txtTotal.Text = total.ToString("N0") + " VNĐ";
        }

        /// <summary>
        /// Lấy giá vé dựa trên số ghế (hàng)
        /// </summary>
        private double GetSeatPrice(int seatNum)
        {
            if (seatNum <= 5) return 30000;       // Hàng 1
            if (seatNum <= 10) return 40000;      // Hàng 2
            if (seatNum <= 15) return 50000;      // Hàng 3
            return 80000;                         // Hàng 4
        }

        /// <summary>
        /// Xử lý nút CHỌN (Mua vé)
        /// </summary>
        private void btnChon_Click(object sender, EventArgs e)
        {
            double finalTotal = 0;
            bool hasSelection = false;

            foreach (Button btn in seatButtons)
            {
                if (btn.BackColor == Color.Blue)
                {
                    // Chuyển sang màu vàng (đã bán)
                    btn.BackColor = Color.Yellow;

                    int seatNum = int.Parse(btn.Text);
                    finalTotal += GetSeatPrice(seatNum);
                    hasSelection = true;
                }
            }

            if (hasSelection)
            {
                MessageBox.Show("Đặt vé thành công!\nTổng tiền: " + finalTotal.ToString("N0") + " VNĐ", "Thông báo");
                // Reset ô thành tiền về 0 sau khi thanh toán xong
                txtTotal.Text = "0 VNĐ";
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ghế trước khi thanh toán.", "Thông báo");
            }
        }

        /// <summary>
        /// Xử lý nút HỦY BỎ
        /// </summary>
        private void btnHuy_Click(object sender, EventArgs e)
        {
            foreach (Button btn in seatButtons)
            {
                // Chỉ hủy các ghế đang chọn (Xanh), không hủy ghế đã bán (Vàng)
                if (btn.BackColor == Color.Blue)
                {
                    btn.BackColor = Color.White;
                }
            }

            // Cập nhật lại thành tiền về 0 thông qua Delegate
            _updateTotalDelegate?.Invoke();
        }

        /// <summary>
        /// Xử lý nút KẾT THÚC
        /// </summary>
        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}