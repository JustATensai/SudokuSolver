using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class Form1 : Form
    {
        int [,] Numbers = new int[9,9];

        //List<int> Kalanlar;

        void CreateArray()
        {
            CreateRow(textBox1.Text, 1);
            CreateRow(textBox2.Text, 2);
            CreateRow(textBox3.Text, 3);
            CreateRow(textBox4.Text, 4);
            CreateRow(textBox5.Text, 5);
            CreateRow(textBox6.Text, 6);
            CreateRow(textBox7.Text, 7);
            CreateRow(textBox8.Text, 8);
            CreateRow(textBox9.Text, 9);
        }

        void CreateRow(string text, int row)
        {
            row -= 1;
            for (int i = 0; i < 9; i++)
            {
                Numbers[row, i] = Int32.Parse(text.Substring(i, 1));
            }
        }

        void SolveSudoku()
        {
            bool isItDone = true;

            for (int i=0; i<9; i++)
            {
                for (int j=0; j<9; j++)
                {
                    int NumberToCheck = Numbers[i, j];
                    
                    if (NumberToCheck == 0)
                    {
                        int NumberToWrite = Check(i, j);

                        if (NumberToWrite != 0)
                        {
                            Numbers[i, j] = NumberToWrite;
                        }
                    }
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int NumberToCheck = Numbers[i, j];

                    if (NumberToCheck == 0)
                    {
                        isItDone = false;
                        break;
                    }
                }
            }

            if (isItDone == false)
            {
                SolveSudoku();
            }

            else //Solved
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        int NumberToCheck = Numbers[i, j];

                        richTextBox1.Text += NumberToCheck + "  ";
                    }

                    richTextBox1.Text += "\n";
                }
            }
        }

        void BruteSolve()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int NumberToCheck = Numbers[i, j];

                    if (NumberToCheck == 0)
                    {
                        for (int k=0; k<9; k++)
                        {
                            if (BruteCheck(i, j, k+1))
                            {
                                Numbers[i, j] = k+1;
                                BruteSolve();
                                Numbers[i, j] = 0;
                            }
                        }

                        return;
                    }
                }
            }

            //Solved
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int NumberToCheck = Numbers[i, j];

                    richTextBox1.Text += NumberToCheck + "  ";
                }

                richTextBox1.Text += "\n";
            }
        }

        bool BruteCheck(int row, int col, int number)
        {
            //Checking row
            for (int i = 0; i < 9; i++)
            {
                if (Numbers[row, i] == number)
                {
                    return false;
                }
            }

            //Checking column
            for (int i = 0; i < 9; i++)
            {
                if (Numbers[i, col] == number)
                {
                    return false;
                }
            }

            //Checking 3x3
            float here1 = row / 3;
            float here2 = col / 3;
            int newRow = (int)Math.Floor(here1);
            int newCol = (int)Math.Floor(here2);
            for (int i=0; i<3; i++)
            {
                for (int j=0; j<3; j++)
                {
                    if (Numbers[(newRow*3)+i, (newCol * 3) + j] == number)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        int Check(int row, int col)
        {
            List<int> Kalanlar = new List<int>();
            for (int i=1; i<10; i++)
            {
                Kalanlar.Add(i);
            }

            //checking row
            for (int i=0; i<9; i++)
            {
               if (Numbers[row, i] != 0)
               {
                    Kalanlar.Remove(Numbers[row, i]);
               }
            }

            //checking column
            for (int i = 0; i < 9; i++)
            {
                if (Numbers[i, col] != 0)
                {
                    Kalanlar.Remove(Numbers[i, col]);
                }
            }

            //checking 3x3
            if (row < 3)
            {
                if (col < 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j=0; j<3; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
            }

            else if (row < 6)
            {
                if (col < 3)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 3; i < 6; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
            }

            else
            {
                if (col < 3)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else if (col < 6)
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 3; j < 6; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 6; i < 9; i++)
                    {
                        for (int j = 6; j < 9; j++)
                        {
                            if (Numbers[i, j] != 0)
                            {
                                Kalanlar.Remove(Numbers[i, j]);
                            }
                        }
                    }
                }
            }

            if (Kalanlar.Count > 1)
            {
                return 0;
            }
            else
            {
                return Kalanlar.FirstOrDefault();
            }
        }

        void ResetEverything()
        {
            Numbers = new int[9, 9];

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";

            richTextBox1.Text = "";
        }

        bool CheckIfInputsAreRight()
        {

            if (textBox1.Text.Length != 9)
            {
                MessageBox.Show("1. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox2.Text.Length != 9)
            {
                MessageBox.Show("2. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox3.Text.Length != 9)
            {
                MessageBox.Show("3. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox4.Text.Length != 9)
            {
                MessageBox.Show("4. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox5.Text.Length != 9)
            {
                MessageBox.Show("5. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox6.Text.Length != 9)
            {
                MessageBox.Show("6. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox7.Text.Length != 9)
            {
                MessageBox.Show("7. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox8.Text.Length != 9)
            {
                MessageBox.Show("8. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            if (textBox9.Text.Length != 9)
            {
                MessageBox.Show("9. satırda hata var.",
                    "Sudoku Solver v1.0",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool OkToGo = CheckIfInputsAreRight();

            if (OkToGo == true)
            {
                CreateArray();
                BruteSolve();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ResetEverything();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
