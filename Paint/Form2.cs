using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form2 : Form
    {
        public int width, height;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (genislik.Text != null && yukseklik.Text != null)
            {
                try
                {
                    width = Int32.Parse(genislik.Text);
                    height = Int32.Parse(yukseklik.Text);
                    this.Close();
                }
                catch
                {
                    MessageBox.Show(this, "Lütfen mümkün değerler giriniz.", "Hata");
                }
                
            }
            else {
                MessageBox.Show(this, "Boş alanları doldurun.", "Hata");
            }
        }
    }
}
