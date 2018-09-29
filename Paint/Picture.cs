using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class Picture
    {
        //Mousenin bilgisini almak için cursor objesi.
        Cursor cursor;
        //Anaformdaki resimi yakalamak için picturebox.
        PictureBox pictureBox;
        //Pictureboxdaki resim.
        Bitmap bitmap;
        //Yuvarlağın kaydedilceği resim.
        Bitmap palette;
        //Anaform
        Form form;
        public int x
        {
            get {
                //Kenardaki fazladan dolgu için.
                int x_padding = 7;

                //x değeri burda form + resmin en sol köşesi
                int x = pictureBox.Left + form.Left;

                //Bu x değeri dolguyla toplanıp mousenin x değerinden çıkarıldığında mousenin 
                //Forma göre x değeri bulunur.
                return (Cursor.Position.X - (x + x_padding));
            }
        }
        public int y
        {
            get
            {
                //X'deki mantığın aynısıdır.
                int y_padding = 30;

                int y = pictureBox.Top + form.Top;

                return (Cursor.Position.Y - (y + y_padding));
            }
        }
        public Picture(PictureBox pic,Cursor cursor,Form form)
        {
            //Klasik constructor işlemi.
            this.cursor = cursor;

            pictureBox = pic;

            this.form = form;
                    
        }
        public void paletHazirla(int radius, int r, int g, int b) {
            if (radius == 0)
                radius = 20;
            //Paletdeki yuvarlak önceden çizilir.
            //Bu sayede Gerçek zamanlı çizilmez ve performans artışı olur.
            palette = new Bitmap(2 * radius, 2 * radius);
            try
            {
                for (int angle = 0; angle < 360; angle++)
                    for (int ro = 0; ro < radius; ro++)
                    {
                        //yarıçaptan 0a kadar iç içe çemberler oluşturularak daire elde edilir.
                        palette.SetPixel((int)(radius + ro * Math.Cos(angle)), (int)(radius + ro * Math.Sin(angle)), Color.FromArgb(r, g, b));
                    }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void boya(int x, int y, int r, int g, int b)
        {
            try
            {
                //Verilen x,y ve renk değerine göre boyama yapar.
                bitmap = (Bitmap) pictureBox.Image;

                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));

                pictureBox.Image = bitmap;
            }
            catch
            {
            }
        }
        public void fircadarbesi(int x, int y, int radius, int r, int g, int b)
        {
            int cap = 2 * radius;
            try
            {
                //Önceden oluşturulan fırçayı resme kopyalama işlemi.
                bitmap = (Bitmap)pictureBox.Image;

                copyRegionIntoImage(palette, new Rectangle(0, 0, cap, cap), bitmap, new Rectangle(x - radius, y - radius, cap, cap));

                pictureBox.Image = bitmap;
            }
            catch
            {
            }

        }
        private void copyRegionIntoImage(Bitmap source,Rectangle sourceRegion, Bitmap destination, Rectangle destRegion)
        {
            using (Graphics grd = Graphics.FromImage(destination)) {
                grd.DrawImage(source, destRegion, sourceRegion, GraphicsUnit.Pixel);
            }
        }
    }
}
