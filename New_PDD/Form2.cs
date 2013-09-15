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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            if (File.ReadAllText("conf.fl") == "Онлайн")               // заполняем поля на форме
                comboBox1.Text = "Онлайн";
            else
            {
                comboBox1.Text = "Подгрузить вопросы на HDD";
                textBox1.Text = File.ReadAllText("conf.fl");
            }
        }

        private void button1_Click(object sender, EventArgs e)         // закрываем форму без сохранения изменений
        {
            Form1 Form1 = new Form1();
            this.Close();
            Form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)         // закрываем с сохранением изменений
        {
            if (comboBox1.Text == "Подгрузить вопросы на HDD")         // если грузим на хдд
            {
                File.WriteAllText("conf.fl", textBox1.Text);           // переписываем conf.fl
                if (Directory.Exists(textBox1.Text + '\\' + "40")) MessageBox.Show("Загрузка билетов не требуеться"); 
                else                                // если билеты есть -- не грузим.
                {
                    Form4 Form4 = new Form4(textBox1.Text);     // если нету -- передаем путь до папки с билетами на форму 4
                    Form4.ShowDialog();
                    button1_Click(null, null);                  // закрываем форму
                }
            }
            else   // если работаем онлайн
            {     // проверяем есть ли каталог с билетами, если есть -- удаляем рекурсивно
                if (Directory.Exists(File.ReadAllText("conf.fl"))) Directory.Delete(File.ReadAllText("conf.fl"), true);
                File.WriteAllText("conf.fl", "Онлайн");    //  переписываем conf.fl
                button1_Click(null, null);                 // закрываем форму
            }
        }

        private void button3_Click(object sender, EventArgs e)              // работа с диалогом
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)      
                textBox1.Text = folderBrowserDialog1.SelectedPath;         
        }
    }
}
