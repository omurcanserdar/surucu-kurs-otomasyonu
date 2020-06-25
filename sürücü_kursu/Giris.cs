using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using WinWebDll;

namespace sürücü_kursu
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
    
        SqlConnection con;
        
        private void Giris_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFileName=|DataDirectory|\srckurs.mdf;Integrated Security=True;Connection TimeOut=30";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinizden eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            { Application.Exit(); }
        }

        private void btnyenikyt_Click(object sender, EventArgs e)
        {
            yenikayit yk = new yenikayit();
            this.Hide();
            yk.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anamenü ae = new Anamenü();
            ae.ShowDialog();
            this.Hide();
        }
        public bool Girisİslemi(string Adı, string Parola)
        {

            try
            {
                con.Open();
                int sayac = 0;
                SqlCommand cmd = new SqlCommand("SELECT Adı,Parola FROM uye WHERE Adı ='" +Adı+ "' AND Parola ='" +Parola+ "'", con);
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    sayac++;
                }

                if (sayac > 0)
                {
                    return true;
                    
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                con.Close();
                return false;
            }
            finally 
            { 
                con.Close(); 
            }
        }

        private void btngiris_Click(object sender, EventArgs e)
        {

            bool sonuc = Girisİslemi(Convert.ToString(textBox1.Text), Convert.ToString(textBox2.Text));
            if (sonuc == true)
            {
                MessageBox.Show(textBox1.Text + " " +  "Kullanıcı adıyla oturum açtınız.."+"Giriş Başarılı,\n Anamenüye Gidiliyor...", "Giriş İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Anamenü amenu = new Anamenü();
                this.Hide();
                amenu.ShowDialog();
            }

            else if (textBox1.Text == "admin" && textBox2.Text == "123")
            {
                MessageBox.Show(textBox1.Text + " " + "Kullanıcı adıyla oturum açtınız.." + "Giriş Başarılı,\n Anamenüye Gidiliyor...", "Giriş İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Anamenü amenu = new Anamenü();
                this.Hide();
                amenu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız,\n Lütfen Kullanıcı Adınızı veya Şifrenizi Kontrol Ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        }
    }