using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
// zmiana programu
namespace przeglądarka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int nr = 0;
        WebBrowser webBrowser = new WebBrowser();

        public void GoToSite()
        {
            try
            {
                if (toolStripComboBox1.Text != "")
                {
                    if (!toolStripComboBox1.Text.StartsWith("http://") &&
                        !toolStripComboBox1.Text.StartsWith("https://"))
                    {
                        toolStripComboBox1.Text = @"http://" + toolStripComboBox1.Text;
                        ((WebBrowser) tabControl.SelectedTab.Controls[0]).Navigate(toolStripComboBox1.Text);
                        if (!toolStripComboBox1.Items.Contains(toolStripComboBox1.Text))
                        {
                            toolStripComboBox1.Items.Add(toolStripComboBox1.Text);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser = new WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Visible = true;
            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
            tabControl.TabPages.Add("Nowa karta");
            tabControl.SelectTab(nr);
            tabControl.SelectedTab.Controls.Add(webBrowser);
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).Navigate("google.pl");
            nr += 1;
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                tabControl.SelectedTab.Text = ((WebBrowser) tabControl.SelectedTab.Controls[0]).DocumentTitle;
                toolStripComboBox1.Text = ((WebBrowser) tabControl.SelectedTab.Controls[0]).Url.ToString();
                if (((WebBrowser) tabControl.SelectedTab.Controls[0]).CanGoBack) button2.Enabled = true;
                else button2.Enabled = false;
                if (((WebBrowser) tabControl.SelectedTab.Controls[0]).CanGoForward) button3.Enabled = true;
                else button3.Enabled = false;
            }
            catch
            {
                MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
            }
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                GoToSite();
            }
            catch
            {
                MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                try
                {
                    string url = $"http://www.google.pl/search?q={textBox1.Text.Replace(" ", "+")}";
                    ((WebBrowser) tabControl.SelectedTab.Controls[0]).Navigate(url);
                }
                catch
                {
                    MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).GoBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).GoForward();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).Refresh();
        }

        private void noweOknoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Mateusz Szopa \n IVIA \n 2014");
        }


        private void nowaKartaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            webBrowser = new WebBrowser();
            webBrowser.ScriptErrorsSuppressed = true;
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Visible = true;
            webBrowser.DocumentCompleted += webBrowser_DocumentCompleted;
            tabControl.TabPages.Add("Nowa karta");
            tabControl.SelectTab(nr);
            tabControl.SelectedTab.Controls.Add(webBrowser);
            nr += 1;
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).Navigate("google.pl");
        }

        private void tabControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
             
                if (tabControl.TabPages.Count - 1 > 0)
                {
                    tabControl.TabPages.RemoveAt(tabControl.SelectedIndex);
                    tabControl.SelectTab(tabControl.TabPages.Count - 1);
                    nr -= 1;
                }
                else
                {
                    Application.Exit();
                }
            }
            catch
            {
                MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = ((WebBrowser) tabControl.SelectedTab.Controls[0]).Url.ToString();
        }

        private void tabControl_TabStopChanged(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = ((WebBrowser) tabControl.SelectedTab.Controls[0]).Url.ToString();
        }

        private void toolStripComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                try
                {
                    GoToSite();
                }
                catch
                {
                    MessageBox.Show(@"Niespodziewany błąd. Przepraszamy");
                }
            }
        }

        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void toolStripComboBox1_MouseEnter(object sender, EventArgs e)
        {
            ((WebBrowser) tabControl.SelectedTab.Controls[0]).Navigate(toolStripComboBox1.Text);
        }
    }
}
