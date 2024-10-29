using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProductCodeGenerator
{
    public partial class Form1 : Form
    {
        private readonly string TITLE = "ProductCodeGenerator";
        private readonly Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// btnHelpが押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A: Alphabet\nN: Num", this.TITLE);
        }

        /// <summary>
        /// btnGen が押された
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, EventArgs e)
        {
            var tptn = txtPattern.Text.Replace("\r\n", "\n").Split('\n');

            int num;
            if (!int.TryParse(txtNum.Text, out num))
            {
                MessageBox.Show("Num is strange.", this.TITLE);
                return;
            }

            var ptn = new List<string>();
            foreach(var t in tptn)
            {
                string v = t.Trim();
                if (t.Length > 0)
                {
                    ptn.Add(t);
                }
            }
            if (ptn.Count == 0)
            {
                MessageBox.Show("No pattern.", this.TITLE);
                return;
            }

            txtCode.Text = generateProductCode(num, ptn);
        }

        /// <summary>
        /// コードを生成
        /// </summary>
        /// <param name="num"></param>
        /// <param name="pattern"></param>
        private string generateProductCode(int num, List<string> pattern)
        {
            var tret = new List<string>();

            for (int i = 0; i < num; i++)
            {
                string ptr = pattern[rnd.Next(pattern.Count)];  // パターン決定
                int len = ptr.Length;

                var t = "";
                for (int j = 0; j < len; j++)
                {
                    switch(ptr[j])
                    {
                        case 'A':
                            t += Convert.ToChar('A' + rnd.Next(26));
                            break;

                        case 'N':
                            t += rnd.Next(10).ToString();
                            break;

                        default:
                            t += ptr[j];
                            break;
                    }
                }

                // 重複チェック
                bool isDup = false;
                for(int j = tret.Count - 1; j >= 0; j--)
                {
                    if(tret[j] == t)
                    {
                        isDup = true;
                        break;
                    }
                }
                if (!isDup)
                {
                    tret.Add(t);
                }
                else
                {
                    i--;
                }
            }
            string ret = "";
            for (int i = 0; i < num; i++)
            {
                ret += tret[i] + "\r\n";
            }
            return ret.Trim();
        }

        /// <summary>
        /// btnCode をダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            txtCode.SelectAll();
        }
    }
}
