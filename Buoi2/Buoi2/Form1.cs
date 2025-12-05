using System;
using System.Windows.Forms;

namespace Buoi2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Func<float, float, float> phepCong = (a, b) => a + b;
        Func<float, float, float> phepTru = (a, b) => a - b;
        Func<float, float, float> phepNhan = (a, b) => a * b;
        Func<float, float, float> phepChia = (a, b) => b == 0 ? 0 : a / b; 

        private float autoMath(Func<float, float, float> caculator)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_Number1.Text) 
                    || string.IsNullOrEmpty(txt_Number2.Text))
                {
                    MessageBox.Show("Vui lòng nhập đủ số!");
                    return 0;
                }

                return caculator(float.Parse(txt_Number1.Text), float.Parse(txt_Number2.Text)); ;
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số!");
                txt_Number1.Clear();
                txt_Number2.Clear();
                txt_Number1.Focus();
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return 0;
            }
        }
        private void btn_Cong_Click(object sender, EventArgs e)
        {
            txt_Answer.Text = autoMath(phepCong).ToString();
        }

        private void bnt_Tru_Click(object sender, EventArgs e)
        {
            txt_Answer.Text = autoMath(phepTru).ToString();
        }

        private void btn_Nhan_Click(object sender, EventArgs e)
        {
            txt_Answer.Text = autoMath(phepNhan).ToString();
        }

        private void btn_Chia_Click(object sender, EventArgs e)
        {
            txt_Answer.Text = autoMath(phepChia).ToString();
        }
    }
}
