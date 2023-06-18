using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EightQueensGA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SolveHillClimbing((int)numericUpDown1.Value);
        }

        const int BT_N = 8;
        public static int TOTAL_QUEENS = 8;
        int[][] HC_board = new int[8][];
        private int[] queenPositions = new int[8];
        void SolveHillClimbing(int count)
        {
            Boolean climb = true;
            int climbCount = 0;

            // 5 restarts
            while (climb)
            {
                // score to be compared against
                int localMin = h();
                Boolean best = false;
                // array to store best queen positions by row (array index is column)
                int[] bestQueenPositions = new int[8];

                // iterate through each column 
                for (int j = 0; j < HC_board.Count(); j++)
                {
                    Console.WriteLine("\nCOLUMN " + j + ":");
                    best = false;
                    //  iterate through each row
                    for (int i = 0; i < HC_board.Count(); i++)
                    {

                        // skip score calculated by original board
                        if (i != queenPositions[j])
                        {

                            // move queen 
                            moveQueen(i, j);
                            printBoard();
                            // calculate score, if best seen then store queen position
                            if (h() < localMin)
                            {
                                best = true;
                                localMin = h();
                                bestQueenPositions[j] = i;
                            }
                            // reset to original queen position
                            resetQueen(i, j);

                        }
                    }

                    // change 2 back to 1

                    resetBoard(j);
                    if (best)
                    {
                        // Neu tim duoc vi tri tot nhat, di chuyen queen toi vi tri nay
                        placeBestQueen(j, bestQueenPositions[j]);
                        printBoard();

                    }
                    else
                    {

                        printBoard();
                    }
                }

                // if score = 0, hill climbing has solved problem
                if (h() == 0)
                    climb = false;

                climbCount++;


                if (climbCount == count)
                {
                    //label8.Text = climbCount.ToString();
                    climb = false;
                }
                // break;

            }
        }

        private int[] generateQueens()
        {

            List<int> randomPos = new List<int>();

            Random r = new Random();
            for (int i = 0; i < TOTAL_QUEENS; i++)
            {
                randomPos.Add(r.Next(8));
            }

            int[] randomPositions = new int[TOTAL_QUEENS];

            for (int i = 0; i < randomPos.Count(); i++)
            {
                randomPositions[i] = randomPos.ElementAt(i);
            }

            return randomPositions;
        }
        public void placeQueens()
        {

            queenPositions = generateQueens();
            for (int i = 0; i < HC_board.Count(); i++)
            {
                HC_board[queenPositions[i]][i] = 1;
            }
        }

        public int h()
        {
            int totalPairs = 0;
            int Pairs_row = 0;
            // kiem tra tinh so cac cap tan cong nhau theo hang ngang
            for (int i = 0; i < HC_board.Count(); i++)
            {
                List<Boolean> pairs = new List<Boolean>();
                for (int j = 0; j < HC_board[i].Count(); j++)
                {
                    if (HC_board[i][j] == 1)
                    {
                        pairs.Add(true);
                    }
                }
                if (pairs.Count() != 0)
                    totalPairs = totalPairs + (pairs.Count() - 1);
            }
            Pairs_row = totalPairs;
            Console.WriteLine("Tong cac cap tan cong nhau hang ngang: " + Pairs_row);

            // kiem tra tinh so cac cap tan cong nhau theo duong cheo
            int pairs_dcc = checkDuongCheoChinh();
            Console.WriteLine("Tong cac cap tan cong nhau duong cheo chinh: " + pairs_dcc);
            //int pairs_dcp = checkDuongCheoPhu();
            //Console.WriteLine("Tong cac cap tan cong nhau duong cheo phu: " + pairs_dcp);
            Console.WriteLine("Heuristic: " + (totalPairs + pairs_dcc));
            Console.WriteLine("");
            return totalPairs + pairs_dcc;
        }

        // hàm tính đường chéo chính
        private int checkDuongCheoChinh()
        {

            int[] b1 = { HC_board[7][0] };
            int[] b2 = { HC_board[7][1], HC_board[6][0] };
            int[] b3 = { HC_board[7][2], HC_board[6][1], HC_board[5][0] };
            int[] b4 = { HC_board[7][3], HC_board[6][2], HC_board[5][1], HC_board[4][0] };
            int[] b5 = { HC_board[7][4], HC_board[6][3], HC_board[5][2], HC_board[4][1],
                HC_board[3][0] };
            int[] b6 = { HC_board[7][5], HC_board[6][4], HC_board[5][3], HC_board[4][2],
                HC_board[3][1], HC_board[2][0] };
            int[] b7 = { HC_board[7][6], HC_board[6][5], HC_board[5][4], HC_board[4][3],
                HC_board[3][2], HC_board[2][1], HC_board[1][0] };
            int[] b8 = { HC_board[7][7], HC_board[6][6], HC_board[5][5], HC_board[4][4],
                HC_board[3][3], HC_board[2][2], HC_board[1][1], HC_board[0][0] };
            int[] b9 = { HC_board[6][7], HC_board[5][6], HC_board[4][5], HC_board[3][4],
                HC_board[2][3], HC_board[1][2], HC_board[0][1] };
            int[] b10 = { HC_board[5][7], HC_board[4][6], HC_board[3][5], HC_board[2][4],
                HC_board[1][3], HC_board[0][2] };
            int[] b11 = { HC_board[4][7], HC_board[3][6], HC_board[2][5], HC_board[1][4],
                HC_board[0][3] };
            int[] b12 = { HC_board[3][7], HC_board[2][6], HC_board[1][5], HC_board[0][4] };
            int[] b13 = { HC_board[2][7], HC_board[1][6], HC_board[0][5] };
            int[] b14 = { HC_board[1][7], HC_board[0][6] };
            int[] b15 = { HC_board[0][7] };

            int totalPairs = 0;

            totalPairs += checkMirrorHorizontal(b1);
            totalPairs += checkMirrorHorizontal(b2);
            totalPairs += checkMirrorHorizontal(b3);
            totalPairs += checkMirrorHorizontal(b4);
            totalPairs += checkMirrorHorizontal(b5);
            totalPairs += checkMirrorHorizontal(b6);
            totalPairs += checkMirrorHorizontal(b7);
            totalPairs += checkMirrorHorizontal(b8);
            totalPairs += checkMirrorHorizontal(b9);
            totalPairs += checkMirrorHorizontal(b10);
            totalPairs += checkMirrorHorizontal(b11);
            totalPairs += checkMirrorHorizontal(b12);
            totalPairs += checkMirrorHorizontal(b13);
            totalPairs += checkMirrorHorizontal(b14);
            totalPairs += checkMirrorHorizontal(b15);

            return totalPairs;

        }

        // hàm tính đường chéo phụ
        private int checkDuongCheoPhu()
        {

            int[] b1 = { HC_board[0][0] };
            int[] b2 = { HC_board[1][0], HC_board[0][1] };
            int[] b3 = { HC_board[2][0], HC_board[1][1], HC_board[0][2] };
            int[] b4 = { HC_board[3][0], HC_board[2][1], HC_board[1][2], HC_board[0][3] };
            int[] b5 = { HC_board[4][0], HC_board[3][1], HC_board[2][2], HC_board[1][3],
                HC_board[0][4] };
            int[] b6 = { HC_board[5][0], HC_board[4][1], HC_board[3][2], HC_board[2][3],
                HC_board[1][4], HC_board[0][5] };
            int[] b7 = { HC_board[6][0], HC_board[5][1], HC_board[4][2], HC_board[3][3],
                HC_board[2][4], HC_board[1][5], HC_board[0][6] };
            int[] b8 = { HC_board[7][0], HC_board[6][1], HC_board[5][2], HC_board[4][3],
                HC_board[3][4], HC_board[2][5], HC_board[1][6], HC_board[0][7] };
            int[] b9 = { HC_board[7][1], HC_board[6][2], HC_board[5][3], HC_board[4][4],
                HC_board[3][5], HC_board[2][6], HC_board[0][7] };
            int[] b10 = { HC_board[7][2], HC_board[6][3], HC_board[5][4], HC_board[4][5],
                HC_board[3][6], HC_board[2][7] };
            int[] b11 = { HC_board[7][3], HC_board[6][4], HC_board[5][5], HC_board[4][6],
                HC_board[3][7] };
            int[] b12 = { HC_board[7][4], HC_board[6][5], HC_board[5][6], HC_board[4][7] };
            int[] b13 = { HC_board[7][5], HC_board[6][6], HC_board[5][7] };
            int[] b14 = { HC_board[7][6], HC_board[6][7] };
            int[] b15 = { HC_board[7][7] };

            int totalPairs = 0;

            totalPairs += checkMirrorHorizontal(b1);
            totalPairs += checkMirrorHorizontal(b2);
            totalPairs += checkMirrorHorizontal(b3);
            totalPairs += checkMirrorHorizontal(b4);
            totalPairs += checkMirrorHorizontal(b5);
            totalPairs += checkMirrorHorizontal(b6);
            totalPairs += checkMirrorHorizontal(b7);
            totalPairs += checkMirrorHorizontal(b8);
            totalPairs += checkMirrorHorizontal(b9);
            totalPairs += checkMirrorHorizontal(b10);
            totalPairs += checkMirrorHorizontal(b11);
            totalPairs += checkMirrorHorizontal(b12);
            totalPairs += checkMirrorHorizontal(b13);
            totalPairs += checkMirrorHorizontal(b14);
            totalPairs += checkMirrorHorizontal(b15);

            return totalPairs;

        }

        public void moveQueen(int row, int col)
        {
            // original queen will become a 2 and act as a marker
            HC_board[queenPositions[col]][col] = 2;

            HC_board[row][col] = 1;
        }


        private int checkMirrorHorizontal(int[] b)
        {

            int totalPairs = 0;

            List<Boolean> pairs = new List<Boolean>();
            for (int i = 0; i < b.Count(); i++)
            {
                if (b[i] == 1)
                    pairs.Add(true);

            }

            if (pairs.Count() != 0)
                totalPairs = (pairs.Count() - 1);

            return totalPairs;
        }

        public void resetQueen(int row, int col)
        {

            if (HC_board[row][col] == 1)
                HC_board[row][col] = 0;
        }

        public void resetBoard(int col)
        {

            for (int i = 0; i < HC_board.Count(); i++)
            {
                if (HC_board[i][col] == 2)
                    HC_board[i][col] = 1;
            }
        }

        public void placeBestQueen(int col, int queenPos)
        {

            for (int i = 0; i < HC_board.Count(); i++)
            {
                if (HC_board[i][col] == 1)
                    HC_board[i][col] = 2;

            }
            HC_board[queenPos][col] = 1;
            for (int i = 0; i < HC_board.Count(); i++)
            {
                if (HC_board[i][col] == 2)
                    HC_board[i][col] = 0;

            }
        }

        public void printBoard()
        {

            for (int i = 0; i < HC_board.Count(); i++)
            {
                for (int j = 0; j < HC_board[i].Count(); j++)
                {
                    Console.Write(HC_board[i][j] + " ");
                }
                Console.WriteLine();
            }
            int[] sol = new int[BT_N];
            for (int p = 0; p < BT_N; p++)
            {
                for (int q = 0; q < BT_N; q++)
                {
                    if (HC_board[p][q] == 1)
                    {
                        sol[p] = q;
                    }
                }
                board1.Genes = sol;
            }
        }

        private void btn_xepco_Click(object sender, EventArgs e)
        {
            Console.WriteLine("\nKhoi tao ban co:");
            btnStart.Enabled = true;
            for (int i = 0; i < 8; i++)
            {
                HC_board[i] = new int[8];
            }
            // randoml vị trí quân hậu
            placeQueens();
            printBoard();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

    }

}


