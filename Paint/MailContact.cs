using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class MailContact : Form
    {
        public MailContact()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> toAdresses = new List<string>();
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("celebrimbor@outlook.com", "cptprice2");
            SmtpServer.EnableSsl = true;

           // MessageBox.Show(""+to.Text[0]);
            int a = 0;
            string todo = to.Text;
            bool c = false;
            for (int n = 0; n < todo.Length; n++)
            {
                if (todo[n] == ',')
                {
                    todo = to.Text.Substring(a, n);
                    toAdresses.Add(todo);
                    n = a;
                    c = true;
                }
                else if ((n == todo.Length - 1) && !c)
                {
                    toAdresses.Add(todo);
                }
            }
            string[] addreses = toAdresses.ToArray();
            string b = "";
            foreach (string adress in addreses)
                b += adress;
                MessageBox.Show(b);
            /* try
             {
                 mail.From = new MailAddress("celebrimbor@outlook.com");
                 for (int n = 0; n < toAdresses.Count; n++)
                     mail.To.Add(toAdresses[n]);

                 string subject = Subject.Text;
                 string body = Body.Text;


                 SmtpServer.SendAsync(mail.From.Address, "yuso96xp2@gmail.com", subject, body, null);

                 MessageBox.Show("Mail Gönderildi.");
             }
             catch {
                 MessageBox.Show("Mail Gönderilemedi.");

             }*/
        }
    }
}
