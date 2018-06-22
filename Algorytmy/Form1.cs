using System;
using System.Windows.Forms;

namespace Algorytmy
{
    public partial class Form1 : Form

    {
        private Random losuj = new Random();
        private Tekst txt;
        private string T;
        private string PT;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (BM.Checked)
            {
                if (textBox1.TextLength > 0)
                {
                    BMSearch.BoyerMoore BM = new BMSearch.BoyerMoore(textBox1.Text);
                    BM.Initialize(textBox1.Text);
                    int A = BM.Search(txt.txt);
                     A = BM.bmSearch(textBox1.Text, txt.txt);
                    if (A == 0)
                        MessageBox.Show("Ni ma");
                    else
                        MessageBox.Show("Występoje " + Convert.ToString(A) + "  razy");
                }
            }

            if (KMP_box.Checked)
            {
                if (textBox1.TextLength > 0)
                {

                    int KMP = txt.KMPSearch(textBox1.Text);
                    if (KMP == 0)
                        MessageBox.Show("Ni ma");
                    else
                        MessageBox.Show("Występoje " + Convert.ToString(KMP) + "  razy");
                }
            }
            if (sunday_box.Checked)
            {
                if (textBox1.TextLength > 0)
                {

                    int Sunday = txt.SundaySeartch(textBox1.Text);
                    int SundayMeta = txt.SundayMeta(textBox1.Text);
                    if (Sunday == 0)
                        MessageBox.Show("Ni ma");
                    else
                        MessageBox.Show("Występoje Sunday" + Convert.ToString(Sunday) + "  razy");
                    MessageBox.Show("Występoje MetaSunday " + Convert.ToString(SundayMeta) + "  razy");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
                MessageBox.Show("Podaj długość tekstu", "Uwaga", MessageBoxButtons.OK);
            else
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                progressBar1.Value = 0;
                progressBar1.Maximum = Convert.ToInt32(textBox3.Text);
                progressBar1.Step = 1;
                int dlugosc;

                if (textBox2.TextLength > 0)
                {
                    dlugosc = Convert.ToInt32(textBox2.Text);
                    char litera;

                    for (int i = 0; i < Convert.ToInt32(textBox3.Text); i++)
                    {
                        litera = (char)losuj.Next(65, 65 + dlugosc);
                        T += litera;
                        progressBar1.PerformStep();

                    }
                    txt = new Tekst(T);
                    T = T.Remove(0);
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    MessageBox.Show("Tekst wygenerowany zajeło to " + Convert.ToString(elapsedMs) + " ms");
                }
                else
                    MessageBox.Show("Wpisz długość alfabetu");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (txt.txt.Length < 2500)
                MessageBox.Show(txt.txt);
            else
                MessageBox.Show("Tekst za długi");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(@"D:\tekst.txt", txt.txt);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = Algorytmy.Properties.Resources.tekstLosowy;
            T = text;
            txt = new Tekst(text);
        }

        private void button_badaj_Click(object sender, EventArgs e)
        {
            bool wzorzeclosuj = true;
           
                if (Lbox.Checked)
                {
                    string text = Algorytmy.Properties.Resources.tekstLosowy;
                    T = text;
                }
                if (DNA_box.Checked)
                {
                    string text = Algorytmy.Properties.Resources.DNA;
                    T = text;
                }
                if (Tbox.Checked)
                {
                    string text = Algorytmy.Properties.Resources.Tadek;
                    T = text;
                }
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
            if(KW_BOX.Checked)
            {
                wzorzeclosuj = false;
                PT = textBox4.Text;
            }
            foreach (var series in chart2.Series)
            {
                series.Points.Clear();
            }
            char litera;
            string U;
            if (wzorzeclosuj == true)
            {
                for (int i = 0; i < Convert.ToInt32(Patern_min.Text); i++)
                {
                    litera = (char)losuj.Next(65, 65 + Convert.ToInt32(Alfabet.Text));
                    PT += litera;
                }
            }
            for (int u = Convert.ToInt32(Text_min.Text); u < Convert.ToInt32(Tekst_max.Text); u = u + 1000)
            {
                U = T;
                U = U.Remove(u);
                txt = new Tekst(U);
                chart1.Update();

                if (KMP_box.Checked)
                {
                    var watchKMP = System.Diagnostics.Stopwatch.StartNew();
                    txt.KMPSearch(PT);
                    watchKMP.Stop();
                    chart1.Series["KMP"].Points.AddXY((double)u, Convert.ToInt32(watchKMP.ElapsedTicks));
                }
                if (sunday_box.Checked)
                {
                    var watchSunday = System.Diagnostics.Stopwatch.StartNew();
                    txt.SundaySeartch(PT);
                    watchSunday.Stop();
                    chart1.Series["Sunday"].Points.AddXY((double)u, Convert.ToInt32(watchSunday.ElapsedTicks));
                }
                if (BM.Checked)
                {
                    txt.preBM(PT);
                    var watchBM = System.Diagnostics.Stopwatch.StartNew();
                    txt.BMSeartch();
                    watchBM.Stop();
                    chart1.Series["BM"].Points.AddXY((double)u, Convert.ToInt32(watchBM.ElapsedTicks));
                }

            }
            ////////////////////Wzorzec
            PT = PT.Remove(0);
            if(wzorzeclosuj)
            { 
            for (int i = 0; i < Convert.ToInt32(Patern_min.Text); i++)
            {
                litera = (char)losuj.Next(65, 65 + Convert.ToInt32(Alfabet.Text));
                PT += litera;
            }

                for (int u = Convert.ToInt32(Patern_min.Text); u < Convert.ToInt32(Patern_Max.Text); u = u + 10)
                {

                    for (int i = PT.Length; i < u; i++)
                    {
                        litera = (char)losuj.Next(65, 65 + Convert.ToInt32(Alfabet.Text));
                        PT += litera;
                    }

                    chart2.Update();

                    if (KMP_box.Checked)
                    {
                        var watchKMP = System.Diagnostics.Stopwatch.StartNew();
                        txt.KMPSearch(PT);
                        watchKMP.Stop();
                        chart2.Series["KMP"].Points.AddXY((double)u, Convert.ToInt32(watchKMP.ElapsedTicks));

                    }
                    if (sunday_box.Checked)
                    {
                        var watchSunday = System.Diagnostics.Stopwatch.StartNew();
                        txt.SundaySeartch(PT);
                        watchSunday.Stop();
                        chart2.Series["Sunday"].Points.AddXY((double)u, Convert.ToInt32(watchSunday.ElapsedTicks));

                    }
                    if (BM.Checked)
                    {

                        BMSearch.BoyerMoore BM = new BMSearch.BoyerMoore(textBox1.Text);
                        txt.preBM(PT);
                        var watchBM = System.Diagnostics.Stopwatch.StartNew();
                        txt.BMSeartch();
                        watchBM.Stop();
                        chart2.Series["BM"].Points.AddXY((double)u, Convert.ToInt32(watchBM.ElapsedTicks));
                    }
                }
                PT.Remove(0);
                T.Remove(0);

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string text = Algorytmy.Properties.Resources.Tadek;
            T = text;
            txt = new Tekst(text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string text = System.IO.File.ReadAllText(@"D:\tekst.txt");
            T = text;
            txt = new Tekst(text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string text = Algorytmy.Properties.Resources.DNA;
            T = text;
            txt = new Tekst(text);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

      


