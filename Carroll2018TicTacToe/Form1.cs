using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Carroll2018TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            //this.KeyPress +=
                //new KeyPressEventHandler(Form1_KeyPress);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            designButtons();
            resetBoard();
        }

        //void Form1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar >= 48 && e.KeyChar <= 57)
        //    {
        //        MessageBox.Show("Form.KeyPress: '" +
        //            e.KeyChar.ToString() + "' pressed.");
        //        switch (e.KeyChar)
        //        {
        //            case (char)49:
        //            case (char)52:
        //            case (char)55:
        //                MessageBox.Show("Form.KeyPress: '" +
        //                    e.KeyChar.ToString() + "' consumed.");
        //                e.Handled = true;
        //                break;
        //        }
        //    }
        //}

        // 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    this.Left -= 20;
                    return true;

                case Keys.Right:
                    this.Left += 20;
                    return true;

                case Keys.Up:
                    this.Top -= 20;
                    return true;

                case Keys.Down:
                    this.Top += 20;
                    return true;


            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        string turn = "X";
        bool aiEnabled = false;
        int p1score = 0;
        int p2score = 0;
        bool gameFinished = false;
        bool draw = false;
        Random rnd = new Random();
        Button[] highlightList = new Button[3];
        

        private void designButtons()
        {
            foreach (Button b in Controls.OfType<Button>())
            {
                if (b.Tag.ToString() != "nongame")
                {
                    b.FlatStyle = FlatStyle.Flat;
                    b.FlatAppearance.BorderSize = 0;
                    b.Height = 75;
                    b.Font = statusBox.Font;
                    b.ForeColor = statusBox.ForeColor;
                }
            }
        }

        private void resetBoard() 

        {
            turn = "X";
            foreach (Button b in Controls.OfType<Button>())
            {
                if (b.Tag.ToString() != "nongame")
                {

                    b.Text = "";
                    statusBox.Text = "";
                    if (b.Tag.ToString() == "1" || b.Tag.ToString() == "3" || b.Tag.ToString() == "5" || b.Tag.ToString() == "7" || b.Tag.ToString() == "9")
                    {
                        b.BackColor = Color.FromArgb(255, 105, 105, 105);
                    } else if (b.Tag.ToString() == "2" || b.Tag.ToString() == "4" || b.Tag.ToString() == "6" || b.Tag.ToString() == "8" )
                    {
                        b.BackColor = Color.FromArgb(255, 225, 225, 225);
                    }
                }
            }
        }

        private void buttonsEnabled(bool status)
        {
            foreach (Button b in Controls.OfType<Button>())
            {
                if (b.Tag.ToString() != "nongame")
                {

                    b.Enabled = status;
                }
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {

            statusBox.Text = "It's " + turn + "'s turn.";

            if (gameFinished)
            {
                resetBoard();
                gameFinished = false;
                return;
            }

            // Cast the 'sender' as a button
            Button sentButton = (Button)sender;

            if (sentButton.Text == "")
            {
                // set button contents to turn
                

                if (turn == "X")
                { turn = "O"; }
                else if (turn == "O")
                { turn = "X"; }
                sentButton.Text = turn;



                if (checkWin("X"))
                {
                    statusBox.Text = "X has won!!";
                    p2score += 1;
                    gameFinished = true;
                    draw = false;

                }
                else if (checkWin("O"))
                {
                    statusBox.Text = "O has won!!";
                    p1score += 1;
                    gameFinished = true;
                    draw = false;
                } else if (checkDraw())
                {
                    statusBox.Text = "It's a Draw!";
                    gameFinished = true;
                    draw = true;
                }

                if (gameFinished)
                {
                    scoreBox.Text = $"O: {p1score.ToString()}  |  X: {p2score.ToString()}";
                    if (!draw)
                    {
                        foreach (Button b in highlightList)
                        {
                            b.BackColor = Color.LightGreen;
                        }
                    }
                }

                if (turn == "O" && aiEnabled && !gameFinished)
                {
                    dumbAI();
                }
            }

        }

        private void playerChoice(object sender, EventArgs e)
        {
            // Cast the 'sender' as a button
            Button sentButton = (Button)sender;

            if (sentButton.Text == "1P")
            {
                aiEnabled = true;
            } else
            {
                aiEnabled = false;
            }

            resetBoard();


            onePlayer.Height = 45;
            twoPlayer.Height = 45;
            onePlayer.Top = 285;
            twoPlayer.Top = 285;

        }

        private void dumbAI()
        {
            Button[] bList = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            int i = rnd.Next(0,9);
            Button cb = bList[i];
            if (cb.Text == "")
            {
                cb.PerformClick();
                return;
            } else
            {
                dumbAI();
            }
        }

        private bool checkWin(string piece)
        {
            bool win = false;
            Array.Clear(highlightList, 0, highlightList.Length);

            if (button1.Text == piece && button2.Text == piece && button3.Text == piece)
            {
                highlightList[0] = button1;
                highlightList[1] = button2;
                highlightList[2] = button3;

                win = true;
            }
            else if (button4.Text == piece && button5.Text == piece && button6.Text == piece)
            {
                highlightList[0] = button4;
                highlightList[1] = button5;
                highlightList[2] = button6;

                win = true;
            }
            else if (button7.Text == piece && button8.Text == piece && button9.Text == piece)
            {
                highlightList[0] = button7;
                highlightList[1] = button8;
                highlightList[2] = button9;

                win = true;
            }

            if (button1.Text == piece && button4.Text == piece && button7.Text == piece)
            {
                highlightList[0] = button1;
                highlightList[1] = button4;
                highlightList[2] = button7;

                win = true;
            }
            else if (button2.Text == piece && button5.Text == piece && button8.Text == piece)
            {
                highlightList[0] = button2;
                highlightList[1] = button5;
                highlightList[2] = button8;

                win = true;
            }
            else if (button3.Text == piece && button6.Text == piece && button9.Text == piece)
            {
                highlightList[0] = button3;
                highlightList[1] = button6;
                highlightList[2] = button9;

                win = true;
            }

            if (button1.Text == piece && button5.Text == piece && button9.Text == piece)
            {
                highlightList[0] = button1;
                highlightList[1] = button5;
                highlightList[2] = button9;

                win = true;
            }
            else if (button7.Text == piece && button5.Text == piece && button3.Text == piece)
            {
                highlightList[0] = button7;
                highlightList[1] = button5;
                highlightList[2] = button3;

                win = true;
            }


            return win;
        }

        private bool checkDraw()
        {
            Button[] bList = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
            int filledCount = 0;
            foreach (Button b in bList)
            {
                if (b.Text != "")
                {
                    filledCount += 1;
                }
            }
            if (filledCount == 9)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
