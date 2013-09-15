using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace New_PDD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();                                                        
            if ((File.Exists("conf.fl") == false) || (File.ReadAllText("conf.fl") == ""))   // если conf.fl отсутствует или пустой
                File.WriteAllText("conf.fl", "Онлайн");                                     // перезаписываем для работы онлайн
        }
 
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)            // в настройки
        {
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)               // закрыть приложение
        {
            Application.Exit();
        }

        private void TestOpen(int Number)                                                   // на форму с тестами, передаем номер вопроса
        {
            string NBilet =(Number < 10) ? 0 + Convert.ToString(Number) : Convert.ToString(Number);  // генерим номер билета
            if (File.ReadAllText("conf.fl") != "Онлайн")                                       // если работаем не онлайн
                if (Directory.Exists(File.ReadAllText("conf.fl") + '\\' + NBilet))            // проверяем сушествует ли директория с билетом
                {                                                       
                    Form3 Form3 = new Form3(Number);                                          // если да
                    Form3.Show();
                    this.Hide();
                }
                else MessageBox.Show("Билет не найден. \nПопробуйте заново загрузить билеты.");      // если нет -- выводим сообщение 
            else                                                                                     // если работаем онлайн
            {
                Form3 Form3 = new Form3(Number);
                Form3.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e) { TestOpen(1); }                      // кнопки
        private void button2_Click(object sender, EventArgs e) { TestOpen(2); }
        private void button3_Click(object sender, EventArgs e) { TestOpen(3); }
        private void button4_Click(object sender, EventArgs e) { TestOpen(4); }
        private void button5_Click(object sender, EventArgs e) { TestOpen(5); }
        private void button6_Click(object sender, EventArgs e) { TestOpen(6); }  
        private void button7_Click(object sender, EventArgs e) { TestOpen(7); }
        private void button8_Click(object sender, EventArgs e) { TestOpen(8); }
        private void button9_Click(object sender, EventArgs e) { TestOpen(9); }
        private void button10_Click(object sender, EventArgs e) { TestOpen(10); }
        private void button11_Click(object sender, EventArgs e) { TestOpen(11); }
        private void button12_Click(object sender, EventArgs e) { TestOpen(12); }
        private void button13_Click(object sender, EventArgs e) { TestOpen(13); }
        private void button14_Click(object sender, EventArgs e) { TestOpen(14); }
        private void button15_Click(object sender, EventArgs e) { TestOpen(15); }
        private void button16_Click(object sender, EventArgs e) { TestOpen(16); }
        private void button17_Click(object sender, EventArgs e) { TestOpen(17); }
        private void button18_Click(object sender, EventArgs e) { TestOpen(18); }
        private void button19_Click(object sender, EventArgs e) { TestOpen(19); }
        private void button20_Click(object sender, EventArgs e) { TestOpen(20); }
        private void button21_Click(object sender, EventArgs e) { TestOpen(21); }
        private void button22_Click(object sender, EventArgs e) { TestOpen(22); }
        private void button23_Click(object sender, EventArgs e) { TestOpen(23); }
        private void button24_Click(object sender, EventArgs e) { TestOpen(24); }
        private void button25_Click(object sender, EventArgs e) { TestOpen(25); }
        private void button26_Click(object sender, EventArgs e) { TestOpen(26); }
        private void button27_Click(object sender, EventArgs e) { TestOpen(27); }
        private void button28_Click(object sender, EventArgs e) { TestOpen(28); }
        private void button29_Click(object sender, EventArgs e) { TestOpen(29); }
        private void button30_Click(object sender, EventArgs e) { TestOpen(30); }
        private void button31_Click(object sender, EventArgs e) { TestOpen(31); }
        private void button32_Click(object sender, EventArgs e) { TestOpen(32); }
        private void button33_Click(object sender, EventArgs e) { TestOpen(33); }
        private void button34_Click(object sender, EventArgs e) { TestOpen(34); }
        private void button35_Click(object sender, EventArgs e) { TestOpen(35); }
        private void button36_Click(object sender, EventArgs e) { TestOpen(36); }
        private void button37_Click(object sender, EventArgs e) { TestOpen(37); }
        private void button38_Click(object sender, EventArgs e) { TestOpen(38); }
        private void button39_Click(object sender, EventArgs e) { TestOpen(39); }
        private void button40_Click(object sender, EventArgs e) { TestOpen(40); }
    }
   

   
}
