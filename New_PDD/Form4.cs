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
using System.Net;

namespace New_PDD
{
    public partial class Form4 : Form
    {
        private string Path;
        public Form4(string Path)
        {
            InitializeComponent();
            this.Path = Path;                        // путь до папки с билетами
        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            Download();
        }

        private void Download()                       // генерим пути, и сохраняем файлы на хдд
        {
            Directory.CreateDirectory(Path);          // создаем директорию
            progressBar1.Maximum = 40; progressBar1.Value = 0;          // прогрессбар на 40 делений, начинаем с нуля
            for (int bilet = 1; bilet < 41; bilet++)                    // 40 билетов
            {
                progressBar1.Value++;                                   // прогрессбар +1
                string NBilet = (bilet < 10) ? '0' + Convert.ToString(bilet) : Convert.ToString(bilet);  //генерим путь к папке с каждым билетом
                Directory.CreateDirectory(Path + '\\' + NBilet);                      // создаем папку
                for (int vopros = 1; vopros < 21; vopros++)                           // 20 вопросов
                {
                    string NVopros = (vopros < 10) ? '0' + Convert.ToString(vopros) : Convert.ToString(vopros);  //генерим имя файла
                    while (File.Exists(Path + '\\' + NBilet + '\\' + NVopros + ".txt") == false)         // если фаил не существует догружаем
                        SaveHTML("http://www.gibdd.ru/mens/avtovladeltsam/exm/ab/"
                            + NBilet + '/' + NVopros, Path + '\\' + NBilet + '\\' + NVopros);
                }
            }
            this.Close();                 // закрываем форму после загрузки всех билетов
        }

        public static void SaveHTML(string Url, string Path)        // тут происходит сохранение файлов на хдд
        {
            WebClient WClient = new WebClient();                   // создаем 2 вебклиента
            WebClient WClient2 = new WebClient();                  
            try
            {
                Uri Uri = new Uri(Url + ".jpg");                   // создаем индентификатор ресурса для картинки
                WClient.DownloadFileAsync(Uri, Path + ".jpg");             // грузим картинку
                File.WriteAllText(Path + ".txt", WClient2.DownloadString(Url + ".htm"));    //  грузим хтмл
            }
            catch (Exception)                                                               //  если неудача
            {
                File.WriteAllText(Path + ".txt", WClient2.DownloadString(Url + ".htm"));    
            }
        }
    }
}
