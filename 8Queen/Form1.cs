using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _8Queen
{
    public partial class Form1 : Form
    {
        public Bitmap quanHau;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowTemplate.Height = 40;
            //quanHau = new Bitmap("QuanHau.bmp");

            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "1";
            dataGridView1.Columns[1].Name = "2";
            dataGridView1.Columns[2].Name = "3";
            dataGridView1.Columns[3].Name = "4";
            dataGridView1.Columns[4].Name = "5";
            dataGridView1.Columns[5].Name = "6";
            dataGridView1.Columns[6].Name = "7";
            dataGridView1.Columns[7].Name = "8";
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        static void NeighborCheck(int[] testState, out int hCount, out int[] bestNeighborState)
        {
            int[] neighborState = new int[8];
            int i;
            int t;
            int hNeighbor;
            int hNeighborBest = HeuristicTest(testState);
            int hCurrent = HeuristicTest(testState);
            int columnTemp = 0;
            int betterHCount = 0;
            bestNeighborState = new int[8];
            Array.Copy(testState, neighborState, 8);
            for (i = 0; i < 8; i++)
            {

                for (t = 0; t < 8; t++)
                {

                    columnTemp = neighborState[i];
                    neighborState[i] = t;

                    hNeighbor = HeuristicTest(neighborState);

                    if (hNeighbor < hNeighborBest)
                    {

                        Array.Copy(neighborState, bestNeighborState, 8);
                        hNeighborBest = hNeighbor;
                        betterHCount++;
                    }

                    neighborState[i] = columnTemp;
                }
            }
            hCount = betterHCount;
        }

        static int HeuristicTest(int[] testState)
        {
            int h = 0;

            //Horizontal Test

            int i;
            int c;

            for (i = 0; i < 8; i++)
            {
                for (c = 0; c < 8; c++)
                {
                    if (testState[i] == testState[c] && i != c)
                    {
                        h++;
                    }
                }

            }

            //Diagonal Test
            i = 0;
            int t = 0;


            for (i = 0; i < 8; i++)
            {

                for (t = 1; t < 8; t++)
                {

                    if (i + t < 8)
                    {
                        //down right
                        if ((testState[i] + t) == (testState[i + t]))
                        {
                            h++;
                        }


                        //up right
                        if ((testState[i] - t) == (testState[i + t]))
                        {
                            h++;
                        }

                    }


                    if (i - t >= 0)
                    {
                        //up left
                        if ((testState[i] - t) == (testState[i - t]))
                        {
                            h++;
                        }


                        //down left
                        if ((testState[i] + t) == (testState[i - t]))
                        {
                            h++;
                        }
                    }


                }


            }

            return h / 2;
        }

        static int[] RandomState()
        {

            Random random = new Random();

            int[] randomState = new int[8];

            for (int i = 0; i < 8; i++)
            {
                randomState[i] = random.Next(8);

            }

            return randomState;
        }

        //public void WriteState(int[] testState)
        //{
        //    int m;
        //    int k;
        //    Image[,] kq = new Image[8, 8];
        //    for (m = 0; m < 8; m++)
        //    {
        //        Image[] row = new Image[8];
        //        for (k = 0; k < 8; k++)
        //        {
        //            if (testState[k] == m)
        //            {
        //                row[k] = Image.FromFile(@"C:\Users\Thịnh\Desktop\ttnt\EightQueens\EightQueensGA\8Queen\qeen.png");
        //            }
        //            //else 
        //            //{
        //            //    kq[m, k];
        //            //    row[k] = kq[m, k];
        //            //}
        //        }
        //        dataGridView1.Rows.Add(row);
        //    }
        //}
        public void WriteState(int[] testState)
        {
            int m;
            int k;
            string[,] kq = new string[8, 8];
            for (m = 0; m < 8; m++)
            {
                string[] row = new string[8];

                for (k = 0; k < 8; k++)
                {
                    if (testState[k] == m)
                    {
                        row[k] = "Q";
                    }
                    //else
                    //{
                    //    kq[m, k];
                    //    row[k] = kq[m, k];
                    //}
                }
                dataGridView1.Rows.Add(row);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int[] currentState = new int[8];
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = true;
            int restarts = 0;
            int changes = 0;

            while (HeuristicTest(currentState) != 0)
            {
                int hBetter;
                int[] bestNeighborResult;

                NeighborCheck(currentState, out hBetter, out bestNeighborResult);

                if (hBetter > 0)
                {
                    dataGridView1.Rows.Clear();
                    WriteState(currentState);
                    txt_h.Text = HeuristicTest(currentState).ToString();
                    changes++;
                    currentState = bestNeighborResult;
                }

                else
                {
                    txt_h.Text = HeuristicTest(currentState).ToString();
                    dataGridView1.Rows.Clear();
                    WriteState(currentState);
                    restarts++;

                    currentState = RandomState();
                }
            }
            dataGridView1.Rows.Clear();
            txt_r.Text = restarts.ToString();
            WriteState(currentState);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            button3.Enabled = true;
            currentState = RandomState();
            WriteState(currentState);
            txt_h.Text = HeuristicTest(currentState).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = false;
            txt_r.Text = "0";
            txt_h.Text = HeuristicTest(currentState).ToString();
            dataGridView1.Rows.Clear();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 1; j < 8; j = j + 2)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.GreenYellow;
                    }
                }
                if (i % 2 != 0)
                {
                    for (int j = 0; j < 8; j = j + 2)
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.GreenYellow;
                    }
                }
            }
            //DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //String stringValue = e.Value as string;
            //cell.ToolTipText = stringValue;
            //e.Value = quanHau;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.No)
                e.Cancel = true;
        }

    }
}
