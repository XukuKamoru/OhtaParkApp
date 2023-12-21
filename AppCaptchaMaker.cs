using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;
using Brushes = System.Drawing.Brushes;
using System.Windows.Media.Media3D;
using System.Linq.Expressions;
using System.Drawing.Imaging;
using System.Windows;

namespace OhtaPark
{
    internal class AppCaptchaMaker
    {
        public string CaptchaText;

        Brush[] Colors =
        {
            Brushes.Red,
            Brushes.Black,
            Brushes.Azure,
            Brushes.Green,
            Brushes.Yellow,
            Brushes.Violet,
            Brushes.Blue,
            Brushes.DarkGoldenrod
        };
        // Конвертор
        public BitmapImage GenerateImage(Bitmap Image)
        {
            // Инициализируем BitmapImage для конвертора
            BitmapImage convertingImage = new BitmapImage();
            convertingImage.BeginInit();
            // Создаем поток
            MemoryStream mem = new MemoryStream();
            // Сохраняем bmp карту
            Image.Save(mem, ImageFormat.Bmp);
            // Возврщаемся к началу
            mem.Position = 0;
            // Загружаем карту из потока
            convertingImage.StreamSource = mem;
            // Кэшируем изображение в памяти, чтобы иметь доступ из потока
            convertingImage.CacheOption = BitmapCacheOption.OnLoad;
            // Конец инициализации
            convertingImage.EndInit();
            return convertingImage;

        }

        // Создаем капчу
        public ImageSource CreateBitmap(int w, int h) // берем высоту и длину Image
        {
            CaptchaText = String.Empty;
            Random random = new Random();
            Bitmap bitmap = new Bitmap(w, h);
            // Позиция для текста
            int xPosition = random.Next(0, w - 50);
            int yPosition = random.Next(0, h - 50);
            // Полотно для рисования, инициализируем цвет фона
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            Color Background = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            graphics.Clear(Background);
            // генерация текста капчи
            string AlpAndNum = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            for (int i = 0; i < 8; i++)
            {
                CaptchaText += AlpAndNum[random.Next(AlpAndNum.Length)];
            }
            // Рисование текста
            graphics.DrawString(CaptchaText, new Font("Arial", 90), Colors[random.Next(Colors.Length)], new PointF(xPosition, yPosition));
            // Рисуем шумы 
            Color colors = new Color();
            colors = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            for (int i = 0; i < w; ++i)
                for (int j = 0; j < h; ++j)
                    if (random.Next() % 20 == 0)
                        bitmap.SetPixel(i, j, colors);
            // Шумы (Линии)
            graphics.DrawLine(Pens.Red, new System.Drawing.Point(random.Next(255), 0), new System.Drawing.Point(w - 5, h - 5));
            graphics.DrawLine(Pens.Black, new System.Drawing.Point(random.Next(255), h - 5), new System.Drawing.Point(w - 5, 0));

            return GenerateImage(bitmap);
        }

        // Проверка введенного пользователем текста
        public bool Check(string inputBoxText)
        {
            if (inputBoxText == CaptchaText)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
