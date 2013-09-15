using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace New_PDD
{
    public partial class Form3 : Form
    {
        private int number, sh = 1, AnsRes, AnsSh; // глобальные переменные
        private string Path, Picture, Answer;      
        
        public Form3(int n)
        {
            InitializeComponent();
            number = n;
            AnsSh = 0;
            LoadResurs();
            LoadElement();
            this.Text = this.Text + " " + number;
        }

        private void LoadResurs()                      // тут генерим пути до файлов в зависимости от режима работы и номера билета
        {
            string a = (sh < 10) ? '0' + Convert.ToString(sh) : Convert.ToString(sh);                  // номер вопроса 
            string b = (number < 10) ? '0' + Convert.ToString(number) : Convert.ToString(number);      // номер билета
            if (File.ReadAllText("conf.fl") == "Онлайн")
            {                                                                                          //если работаем онлайн
                Path = "http://www.gibdd.ru/mens/avtovladeltsam/exm/ab/" + b + '/' + a + ".htm";
                Picture = "http://www.gibdd.ru/mens/avtovladeltsam/exm/ab/" + b + '/' + a + ".jpg";
            }
            else
            {                                                                                           // если работаем с хдд
                Path = File.ReadAllText("conf.fl") + '/' + b + '/' + a + ".txt";
                Picture = File.ReadAllText("conf.fl") + '/' + b + '/' + a + ".jpg";
            }
            Answer = "//a[@href ='" + a + "1.htm']";                                                    // ссылка на верный ответ
        }
        private void LoadElement()                    //тут настраиваем кнопки -- надписи -- рисунки
        {
            Object[] ArrObj = new Object[4] { radioButton1, radioButton2, radioButton3, radioButton4 };
            button1.Enabled = false;                                                        // настраиваем кнопки перед стартом
            for (int i = 0; i < 4; i++) 
            {
                ((RadioButton)ArrObj[i]).Visible = false;                                   // делаем невидимыми
                ((RadioButton)ArrObj[i]).Checked = false;                                   // снимаем клики
            }
            pictureBox1.ImageLocation = Picture;                                            // добавляем картинку в пикчбокс
            string Text;                                                                    // тут храниться текст билета
            if (File.ReadAllText("conf.fl") != "Онлайн") Text = File.ReadAllText(Path);      // работаем с хдд
            else                                                                            // работаем онлайн
            {                                                                   
                WebClient WClient = new WebClient();
                Text = WClient.DownloadString(Path); 
            }
            label1.Text = HTMLParserForText(Text.Remove(200, 40), "//center//h2", "text");  // текст вопроса
            string [] ArrTrash = HTMLParserForText(Text, "//ol", "html").Split('i');             // массив с ответами в виде кода +мусор
            ArrayList ArrListAns = new ArrayList();                                         // динамический массив для ответов
            for (int i = 0; i < ArrTrash.Length; i++)
                if (HTMLParserForText(ArrTrash[i], "//a", "text") != "")                         // если мусор -- ответ пустой
                    ArrListAns.Add(HTMLParserForText(ArrTrash[i], "//a", "text"));               // если пусто заполняем коллекцию
            for (int i = 0; i < ArrListAns.Count; i++)   
            {                                                                               // заполняем кнопки и делаем их видимыми
                ((RadioButton)ArrObj[i]).Visible = true;
                ((RadioButton)ArrObj[i]).Text = Convert.ToString(ArrListAns[i]);
                if (((RadioButton)ArrObj[i]).Text == HTMLParserForText(Text, Answer, "text")) // получаем номер кнопки с верным ответом
                    AnsRes = i;
            }   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((AnsRes == 0) & (radioButton1.Checked == true)) AnsSh++;                   // счетчик верных ответов
            else if ((AnsRes == 1) & (radioButton2.Checked == true)) AnsSh++;
            else if ((AnsRes == 2) & (radioButton3.Checked == true)) AnsSh++;
            else if ((AnsRes == 3) & (radioButton4.Checked == true)) AnsSh++;
            if (sh == 20)
            {
                string Res = (AnsSh > 17)? "cдан." : "не сдан.";                           // сдал -- не сдал
                MessageBox.Show("Билет " + Res + "\n Верно -- " + AnsSh);
                Form1 Form1 = new Form1();                                                 // закрываем форму
                Form1.Show();
                this.Close();
            }
            else
            {
                sh++;                                                                       // счетчик вопросов
                LoadResurs();
                LoadElement();
            }
        }
        public static string HTMLParserForText(string file, string b, string h)
        {
            HtmlAgilityPack.HtmlDocument HDoc = new HtmlAgilityPack.HtmlDocument();       // создаем новый обьект
            HDoc.LoadHtml(file);                                                          // подгружаем в него текст
            HtmlNode HNode = HDoc.DocumentNode.SelectSingleNode(b);                       // все текстовое в теге b
            string ResText = "";
            try
            {
                switch (h)                                                                // код или текст?
                {
                    case "text": ResText = HNode.InnerText; break;                        // возвращаем текст
                    case "html": ResText = HNode.InnerHtml; break;                        // возвращаем код
                }
            }
            catch (Exception)
            {
                ResText = "";                                                             // если ошибка -- возвращаем пустоту
            }
            return ResText;
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e) { button1.Enabled = true; }    //событие по клику на кнопке
        private void radioButton2_MouseClick(object sender, MouseEventArgs e) { button1.Enabled = true; }
        private void radioButton3_MouseClick(object sender, MouseEventArgs e) { button1.Enabled = true; }
        private void radioButton4_MouseClick(object sender, MouseEventArgs e) { button1.Enabled = true; }
    }
}
