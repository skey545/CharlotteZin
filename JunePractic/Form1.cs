using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunePractic
{
    public partial class Form1 : Form
    {
        //List of icons
        private List<string> IconList = null;
        private Random rand = new Random();
        //Clicks
        Label First = null;
        Label Second = null;
        //Timer
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer() { Interval = 750, Enabled = false };
        public Form1()
        {
            InitializeComponent();
            //
            //Form Start
            this.StartPosition = FormStartPosition.CenterScreen;
            //table settings
            tableLayoutPanel1.BackColor = Color.CornflowerBlue;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            //
            LoadForm();
            timer.Tick += TimerTick;
        }

        protected void LoadForm()
        {

            //
            //Get The Map List
            IconList = new List<string>() { "!", "!", "N", "N", ",", ",", "k", "k", "b", "b", "v", "v", "w", "w", "z", "z" };
            //Generate CELLS on MAP
            if (tableLayoutPanel1.Controls.Count < 1)
                for (int i = 0; i < this.tableLayoutPanel1.ColumnCount * this.tableLayoutPanel1.RowCount; i++)
                {
                    Label newL = new Label { BackColor = Color.CornflowerBlue, AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Webdings", 75, FontStyle.Bold), Text = "c", ForeColor = Color.CornflowerBlue };
                    newL.Click += ClickToLabel;
                    this.tableLayoutPanel1.Controls.Add(newL);

                }



            //Generate Random Icons on map
            RandomIcons();
            //



        }
        private void TimerTick(object o, EventArgs e)
        {
            timer.Stop();
            First.ForeColor = First.BackColor;
            Second.ForeColor = Second.BackColor;

            First = null;
            Second = null;
        }
        private void ClickToLabel(object o, EventArgs e)
        {
            if (timer.Enabled == true) return;
            Label clicked = o as Label;

            if (clicked != null)
            {
                if (clicked.ForeColor == Color.Black) return;

                if (First == null)
                {
                    First = clicked;
                    First.ForeColor = Color.Black;
                    return;
                }

                Second = clicked;
                Second.ForeColor = Color.Black;

                CheckWinner();

                if (First.Text == Second.Text)
                {
                    First.BackColor = Color.Green;
                    Second.BackColor = Color.Green;
                    First = null;
                    Second = null;
                    return;
                }
                timer.Start();

            }


        }

        private void CheckWinner()
        {
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                Label curr = c as Label;
                if (curr != null)
                {
                    if (curr.BackColor == curr.ForeColor)
                    {
                        return;
                    }
                }
            }
            if (MessageBox.Show("You won! Restart the game?", "Caption", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) LoadForm();
            else Close();
        }
        private void RandomIcons()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null)
                {
                    int RandomNumber = rand.Next(IconList.Count);
                    label.Text = IconList[RandomNumber];
                    label.ForeColor = Color.CornflowerBlue;
                    label.BackColor = Color.CornflowerBlue;
                    IconList.RemoveAt(RandomNumber);

                }
            }
        }
    }
}
