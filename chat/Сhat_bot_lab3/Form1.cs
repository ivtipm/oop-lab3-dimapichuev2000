﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Сhat_bot_lab3
{
    public partial class Form1 : Form
    {
        public Bot bot = new Bot();
        public Form1()
        {
            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox2.Select();
        }
        public void RestoreChat()// восстановление чата
        {
            for (int i = 0; i < bot.History.Count; i++)
            {
                textBox1.Text += bot.History[i] + Environment.NewLine;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                String[] userQuestion = textBox2.Text.Split(new String[] { "\r\n" },
                    StringSplitOptions.RemoveEmptyEntries);


                string message = userQuestion[0]; // для отправки боту

                userQuestion[0] = userQuestion[0].Insert(0,
                    "[" + DateTime.Now.ToString("HH:mm") + "] " + bot.UserName + ": ");

                bot.AddToHistory(userQuestion);

                textBox1.AppendText(userQuestion[0] + "\r\n");
                textBox2.Text = "";
                string[] botAnswer = new string[] { bot.Answ(message) };
                botAnswer[0] = botAnswer[0].Insert(0, "Бот: ");
                textBox1.AppendText(botAnswer[0] + Environment.NewLine);

                bot.AddToHistory(botAnswer);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Уверены," +
                "что хотите очистить диалог?", "Подтверждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                File.WriteAllText(bot.Path, string.Empty);
                bot.History.Clear();
                String[] date = new String[] {"Переписка от " +
                        DateTime.Now.ToString("dd.MM.yy"+ "\n")};
                bot.AddToHistory(date);
                textBox1.Text = date[0];
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                Button1_Click(button1, null);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }
    }
}
