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

namespace sürücü_kursu
{
    public partial class yenikayit : Form
    {
        public yenikayit()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        private void yenikayit_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFileName=|DataDirectory|\srckurs.mdf;Integrated Security=True;Connection TimeOut=30";
            if (con.State != ConnectionState.Open)
                con.Open();

        }
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            String cins_yeni = "";
            if (radioButton1.Checked == true)
            { cins_yeni = "KADIN"; }
            else if (radioButton2.Checked == true)
            { cins_yeni = "ERKEK"; }
            else
            {
                MessageBox.Show("Lütfen Cinsiyetinizi seçiniz.");
            }
            if (con.State != ConnectionState.Open)
                con.Open();

            cmd = new SqlCommand("INSERT INTO uye (TC,Adı,Soyadı,Cinsiyeti,DoğumYeri,DoğumTarihi,CepTel,Eposta,Parola) VALUES (@TC,@Adı,@Soyadı,@Cinsiyeti,@DoğumYeri,@DoğumTarihi,@CepTel,@Eposta,@Parola)", con);
          cmd.Parameters.AddWithValue("@TC", txtTC.Text);
            cmd.Parameters.AddWithValue("@Adı", txtAd.Text);
            cmd.Parameters.AddWithValue("@Soyadı", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@Cinsiyeti", cins_yeni.ToString());
            cmd.Parameters.AddWithValue("@DoğumYeri", txtDogumYeri.Text);
            cmd.Parameters.AddWithValue("@DoğumTarihi", dateTimePicker1.Value.ToString("MM.dd.yyyy HH:mm:ss"));
            cmd.Parameters.AddWithValue("@CepTel", txtCepTel.Text);
            cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text);
            cmd.Parameters.AddWithValue("@Parola", txtParola.Text);
            if (cmd.ExecuteNonQuery()==1)
            {
                MessageBox.Show("Kayıt Gerçekleştirildi...");
                Giris g = new Giris();
                this.Hide();
                g.ShowDialog();
            }
            else
                MessageBox.Show("Kayıt Gerçekleştirilemedi...");

            if (con.State != ConnectionState.Closed)
                con.Close();

         
            //     cmd = new SqlCommand("INSERT INTO UYE(TC,Adı,Soyadı,Cinsiyeti,DoğumYeri,DoğumTarihi,CepTel,Eposta,Parola) VALUES (@TC,@Adı,@Soyadı,@Cinsiyeti,@DoğumYeri,@DoğumTarihi,@CepTel,@Eposta,@Parola)", con);
            //     cmd.Parameters.AddWithValue("@TC", maskedtxtTC.Text);
            //     cmd.Parameters.AddWithValue("@Adı", txtAd.Text);
            //     cmd.Parameters.AddWithValue("@Soyadı", txtSoyad.Text);
            //     cmd.Parameters.AddWithValue("@Cinsiyeti",cins_yeni.ToString());
            //     cmd.Parameters.AddWithValue("@DoğumYeri", txtDogumYeri.Text);
            //     cmd.Parameters.AddWithValue("@DoğumTarihi", maskedtxtDogumTarihi.Text);
            //     cmd.Parameters.AddWithValue("@CepTel", maskedtxtCepTel.Text);
            //     cmd.Parameters.AddWithValue("@Eposta", txtEposta.Text);
            //     cmd.Parameters.AddWithValue("@Parola", txtParola.Text);
        }
        private void btntemizle_Click(object sender, EventArgs e)
        {
            foreach (Control txt in this.Controls)
            {
                if (txt is TextBox || txt is MaskedTextBox)
                {
                    txt.Text = "";
                }
                label7.Text = "Bütün TextBoxlar Temizlendi...";
            }
        }
    }
}
        
   