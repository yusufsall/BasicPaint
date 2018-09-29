using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        //Gerekli tanımlamaları yaptık.
        Picture pic;
        Color color;
        int rad = 20;
        public Form1()
        {
            InitializeComponent();
            //Sürükle bırak özelliğimizin etkin olabilmesi için gerekli.
            arkaplan.AllowDrop = true;
        }
        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            //Sürükle bıraktaki elemanların disk konumları string array şeklinde elimize geçer. 
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            //Yazdığımız hazırla fonksiyonuna ilk dosyayı göndeririz.
            hazirla(files[0]);
        }
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            //Mousenin tepkisini ayarladık kullanıcıyı yanıltmaması için.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }        
        private void hazirla(string file)
        {
            try
            { 
                //Bu fonksiyon gönderilen dosya konumunu Image objesine çeviri ve ekranı ona göre hazırlar.
                Image img = Image.FromFile(file);

                //Picture box hazırlanır.
                arkaplan.Width = img.Width;
                arkaplan.Height = img.Height;
                
                arkaplan.Image = img;

                //Oluşturduğumuz picture sınıfından obje üretilir.
                pic = new Picture(arkaplan, Cursor, this);

                //Ekran resme göre ayarlanır.
                const int x_padding = 42;
                const int x_toolbox = 280;
                const int y_padding = 80;

                this.Width = img.Width + x_padding + x_toolbox;
                this.Height = img.Height + y_padding;
            }
            catch
            {
                //Bir hata durumunda...
                MessageBox.Show(this, "Belirlediğin Dosya uygun değil...", "Hata");
            }
        }
        private void hakkinda(object sender, EventArgs e)
        {
            //Kişi hakkında sekmesine tıkladığında.
            MessageBox.Show(this, "Yusuf ŞAL Tarafından Yapıldı...", "Hakkında");
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mouse windows formda oluşturduğumuz pictureboxın üzerindeyken sol tuşa basılırsa...
            if (e.Button == MouseButtons.Left)
            {
                //Eğer araçlardan kalem seçildiyse pic objemizin boya metoduyla resmin o noktası boyanır.
                if (kalem.Checked)
                    try
                    {
                        pic.boya(pic.x, pic.y, color.R, color.G, color.B);
                    }
                    catch
                    {
                        if (arkaplan.Image == null)
                            MessageBox.Show(this, "Dosya Yükleyin...", "Hata!");
                    }
                //Kalem içinde aynısı geçerlidir.
                if (palet.Checked)
                    try
                    {
                        pic.fircadarbesi(pic.x, pic.y, rad, color.R, color.G, color.B);
                    }
                    catch
                    {
                        if (arkaplan.Image == null)
                            MessageBox.Show(this, "Dosya Yükleyin...", "Hata!");
                    }
                
            }
        }
        private void boyaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Burdan renk seçilir. 
            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
                color = colorDialog1.Color;

            pic.paletHazirla(rad, color.R, color.G, color.B);

        }
        private void Kaydet(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            
            DialogResult result = sfd.ShowDialog();

            if (result == DialogResult.OK)
            {
                FileStream save_image = new FileStream(sfd.FileName, FileMode.Create);

                byte[] image_byte = ImageToByte(arkaplan.Image);

                save_image.Write(image_byte,0,image_byte.Length);
            }
        }
        private static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private void yeni(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            int width = form2.width;
            int height = form2.height;
            if (width != 0 && height != 0) {
                Bitmap bitmap = new Bitmap(width, height);

                for (int w = 0; w < width; w++)
                    for (int h = 0; h < height; h++)
                        bitmap.SetPixel(w, h, Color.White);


                arkaplan.Width = bitmap.Width;
                arkaplan.Height = bitmap.Height;

                arkaplan.Image = bitmap;

                pic = new Picture(arkaplan, Cursor, this);
                this.Width = bitmap.Width + 42;
                this.Height = bitmap.Height + 80;
            }
        }
        private void open(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            DialogResult result = ofd.ShowDialog();

            if (result == DialogResult.OK)
            {
                hazirla(ofd.FileName);
            }
        }
        private void exit(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void kalem_click(object sender, EventArgs e)
        {
            if (kalem.Checked)
                kalem.Checked = false;
            else
                kalem.Checked = true;
        }

        private async void contact(object sender, EventArgs e)
        {
            MailContact mail = new MailContact();
            mail.Show();
        }

        private void palet_Click(object sender, EventArgs e)
        {
            if (palet.Checked)
                palet.Checked = false;
            else
            {
                palet.Checked = true;

                rad = paletrad.Value;

                pic.paletHazirla(rad, color.R, color.G, color.B);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (kalem.Checked)
                try
                {
                    pic.boya(pic.x, pic.y, color.R, color.G, color.B);
                }
                catch
                {
                    if (arkaplan.Image == null)
                        MessageBox.Show(this, "Dosya Yükleyin...", "Hata!");
                }
            if (palet.Checked)
                try
                {
                    pic.fircadarbesi(pic.x, pic.y, rad, color.R, color.G, color.B);
                }
                catch
                {
                    if (arkaplan.Image == null)
                        MessageBox.Show(this, "Dosya Yükleyin...", "Hata!");
                }

        }

        private void paletrad_ValueChanged(object sender, EventArgs e)
        {
            rad = paletrad.Value;
            pic.paletHazirla(rad, color.R, color.G, color.B);
        }
    }
}
