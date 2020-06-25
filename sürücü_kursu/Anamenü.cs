using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using System.IO;
using WinWebDll;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using iTextSharp.text;
namespace sürücü_kursu
{
    public partial class Anamenü : Form
    {
        public Anamenü()
        {
            InitializeComponent();
        }
        WinWebSnf wws;
        SqlDataAdapter adp;
        DataSet ds;
        SqlConnection con;
        BindingSource bs;
        string resimPath;
        private void Anamenü_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'srckursDataSet1.firma' table. You can move, or remove it, as needed.
            this.firmaTableAdapter.Fill(this.srckursDataSet1.firma);
            // TODO: This line of code loads data into the 'srckursDataSet1.eSınıf' table. You can move, or remove it, as needed.
            this.eSınıfTableAdapter.Fill(this.srckursDataSet1.eSınıf);
            // TODO: This line of code loads data into the 'srckursDataSet1.Aday' table. You can move, or remove it, as needed.
            this.adayTableAdapter.Fill(this.srckursDataSet1.Aday);
            // TODO: This line of code loads data into the 'srckursDataSet.Aday' table. You can move, or remove it, as needed.
            //  this.adayTableAdapter.Fill(this.srckursDataSet.Aday);


            cmbsınıfadı.SelectedIndexChanged -= cmbsınıfadı_SelectedIndexChanged;
            wws = new WinWebSnf();
            datasnf.DataSource = wws.SqlDataTable("SELECT * From eSınıf", null);
            DataTable tbl = new DataTable();
            tbl = wws.SqlDataTable("SELECT SınıfAdı FROM eSınıf GROUP BY SınıfAdı ORDER BY SınıfAdı", null);
            cmbsınıfadı.DataSource = tbl;
            cmbsınıfadı.DisplayMember = "SınıfAdı";
            cmbsınıfadı.ValueMember = "SınıfAdı";
            cmbsınıfadı.SelectedIndexChanged += cmbsınıfadı_SelectedIndexChanged;


            cmbesınıf.SelectedIndexChanged -= cmbesınıf_SelectedIndexChanged;
            dataehliyet.DataSource = wws.SqlDataTable("SELECT * From Ehliyet", null);
            DataTable tbla = new DataTable();
            tbla = wws.SqlDataTable("SELECT EhliyetSınıfAdı FROM Ehliyet GROUP BY EhliyetSınıfAdı ORDER BY EhliyetSınıfAdı", null);
            cmbesınıf.DataSource = tbla;
            cmbesınıf.DisplayMember = "EhliyetSınıfAdı";
            cmbesınıf.ValueMember = "EhliyetSınıfAdı";
            cmbesınıf.SelectedIndexChanged += cmbesınıf_SelectedIndexChanged;

            #region datasnf
            datasnf.DataSource = wws.SqlDataTable("SELECT * FROM eSınıf", null);
            datasnf.Columns[1].Width = 175;
            datasnf.Columns[2].Width = 175;
            #endregion



            datagridaday.DataSource = wws.SqlDataTable("SELECT * FROM Aday", null);


            DataTable dtb = new DataTable();
            dtb.Clear();
            dtb = wws.SqlDataTable("SELECT Kapasite FROM eSınıf ", null);
            label40.Text = dtb.Rows[0][0].ToString();

            DataTable dt = new DataTable();
            dt = wws.SqlDataTable("SELECT Adı,Parola,SIRANO From uye", null);
            datakullanıcı.DataSource = dt;
            datakullanıcı.Columns[0].Width = 120;
            datakullanıcı.Columns[1].Width = 120;


            dataGridView1.DataSource = wws.SqlDataTable("SELECT * FROM firma", null);

            #region dataevrak
            dataevrak.DataSource = wws.SqlDataTable("SELECT * FROM Evrak", null);
            dataevrak.Columns[1].Width = 200;
            dataevrak.Columns[0].Width = 100;
            #endregion

            dataehliyet.DataSource = wws.SqlDataTable("SELECT * FROM Ehliyet", null);
            dataehliyet.Columns[1].Width = 150;

            #region Permaas
            dataGridViewpermaas.DataSource = wws.SqlDataTable("SELECT * FROM permaas", null);
            dataGridViewpermaas.AutoGenerateColumns = false;
            dataGridViewpermaas.Columns[4].Width = 120;
            dataGridViewpermaas.Columns[5].Width = 85;
            #endregion

            #region GelirTablo
            datagelirtablo.DataSource = wws.SqlDataTable("SELECT * FROM gelirtbl", null);
            datagelirtablo.AutoGenerateColumns = false;
            datagelirtablo.Columns[2].Width = 140;
            datagelirtablo.Columns[3].Width = 140;
            datagelirtablo.Columns[4].Width = 140;
            #endregion

            #region GiderTablo
            datagidertablo.DataSource = wws.SqlDataTable("SELECT * FROM gidertbl", null);
            datagidertablo.AutoGenerateColumns = false;
            datagidertablo.Columns[2].Width = 140;
            datagidertablo.Columns[3].Width = 140;
            datagidertablo.Columns[4].Width = 140;
            #endregion


        }

        #region Evrak,Taksit,AdayArama Formynlndrm
        private void button2_Click(object sender, EventArgs e)
        {
            Evrakdüzenleme ed = new Evrakdüzenleme();
            ed.ShowDialog();
        }
        private void btnevrakdüzenle_Click(object sender, EventArgs e)
        {
            TaksitDurum td = new TaksitDurum();
            td.ShowDialog();
        }
        private void btnadayara_Click_1(object sender, EventArgs e)
        {
            AdayArama arama = new AdayArama();
            arama.ShowDialog();
        }

        #endregion Evrak,Taksit,AdayArama Formynlndrm

        #region adresler

        private void button6_Click(object sender, EventArgs e)
        {
            label18.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label19.Visible = true;
        }
        #endregion

        #region permaasekle
        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[5]
            {
            new SqlParameter("@Adı",SqlDbType.NVarChar),
            new SqlParameter("@Soyadı",SqlDbType.NVarChar),//  cmd.Parameters.AddWithValue("@DoğumTarihi", dateTimePicker1.Value.ToString("MM.dd.yyyy HH:mm:ss"));
            new SqlParameter("@GirişTarihi",SqlDbType.DateTime),
            new SqlParameter("@Görevi",SqlDbType.NVarChar),
            new SqlParameter("@Maaşı",SqlDbType.Money),
            };
            pDizi[0].Value = txtpadı.Text;
            pDizi[1].Value = txtpsadı.Text;
            pDizi[2].Value = datepbil.Text;
            pDizi[3].Value = txtpgörevi.Text;
            pDizi[4].Value = txtpmaas.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO permaas (Adı,Soyadı,GirişTarihi,Görevi,Maaşı) VALUES (@Adı,@Soyadı,@GirişTarihi,@Görevi,@Maaşı)", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("KAYIT BAŞARI İLE EKLENDİ");
                dataGridViewpermaas.DataSource = wws.SqlDataTable("SELECT * FROM permaas", null);
            }
            else
            {
                MessageBox.Show("KAYIT EKLENEMEDİ");
            }
        }
        #endregion permaassekle

        private void button3_Click_1(object sender, EventArgs e)
        {
            txtpmaas.Text = null;
            txtpsadı.Text = null;
            txtpadı.Text = null;
            txtpgörevi.Text = null;
        }

        #region permaassil
        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@KayıtID",SqlDbType.Int),
            };
            pDizi[0].Value = int.Parse(label51.Text);
            int Sonuc = 0;
            Sonuc = wws.SqlExeCuteNonQuery("DELETE FROM permaas WHERE KayıtID=@KayıtID", pDizi);
            if (Sonuc == 1)
            {
                MessageBox.Show("KAYIT SİLİNDİ");
                dataGridViewpermaas.DataSource = wws.SqlDataTable("SELECT * FROM permaas", null);
            }
            else
            {
                MessageBox.Show("KAYIT SİLİNEMEDİ");
            }
        }

        #endregion permaassil

        #region permaascellclick
        private void dataGridViewpermaas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                label51.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtpadı.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtpsadı.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[2].Value.ToString();
                datepbil.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtpgörevi.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtpmaas.Text = dataGridViewpermaas.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
        }

        #endregion permaascellclick

        #region permaasgüncelle
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[6]
            {
            new SqlParameter("@Adı",SqlDbType.NVarChar),
            new SqlParameter("@Soyadı",SqlDbType.NVarChar),
            new SqlParameter("@GirişTarihi",SqlDbType.DateTime),
            new SqlParameter("@Görevi",SqlDbType.NVarChar),
            new SqlParameter("@Maaşı",SqlDbType.Money),
            new SqlParameter("@KayıtID",SqlDbType.Int),
            };
            pDizi[0].Value = txtpadı.Text;
            pDizi[1].Value = txtpsadı.Text;
            pDizi[2].Value = datepbil.Text;
            pDizi[3].Value = txtpgörevi.Text;
            pDizi[4].Value = txtpmaas.Text;
            pDizi[5].Value = label51.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE permaas SET Adı=@Adı,Soyadı=@Soyadı,GirişTarihi=@GirişTarihi,Görevi=@Görevi,Maaşı=@Maaşı WHERE KayıtID=@KayıtID", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("GÜNCELLEME İŞLEMİ GERÇEKLEŞTİRİLDİ");
                dataGridViewpermaas.DataSource = wws.SqlDataTable("SELECT * FROM permaas", null);
            }
            else
            {
                MessageBox.Show("İŞLEM BAŞARISIZ");
            }
        }

        #endregion permaasgüncelle

        #region gelirekle

        private void button4_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[4]
           {
               new SqlParameter("@Açıklama",SqlDbType.NVarChar),
               new SqlParameter("@Tarih",SqlDbType.DateTime),
               new SqlParameter("@GelirAdı",SqlDbType.NVarChar),
               new SqlParameter("@GelirMiktarı",SqlDbType.Money),
           };
            pDizi[0].Value = txtgacklma.Text;
            pDizi[1].Value = dateGelir.Text;
            pDizi[2].Value = txtGad.Text;
            pDizi[3].Value = txtGmiktar.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO gelirtbl (Açıklama,Tarih,GelirAdı,GelirMiktarı) Values (@Açıklama,@Tarih,@GelirAdı,@GelirMiktarı)", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("GELİR TABLOSUNA EKLENDİ");
                datagelirtablo.DataSource = wws.SqlDataTable("SELECT*FROM gelirtbl", null);
            }
            else
            { MessageBox.Show("GELİR İŞLEMİ GERÇEKLEŞTİRİLEMEDİ"); }
        }
        #endregion gelirekle

        private void button12_Click(object sender, EventArgs e)
        {
            txtGmiktar.Text = "";
            txtgacklma.Text = "";
            txtGad.Text = "";
        }

        #region giderekle

        private void btngiderekle_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[4]
           {
               new SqlParameter("@Açıklama",SqlDbType.NVarChar),
               new SqlParameter("@Tarih",SqlDbType.DateTime),
               new SqlParameter("@GiderAdı",SqlDbType.NVarChar),
               new SqlParameter("@GiderMiktarı",SqlDbType.Money),
           };
            pDizi[0].Value = txtgdracklma.Text;
            pDizi[1].Value = dateGider.Text;
            pDizi[2].Value = txtGdrad.Text;
            pDizi[3].Value = txtGdrmiktar.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO gidertbl (Açıklama,Tarih,GiderAdı,GiderMiktarı) Values (@Açıklama,@Tarih,@GiderAdı,@GiderMiktarı)", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("GİDER TABLOSUNA EKLENDİ");
                datagidertablo.DataSource = wws.SqlDataTable("SELECT*FROM gidertbl", null);
            }
            else
            {
                MessageBox.Show("GİDER İŞLEMİ GERÇEKLEŞTİRİLEMEDİ");
            }
        }

        #endregion giderekle

        private void btngdrtemizle_Click(object sender, EventArgs e)
        {
            txtGdrmiktar.Text = "";
            txtgdracklma.Text = "";
            txtGdrad.Text = "";
        }

        #region Gelirsil

        private void btngelirsil_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@KayıtID",SqlDbType.Int),
            };
            pDizi[0].Value = int.Parse(label53.Text);
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("DELETE FROM gelirtbl WHERE KayıtID=@KayıtID", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("SİLME İŞLEMİ GERÇEKLEŞTİRİLDİ");
                datagelirtablo.DataSource = wws.SqlDataTable("SELECT * FROM gelirtbl", null);
            }
            else
            { MessageBox.Show("SİLME İŞLEMİ BAŞARISIZ"); }
        }
        #endregion Gelirsil

        #region Gelirgüncelle
        private void btngelirgnclle_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[5]
           {
               new SqlParameter("@Açıklama",SqlDbType.NVarChar),
               new SqlParameter("@Tarih",SqlDbType.DateTime),
               new SqlParameter("@GelirAdı",SqlDbType.NVarChar),
               new SqlParameter("@GelirMiktarı",SqlDbType.Money),
                 new SqlParameter("@KayıtID",SqlDbType.Int),
           };
            pDizi[0].Value = txtgacklma.Text;
            pDizi[1].Value = dateGelir.Text;
            pDizi[2].Value = txtGad.Text;
            pDizi[3].Value = txtGmiktar.Text;
            pDizi[4].Value = label53.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE gelirtbl SET Açıklama=@Açıklama,Tarih=@Tarih,GelirAdı=@GelirAdı,GelirMiktarı=@GelirMiktarı WHERE KayıtID=@KayıtID", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("Güncelleme Gerçekleştirildi");
                datagelirtablo.DataSource = wws.SqlDataTable("SELECT*FROM gelirtbl", null);
            }
            else
            {
                MessageBox.Show("Güncelleme Başarısız");
            }
        }
        #endregion Gelirgüncelle

        #region DataGelirTabloCellClick
        private void datagelirtablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                label53.Text = datagelirtablo.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtgacklma.Text = datagelirtablo.Rows[e.RowIndex].Cells[1].Value.ToString();
                dateGelir.Text = datagelirtablo.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtGad.Text = datagelirtablo.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtGmiktar.Text = datagelirtablo.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }
        #endregion DataGelirTabloCellClick

        #region DataGiderTabloCellClick
        private void datagidertablo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                label54.Text = datagidertablo.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtgdracklma.Text = datagidertablo.Rows[e.RowIndex].Cells[1].Value.ToString();
                dateGider.Text = datagidertablo.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtGdrad.Text = datagidertablo.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtGdrmiktar.Text = datagidertablo.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }
        #endregion DataGiderTabloCellClick

        #region GİDERSİL
        private void btngdrsil_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@KayıtID",SqlDbType.Int),
            };
            pDizi[0].Value = int.Parse(label54.Text);
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("DELETE FROM gidertbl WHERE KayıtID=@KayıtID", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("SİLME İŞLEMİ GERÇEKLEŞTİRİLDİ");
                datagidertablo.DataSource = wws.SqlDataTable("SELECT * FROM gidertbl", null);
            }
            else
            { MessageBox.Show("SİLME İŞLEMİ BAŞARISIZ"); }
        }

        #endregion GİDERSİL

        #region GİDERgüncelle
        private void btngidergncelle_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[5]
        {
              new SqlParameter("@Açıklama",SqlDbType.NVarChar),
               new SqlParameter("@Tarih",SqlDbType.DateTime),
               new SqlParameter("@GiderAdı",SqlDbType.NVarChar),
               new SqlParameter("@GiderMiktarı",SqlDbType.Money),
                 new SqlParameter("@KayıtID",SqlDbType.Int),
           };
            pDizi[0].Value = txtgdracklma.Text;
            pDizi[1].Value = dateGider.Text;
            pDizi[2].Value = txtGdrad.Text;
            pDizi[3].Value = txtGdrmiktar.Text;
            pDizi[4].Value = label54.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE gidertbl SET Açıklama=@Açıklama,Tarih=@Tarih,Gideradı=@GiderAdı,GiderMiktarı=@GiderMiktarı WHERE KayıtID=@KayıtID", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("Güncelleme Gerçekleştirildi");
                datagidertablo.DataSource = wws.SqlDataTable("SELECT*FROM gidertbl", null);
            }
            else
            {
                MessageBox.Show("Güncelleme Başarısız");
            }

        #endregion GİDERgüncelle

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            frmad.Text = "";
            frmbas.Text = "";
            frmtel.Text = "";
        }

        #region dataevrak cell

        private void dataevrak_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                evrakno.Text = dataevrak.Rows[e.RowIndex].Cells[0].Value.ToString();
                evrakad.Text = dataevrak.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }
        #endregion dataevrak cell

        #region dataehliyet cell
        private void dataehliyet_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                textBox2.Text = dataehliyet.Rows[e.RowIndex].Cells[1].Value.ToString();
                label67.Text = dataehliyet.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }
        #endregion dataehliyet cell

        #region datasnf cell
        private void datasnf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox6.Text = datasnf.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox7.Text = datasnf.Rows[e.RowIndex].Cells[2].Value.ToString();
                label65.Text = datasnf.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }
        #endregion datasnf cell

        #region datakullanıcı cell

        private void datakullanıcı_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                textBox5.Text = datakullanıcı.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox4.Text = datakullanıcı.Rows[e.RowIndex].Cells[1].Value.ToString();
                label66.Text = datakullanıcı.Rows[e.RowIndex].Cells[2].Value.ToString();
            }

        }

        #endregion datakullanıcı cell

        private void button34_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";

        }

        #region datakullanıcı ekle
        private void button31_Click(object sender, EventArgs e)
        {

            SqlParameter[] QPR = new SqlParameter[2]
            {
                new SqlParameter("@Adı",SqlDbType.NVarChar),
                new SqlParameter("@Parola",SqlDbType.NVarChar),
            };
            QPR[0].Value = textBox5.Text;
            QPR[1].Value = textBox4.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO uye (Adı,Parola) VALUES (@Adı,@Parola)", QPR);

            if (sonuc == 1)
            {
                MessageBox.Show("KAYDETME GERÇEKLEŞTİRİLDİ");
                datakullanıcı.DataSource = wws.SqlDataTable("SELECT Adı,Parola,SIRANO FROM uye", null);
            }
            else
            {
                MessageBox.Show("KAYDETME BAŞARISIZ");
            }

        }
        #endregion datakullanıcı ekle

        #region datakullanıcı sil
        private void button33_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@SIRANO",SqlDbType.Int),
            };
            pDizi[0].Value = label66.Text;
            int Sonuc = 0;
            Sonuc = wws.SqlExeCuteNonQuery("DELETE FROM uye WHERE SIRANO=@SIRANO", pDizi);
            if (Sonuc == 1)
            {
                MessageBox.Show("KAYIT SİLİNDİ");
                datakullanıcı.DataSource = wws.SqlDataTable("SELECT Adı,Parola,SIRANO FROM uye", null);
            }
            else
            {
                MessageBox.Show("KAYIT SİLİNEMEDİ");
            }
        }
        #endregion datakullanıcı sil

        #region datakullanıcı Güncelle

        private void button32_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[3]
            {
                new SqlParameter("@Adı",SqlDbType.NVarChar),
                new SqlParameter("@Parola",SqlDbType.NVarChar),
                new SqlParameter("@SIRANO",SqlDbType.Int),
            };
            pDizi[0].Value = textBox5.Text;
            pDizi[1].Value = textBox4.Text;
            pDizi[2].Value = label66.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE uye SET Adı=@Adı,Parola=@Parola WHERE SIRANO=@SIRANO", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("ÜYE GÜNCELLENDİ...");
                datakullanıcı.DataSource = wws.SqlDataTable("SELECT Adı,Parola,SIRANO FROM uye", null);
            }
            else
            {
                MessageBox.Show("GÜNCELLENEMEDİ");
            }
        }
        #endregion

        private void cmbsınıfadı_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@SınıfAdı",SqlDbType.NVarChar),
            };
            pDizi[0].Value = cmbsınıfadı.SelectedValue.ToString();
            datakullanıcı.DataSource = wws.SqlDataTable("SELECT SınıfAdı  FROM eSınıf WHERE SınıfAdı=@SınıfAdı", pDizi);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        #region Ehliyet KAYDET
        private void button27_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@EhliyetSınıfAdı",SqlDbType.NVarChar)
            };
            pDizi[0].Value = textBox2.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO Ehliyet (EhliyetSınıfAdı) VALUES (@EhliyetSınıfAdı)", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("Ehliyet Sınıfına KAYDEDİLDİ...");
                dataehliyet.DataSource = wws.SqlDataTable("SELECT * FROM Ehliyet", null);
            }
            else
            {
                MessageBox.Show("KAYDETME BAŞARISIZ");
            }
        }
        #endregion

        #region Ehliyet sil
        private void button29_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@Ehliyet_id",SqlDbType.Int),
            };
            pDizi[0].Value = label67.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("DELETE FROM Ehliyet WHERE Ehliyet_id=@Ehliyet_id", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("SİLME İŞLEMİ GERÇEKLEŞTİRİLDİ...");
                dataehliyet.DataSource = wws.SqlDataTable("SELECT * FROM Ehliyet", null);
            }
            else
            {
                MessageBox.Show("İŞLEM BAŞARISIZ");
            }
        }
        #endregion

        #region Ehliyet Güncelle
        private void button28_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[2]
            {
                new SqlParameter("@Ehliyet_id",SqlDbType.Int),
                new SqlParameter("@EhliyetSınıfAdı",SqlDbType.NVarChar),
            };
            pDizi[0].Value = label67.Text;
            pDizi[1].Value = textBox2.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE Ehliyet SET EhliyetSınıfAdı=@EhliyetSınıfAdı WHERE Ehliyet_id=@Ehliyet_id", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("GÜNCELLENDİ...");
                dataehliyet.DataSource = wws.SqlDataTable("SELECT * FROM Ehliyet", null);
            }
            else
            {
                MessageBox.Show("İŞLEM BAŞARISIZ");
            }
        }
        #endregion

        private void button38_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox7.Text = "";
        }

        #region sınıfekle

        private void button35_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[2]
            {
                new SqlParameter("@SınıfAdı",SqlDbType.NVarChar),
                new SqlParameter("@Kapasite",SqlDbType.NVarChar),
            };
            pDizi[0].Value = textBox6.Text;
            pDizi[1].Value = textBox7.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO eSınıf (SınıfAdı,Kapasite) VALUES (@SınıfAdı,@Kapasite)", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("SINIF KAYDEDİLDİ...");
                datasnf.DataSource = wws.SqlDataTable("SELECT * FROM eSınıf", null);
            }
            else
            {
                MessageBox.Show("İŞLEM BAŞARISIZ");
            }
        }

        #endregion

        #region Sınıf günclle
        private void button36_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[3]
            {
                new SqlParameter("@SınıfAdı",SqlDbType.NVarChar),
                new SqlParameter("@Kapasite",SqlDbType.NVarChar),
                new SqlParameter("@e_id",SqlDbType.Int),
            };
            pDizi[0].Value = textBox6.Text;
            pDizi[1].Value = textBox7.Text;
            pDizi[2].Value = label65.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE eSınıf SET SınıfAdı=@SınıfAdı,Kapasite=@Kapasite WHERE e_id=@e_id", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("GÜNCELLEME BAŞARIYLA GERÇEKLEŞTİ");
                datasnf.DataSource = wws.SqlDataTable("SELECT * FROM eSınıf", null);
            }
            else
            {
                MessageBox.Show("İŞLEM GERÇEKLEŞTİRİLEMEDİ !");
            }
        }

        #endregion

        #region sınıfsil

        private void button37_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("e_id",SqlDbType.Int),
            };
            pDizi[0].Value = label65.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("DELETE FROM eSınıf WHERE e_id=@e_id", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("SINIF SİLİNDİ...");
                datasnf.DataSource = wws.SqlDataTable("SELECT * FROM eSınıf", null);
            }
            else
            {
                MessageBox.Show("SINIF SİLİNEMEDİ ! ");
            }
        }

        #endregion

        private void button26_Click(object sender, EventArgs e)
        {
            evrakad.Text = "";
            evrakno.Text = "";
        }

        #region evrakekle
        private void button23_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[2]
            {
                new SqlParameter("@EvrakNo",SqlDbType.Int),
                new SqlParameter("@EvrakAdı",SqlDbType.NVarChar),
            };
            pDizi[0].Value = evrakno.Text;
            pDizi[1].Value = evrakad.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO Evrak (EvrakNo,EvrakAdı) VALUES (@EvrakNo,@EvrakAdı)", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("EVRAKLARA EKLENDİ...");
                dataevrak.DataSource = wws.SqlDataTable("SELECT * FROM Evrak", null);
            }
            else
            {
                MessageBox.Show("EVRAK EKLEME BAŞARISIZ ! ");
            }
        }

        #endregion

        #region evrakgünclle
        private void button24_Click(object sender, EventArgs e)
        {

            SqlParameter[] pDizi = new SqlParameter[2]
            {
                new SqlParameter("@EvrakNo",SqlDbType.Int),
                new SqlParameter("@EvrakAdı",SqlDbType.NVarChar),
            };
            pDizi[0].Value = evrakno.Text;
            pDizi[1].Value = evrakad.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE Evrak SET EvrakNo=@EvrakNo,EvrakAdı=@EvrakAdı WHERE EvrakNo=@EvrakNo", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("GÜNCELLENDİ...");
                dataevrak.DataSource = wws.SqlDataTable("SELECT * FROM Evrak", null);
            }
            else
            {
                MessageBox.Show("GÜNCELLEME BAŞARISIZ ! ");
            }
        }

        #endregion

        #region evraksil

        private void button25_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@EvrakNo",SqlDbType.Int),
            };
            pDizi[0].Value = evrakno.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("DELETE FROM Evrak WHERE EvrakNo=@EvrakNo", pDizi);
            if (sonuc == 1)
            {
                MessageBox.Show("EVRAK SİLİNDİ...");
                dataevrak.DataSource = wws.SqlDataTable("SELECT * FROM Evrak", null);
            }
            else
            {
                MessageBox.Show("EVRAK SİLİNEMEDİ ! ");
            }
        }
        #endregion

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {

                SqlParameter[] pd = new SqlParameter[2] 
                {
                new SqlParameter("@t1",SqlDbType.DateTime),
                new SqlParameter("@t2",SqlDbType.DateTime)
                };
                pd[0].Value = dateTimePicker5.Value;
                pd[1].Value = dateTimePicker6.Value;

                DataTable dt = new DataTable();
                dt = wws.SqlDataTable("SELECT SUM(GelirMiktarı) FROM gelirtbl WHERE Tarih BETWEEN @t1 AND @t2 ", pd);
                label49.Text=(dt.Rows[0][0].ToString());

                //DataTable dt = new DataTable();
                //dt.Clear();
                //dt = wws.SqlDataTable("SELECT SUM(GelirMiktarı) FROM gelirtbl WHERE Tarih BETWEEN '" + Convert.ToDateTime(dateTimePicker5) + "' AND '" + Convert.ToDateTime(dateTimePicker6) + "' ", null);
                //label49.Text = dt.Rows[0][0].ToString();

                //DataTable dta = new DataTable();
                //dta.Clear();
                //dta = wws.SqlDataTable("SELECT SUM(GiderMiktarı) FROM gidertbl WHERE Tarih BETWEEN '" + Convert.ToDateTime(dateTimePicker5) + "' AND ' " + Convert.ToDateTime(dateTimePicker6), null);
                //label50.Text = dta.Rows[0][0].ToString();

           

                SqlParameter[] pda = new SqlParameter[2] 
                {
                new SqlParameter("@t1",SqlDbType.DateTime),
                new SqlParameter("@t2",SqlDbType.DateTime)
                };
                pda[0].Value = dateTimePicker5.Value;
                pda[1].Value = dateTimePicker6.Value;

                DataTable dta = new DataTable();
                dta = wws.SqlDataTable("SELECT SUM(GiderMiktarı) FROM gidertbl WHERE Tarih BETWEEN  @t1 AND @t2 ", pda);
                label50.Text = (dta.Rows[0][0].ToString());
            
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDb)\v11.0;AttachDbFileName=|DataDirectory|\srckurs.mdf;Integrated Security=True;Connection Timeout=30";
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Aday WHERE ADI LIKE '%" + textBox13.Text + "%'", con);
                da.SelectCommand = cmd;
                cmd.ExecuteNonQuery();
                da.Fill(ds);
                datagridaday.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnadayara_Click(object sender, EventArgs e)
        {
            AdayArama a = new AdayArama();
            a.ShowDialog();
        }

        private void btnevrakdüzenle_Click_1(object sender, EventArgs e)
        {
            Evrakdüzenleme ea = new Evrakdüzenleme();
            ea.ShowDialog();
        }

        private void btntaksitdurum_Click(object sender, EventArgs e)
        {
            TaksitDurum td = new TaksitDurum();
            td.ShowDialog();
        }

        #region personelmaasödemesi
        private void btnpermaasöde_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl.Clear();
            tbl = wws.SqlDataTable("SELECT SUM (Maaşı) FROM permaas", null);
            int toplamGiderMaas = Convert.ToInt32(tbl.Rows[0][0]);
            MessageBox.Show(toplamGiderMaas.ToString() + " Ödenecek ...");

            SqlParameter[] pDizi = new SqlParameter[4] 
            {
                  new SqlParameter("@Açıklama",SqlDbType.NVarChar),
                  new SqlParameter("@Tarih",SqlDbType.DateTime),
                  new SqlParameter("@GiderAdı",SqlDbType.NVarChar),
                  new SqlParameter("@GiderMiktarı",SqlDbType.Money),            
            };
            pDizi[0].Value = Convert.ToString(dateGider.Value) + " Tarihli Personel Maaş Ödemesi";
            pDizi[1].Value = dateGider.Text;
            pDizi[2].Value = "Personel Maaş Ödemesi";
            pDizi[3].Value = toplamGiderMaas;

            //MessageBox.Show(Convert.ToDateTime(dateGider.Value).ToString() +" \n Tarihli Personel Maaş Ödemesi");

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO gidertbl (Açıklama,Tarih,GiderAdı,GiderMiktarı) Values (@Açıklama,@Tarih,@GiderAdı,@GiderMiktarı)", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("Tüm Personelin Maaşı Ödendi \n GİDER TABLOSUNA EKLENDİ");
                datagidertablo.DataSource = wws.SqlDataTable("SELECT * FROM gidertbl", null);
            }
            else
            {
                MessageBox.Show("İŞLEM GERÇEKLEŞTİRİLEMEDİ ! ");
            }

        #endregion

        }

        private void cmbesınıf_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[1]
            {
                new SqlParameter("@EhliyetSınıfAdı",SqlDbType.NVarChar),
            };
            pDizi[0].Value = cmbesınıf.SelectedValue.ToString();
            dataehliyet.DataSource = wws.SqlDataTable("SELECT EhliyetSınıfAdı  FROM Ehliyet WHERE EhliyetSınıfAdı=@EhliyetSınıfAdı", pDizi);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[16]
            {
                new SqlParameter("@TCKİMLİKNO",SqlDbType.NVarChar),
                new SqlParameter("@ADAYNO",SqlDbType.Int),
                 new SqlParameter("@ADI",SqlDbType.NVarChar),
                  new SqlParameter("@SOYADI",SqlDbType.NVarChar),
                    new SqlParameter("@BABAADI",SqlDbType.NVarChar),
                      new SqlParameter("@ANAADI",SqlDbType.NVarChar),
                        new SqlParameter("@DOĞUMYERİ",SqlDbType.NVarChar),
                          new SqlParameter("@DOĞUMTARİHİ",SqlDbType.DateTime),
                            new SqlParameter("@KANGRUBU",SqlDbType.NVarChar),
                            new SqlParameter("@EĞİTİMDURUMU",SqlDbType.NVarChar),
                            new SqlParameter("@CEPTEL",SqlDbType.NVarChar),
                            new SqlParameter("@KAYITTARİHİ",SqlDbType.DateTime),
                            new SqlParameter("@ADRES",SqlDbType.NVarChar),
                            new SqlParameter("@SINIFADI",SqlDbType.NVarChar),
                            new SqlParameter("@EHLİYETSINIFI",SqlDbType.NVarChar),
                            new SqlParameter("@RESİM",SqlDbType.Image )
            };
            pDizi[0].Value = maskedtc.Text;
            pDizi[1].Value = textBox1.Text;
            pDizi[2].Value = txtadı.Text;
            pDizi[3].Value = txtsoyadı.Text;
            pDizi[4].Value = txtbadı.Text;
            pDizi[5].Value = txtaadı.Text;
            pDizi[6].Value = txtdyeri.Text;
            pDizi[7].Value = Convert.ToDateTime(maskeddtarihi.Text);
            pDizi[8].Value = cmbkgrubu.Text;
            pDizi[9].Value = cmbedurumu.Text;
            pDizi[10].Value = Convert.ToString(maskedtxtCepTel.Text);
            pDizi[11].Value = dateTimePicker2.Text;
            pDizi[12].Value = textBox3.Text;
            pDizi[13].Value = cmbsınıfadı.Text;
            pDizi[14].Value = cmbesınıf.Text;
            string resimtur = openFileDialog1.FileName.Split('.')[(openFileDialog1.FileName.Split('.').Length - 1)].ToString();
            pDizi[15].Value = resimtur;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO Aday (TCKİMLİKNO,ADAYNO,ADI,SOYADI,BABAADI,ANAADI,DOĞUMYERİ,DOĞUMTARİHİ,KANGRUBU,EĞİTİMDURUMU,CEPTEL,KAYITTARİHİ,ADRES,SINIFADI,EHLİYETSINIFI,RESİM) VALUES (@TCKİMLİKNO,@ADAYNO,@ADI,@SOYADI,@BABAADI,@ANAADI,@DOĞUMYERİ,@DOĞUMTARİHİ,@KANGRUBU,@EĞİTİMDURUMU,@CEPTEL,@KAYITTARİHİ,@ADRES,@SINIFADI,@EHLİYETSINIFI,@RESİM)", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("ADAY EKLEME İŞLEMİ GERÇEKLEŞTİRİLDİ...");
                datagridaday.DataSource = wws.SqlDataTable("SELECT * FROM Aday", null);
            }
            else
            {
                MessageBox.Show(" ! EKLEME BAŞARISIZ ! ");
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Document dokuman = new Document(PageSize.A4, 20, 20, 5, 5);
            // dokuman.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter.GetInstance(dokuman, new FileStream(Application.StartupPath + "/pdf/rapor", FileMode.Create));
            dokuman.Open();
            BaseFont arialFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            BaseFont verdanaFont = BaseFont.CreateFont(@"C:\Windows\Fonts\verdana.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font yeniFont = new iTextSharp.text.Font(verdanaFont);
            yeniFont.Size = 7;
            iTextSharp.text.Font yeniFont1 = new iTextSharp.text.Font(arialFont, 10);
            iTextSharp.text.Font kalin = new iTextSharp.text.Font(arialFont, 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font kalin_kirmizi = new iTextSharp.text.Font(arialFont, 10, iTextSharp.text.Font.BOLD);
            kalin_kirmizi.Color = BaseColor.RED;
            iTextSharp.text.Font kalin_kucuk = new iTextSharp.text.Font(arialFont, 8, iTextSharp.text.Font.BOLD);
            PdfPTable tablo1 = new PdfPTable(7);
            tablo1.WidthPercentage = (100.0f);
            float[] widths = new float[] { 10f, 10f, 10f, 20f, 15f, 20f, 15f };
            tablo1.SetWidths(widths);
            tablo1.HorizontalAlignment = 1;

            PdfPCell sutun_baslik = new PdfPCell(new Phrase("ADAY LİSTESİ", kalin_kirmizi));
            sutun_baslik.UseVariableBorders = true;
            sutun_baslik.FixedHeight = 20f;
            sutun_baslik.Colspan = 7;
            sutun_baslik.BorderColorBottom = new BaseColor(255, 255, 255);
            sutun_baslik.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            tablo1.AddCell(sutun_baslik);

            PdfPTable gorusme1 = new PdfPTable(datagridaday.Columns.Count);
            gorusme1.WidthPercentage = (100.0f);
            for (int j = 0; j < datagridaday.Columns.Count; j++)
            {
                gorusme1.AddCell(new Phrase(datagridaday.Columns[j].HeaderText, kalin_kirmizi));
            }
            gorusme1.HeaderRows = 1;
            for (int i = 0; i < datagridaday.Rows.Count; i++)
            {
                for (int k = 0; k < datagridaday.Columns.Count; k++)
                {
                    if (datagridaday[k, i].Value != null)
                    {
                        gorusme1.AddCell(new Phrase(datagridaday[k, i].Value.ToString(), yeniFont));
                    }
                }
            }
            dokuman.Add(tablo1);
            dokuman.Add(gorusme1);
            dokuman.Close();
            Process.Start(Application.StartupPath + "/pdf/rapor.pdf");
            datagridaday.DataSource = null;

        }

        private void button20_Click(object sender, EventArgs e)
        {
            datagridaday.DataSource = wws.SqlDataTable("SELECT * FROM Aday", null);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        string resim2 = null;
        private void button5_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Title = "resim ekle";
            openFileDialog1.Filter = "(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(openFileDialog1.FileName);
                resim2 = openFileDialog1.FileName.ToString();

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            adayBindingSource.MoveNext();
            eSınıfBindingSource.MoveNext();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            adayBindingSource.MovePrevious();
            eSınıfBindingSource.MovePrevious();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txtaadı.Text = "";
            txtbadı.Text = "";
            txtdyeri.Text = "";
            maskeddtarihi.Text = "";
            maskedtc.Text = "";
            maskedtxtCepTel.Text = "";
            txtadı.Text = "";
            txtsoyadı.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            label70.Visible = false;
            label70.Text = "Bütün TextBoxlar Temizlendi...";

        }

        private void button11_Click(object sender, EventArgs e)
        {
            firmaBindingSource.MoveNext();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            firmaBindingSource.MovePrevious();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SqlParameter [] pDizi = new SqlParameter[3]
            {
                new SqlParameter("@Firma_baslik",SqlDbType.NVarChar),
                new SqlParameter("@Firma_adi",SqlDbType.NVarChar),
                new SqlParameter("@Firma_tel",SqlDbType.NVarChar),
               
            };
            pDizi[0].Value = frmbas.Text;
            pDizi[1].Value = frmad.Text;
            pDizi[2].Value = frmtel.Text;

            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("INSERT INTO firma (Firma_baslik,Firma_adi,Firma_tel) VALUES (@Firma_baslik,@Firma_adi,@Firma_tel)", pDizi);

            if (sonuc==1)
            {
                MessageBox.Show("FİRMA EKLENDİ");
               dataGridView1.DataSource = wws.SqlDataTable("SELECT * FROM firma", null);
            }
            else
            {
                MessageBox.Show(" FİRMA EKLENEMEDİ ! ...");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SqlParameter[] pDizi = new SqlParameter[4]
            {
                new SqlParameter("@Firma_baslik",SqlDbType.NVarChar),
                new SqlParameter("@Firma_adi",SqlDbType.NVarChar),
                new SqlParameter("@Firma_tel",SqlDbType.NVarChar),
                new SqlParameter("@Firma_id",SqlDbType.Int),
               
            };
            pDizi[0].Value = frmbas.Text;
            pDizi[1].Value = frmad.Text;
            pDizi[2].Value = frmtel.Text;
            pDizi[3].Value = label72.Text;
            int sonuc = 0;
            sonuc = wws.SqlExeCuteNonQuery("UPDATE firma SET Firma_baslik=@Firma_baslik,Firma_adi=@Firma_adi,Firma_tel=@Firma_tel WHERE Firma_id=@Firma_id", pDizi);

            if (sonuc == 1)
            {
                MessageBox.Show("FİRMA Güncellendi");
                dataGridView1.DataSource = wws.SqlDataTable("SELECT * FROM firma", null);
            }
            else
            {
                MessageBox.Show(" FİRMA Güncellenemedi ! ...");
            }
        }
    }

}

        