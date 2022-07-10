using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;

namespace Roblox_Username_Generator
{
    public partial class Form1 : Form
    {
        private bool generate = false;
        private int failed = 0;
        private int success = 0;
        private int error = 0;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Roblox Username Generator {Failed: " + failed + " | Success: " + success + " | Errors: " + error + "}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generate = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generate = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                while (generate)
                {
                    int length = int.Parse(textBox1.Text);
                    string filePath = length.ToString() + "_Character Usernames.txt";

                    if (File.Exists(filePath))
                    {
                        string name = "";
                        name = "";
                        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
                        var random = new Random();
                        for (int i = 0; i < length; i++)
                        {
                            name = name + characters[random.Next(characters.Length)];
                        }
                        WebClient wc = new WebClient();
                        if (!wc.DownloadString("https://api.roblox.com/users/get-by-username?username=" + name).Contains("Id"))
                        {
                            success += 1;
                            File.AppendAllText(filePath, name + Environment.NewLine);
                            this.Text = "Roblox Username Generator {Failed: " + failed + " | Success: " + success + " | Errors: " + error + "}";
                        }
                        else
                        {
                            failed += 1;
                            this.Text = "Roblox Username Generator {Failed: " + failed + " | Success: " + success + " | Errors: " + error + "}";
                        }
                    }
                    else
                    {
                        File.Create(filePath);
                    }
                    Thread.Sleep(500);
                }
            }
            catch
            {
                error += 1;
                this.Text = "Roblox Username Generator {Failed: " + failed + " | Success: " + success + " | Errors: " + error + "}";
                Thread.Sleep(1000);
            }
        }
    }
}
