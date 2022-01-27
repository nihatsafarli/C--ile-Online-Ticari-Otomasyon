﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Ticari_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("Istanbul", 10);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 12);
            //chartControl1.Series["Series 1"].Points.AddPoint("Bursa", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Antalya", 12);

            SqlDataAdapter da = new SqlDataAdapter("select UrunAd, sum(Adet) as 'Miktar'  from TBL_URUNLER group by URUNAD",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //charta stok miktarı ekleme

            SqlCommand komut = new SqlCommand("select UrunAd, sum(Adet) as 'Miktar'  from TBL_URUNLER group by URUNAD", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();

            //charta firma şehir sayısı ekleme
            SqlCommand komut1 = new SqlCommand("select IL, count(*) from TBL_FIRMALAR group by IL", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr1[0]), int.Parse(dr1[1].ToString()));
            }
            bgl.baglanti().Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay fr = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                fr.ad = dr["URUNAD"].ToString();
            }
            fr.Show();
        }
    }
}
