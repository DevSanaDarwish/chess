using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static Image _blackRookImage = Properties.Resources.Chess_rdt45_svg;
        public static Image _blackKnightImage = Properties.Resources.Chess_ndt45_svg;
        public static Image _blackBishopImage = Properties.Resources.Chess_bdt45_svg;
        public static Image _blackPawnImage = Properties.Resources.Chess_pdt45_svg;
        public static Image _whiteRookImage = Properties.Resources.Chess_rlt45_svg;
        public static Image _whiteKnightImage = Properties.Resources.Chess_nlt45_svg;
        public static Image _whiteBishopImage = Properties.Resources.Chess_blt45_svg;
        public static Image _whitePawnImage = Properties.Resources.Chess_plt45_svg;
        public static Image _blackKingImage = Properties.Resources.Chess_kdt45_svg;
        public static Image _whiteKingImage = Properties.Resources.Chess_klt45_svg;
        public static Image _blackQueenImage = Properties.Resources.Chess_qdt45_svg;
        public static Image _whiteQueenImage = Properties.Resources.Chess_qlt45_svg;
        

        TimeSpan _selectedTime = new TimeSpan(0, 10, 0);

        StringBuilder _formattedSelectedTime = new StringBuilder();

        public static string _tag = string.Empty;

        byte _whiteScore = 0,  _blackScore = 0;

        string _blackPlayer = "BLACK PLAYER", _whitePlayer = "WHITE PLAYER";

        bool _isFirstClick = true;

        enum enTime { OneMinute, ThreeMinutes, TenMinutes };

        enum enChessPieces { rook, knight, bishop, pawn, queen, king };

        public enum enColors { b, w };

        public enum enChoice { Black, White};
        public static enChoice _currentTurn;

        Guna2Button _selectedButton = null;
        public struct stPieceInfo
        {
            public string color { get; set; }
            public string name { get; set; }
            public string location { get; set; }

            public stPieceInfo(string color, string name, string location)
            {
                this.color = color;
                this.name = name;
                this.location = location;
            }
        }

        private void FillFirstAndSecondTagInfo(Guna2Button button, ref string firstInfo, ref string secondInfo)
        {
            if (button.Image == _whitePawnImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.pawn.ToString();
            }
                
            else if (button.Image == _whiteKnightImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.knight.ToString();

            }

            else if (button.Image == _whiteBishopImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.bishop.ToString();
            }

            else if (button.Image == _whiteKingImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.king.ToString();
            }
              
            else if (button.Image == _whiteQueenImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.queen.ToString();
            }           

            else if (button.Image == _whiteRookImage)
            {
                firstInfo = enColors.w.ToString();
                secondInfo = enChessPieces.rook.ToString();
            }

            else if(button.Image == _blackPawnImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.pawn.ToString();
            }
               
            else if (button.Image == _blackKnightImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.knight.ToString();
            }

            else if (button.Image == _blackBishopImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.bishop.ToString();
            }

            else if (button.Image == _blackKingImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.king.ToString();
            }

            else if (button.Image == _blackQueenImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.queen.ToString();
            }

            else if (button.Image == _blackRookImage)
            {
                firstInfo = enColors.b.ToString();
                secondInfo = enChessPieces.rook.ToString();
            }         
        }

        private void FillTagInfo(string thirdInfo, Guna2Button button)
        {
            string firstInfo = "", secondInfo = "";

            FillFirstAndSecondTagInfo(button, ref firstInfo, ref secondInfo);

            _tag = firstInfo + "," + secondInfo + "," + thirdInfo;
        }

        private stPieceInfo SeparateTagInfo(string thirdInfo, Guna2Button button)
        {
            FillTagInfo(thirdInfo, button);

            string[] tagInfo = _tag.Split(',');

            return new stPieceInfo(tagInfo[0], tagInfo[1], tagInfo[2]);
        }

        private bool IsValidSelection(Guna2Button button)
        {
            switch(_currentTurn)
            {
                case enChoice.White:
                    if (button.Image == _whitePawnImage)
                        return true;

                    return false;


                case enChoice.Black:
                    if (button.Image == _blackPawnImage)
                        return true;

                    return false;
            }

            return false;
        }


        private void Button_Click(object sender, EventArgs e)
        {
            Guna2Button clickedButton = sender as Guna2Button;

            if(_isFirstClick)
            {
                if(IsValidSelection(clickedButton))
                {
                    _selectedButton = clickedButton;
                    _isFirstClick = false;
                }
            }

            else
            {
                ChangeTurn();

                _isFirstClick = true;
            }
        }
        private void ChangeTurn()
        {
            if (_currentTurn == enChoice.White)
            {
                _currentTurn = enChoice.Black;
                lblPlayerTurn.Text = _blackPlayer;
                ResetTime(whiteTimer, lblWhiteTime);
                blackTimer.Start();
            }
            else
            {
                _currentTurn = enChoice.White;
                lblPlayerTurn.Text = _whitePlayer;              
                ResetTime(blackTimer, lblBlackTime);
                whiteTimer.Start();
            }
        }

        private void SetImagesInArrayOfButtons(Guna2Button[] buttons, Image image)
        {
            foreach(Guna2Button button in buttons)
            {
                button.Image = image;
            }
        }

        private void SetImageInButton(Guna2Button button, Image image)
        {
            button.Image = image;
        }
        private void SetDefaultImagesInButtons()
        {
            Guna2Button[] blackRooks = {btna8, btnh8};
            Guna2Button[] blackKnights = {btnb8, btng8};
            Guna2Button[] blackBishops = {btnc8, btnf8};
            Guna2Button[] blackPawns = {btna7, btnb7, btnc7, btnd7,
            btne7, btnf7, btng7, btnh7};

            Guna2Button[] whiteRooks = { btna1, btnh1 };
            Guna2Button[] whiteKnights = { btnb1, btng1 };
            Guna2Button[] whiteBishops = { btnc1, btnf1 };
            Guna2Button[] whitePawns = {btna2, btnb2, btnc2, btnd2,
            btne2, btnf2, btng2, btnh2};


            SetImagesInArrayOfButtons(blackRooks, _blackRookImage);
            SetImagesInArrayOfButtons(blackKnights, _blackKnightImage);
            SetImagesInArrayOfButtons(blackBishops, _blackBishopImage);
            SetImagesInArrayOfButtons(blackPawns, _blackPawnImage);

            SetImagesInArrayOfButtons(whiteRooks, _whiteRookImage);
            SetImagesInArrayOfButtons(whiteKnights, _whiteKnightImage);
            SetImagesInArrayOfButtons(whiteBishops, _whiteBishopImage);
            SetImagesInArrayOfButtons(whitePawns, _whitePawnImage);

            SetImageInButton(btne8, _blackKingImage);
            SetImageInButton(btne1, _whiteKingImage);
            SetImageInButton(btnd8, _blackQueenImage);
            SetImageInButton(btnd1, _whiteQueenImage);
        }

        private void RemoveImageFromButton(Guna2Button button)
        {
            button.Image = null;
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            SetDefaultImagesInButtons();

            cbTime.SelectedItem = "10 min";
            lblBlackTime.Text = GetTimeWord(enTime.TenMinutes);
            lblWhiteTime.Text = GetTimeWord(enTime.TenMinutes);
        }


        private string GetTimeWord(enTime time)
        {
            switch(time)
            {
                case enTime.TenMinutes:
                    return "10:00";

                case enTime.ThreeMinutes:
                    return "3:00";

                default:
                    return "1:00";
            }
        }

        private void StartTimer(Label lblTime, Timer timer)
        {
            TimeSpan oneSecond = new TimeSpan(0, 0, 1);

            _selectedTime -= oneSecond;

            _formattedSelectedTime.AppendFormat("{0:0}:{1:00}" ,_selectedTime.Minutes, _selectedTime.Seconds);

            lblTime.Text = _formattedSelectedTime.ToString();

            _formattedSelectedTime.Clear();


            if (_selectedTime <= TimeSpan.Zero)
            {
                TimeSpan zero = TimeSpan.Zero;

                _formattedSelectedTime.AppendFormat("{0:0}:{1:00}", zero.Minutes, zero.Seconds);

                lblTime.Text = _formattedSelectedTime.ToString();

                _formattedSelectedTime.Clear();

                timer.Stop();

                
                MessageBox.Show("Time's up .. you lost", "Notification");
            }
        }

        private void SetSelectedTime(ref enTime time)
        {
            if (cbTime.SelectedIndex == 0)
            {
                time = enTime.OneMinute;
                _selectedTime = new TimeSpan(0, 1, 0);
            }


            else if (cbTime.SelectedIndex == 1)
            {
                time = enTime.ThreeMinutes;
                _selectedTime = new TimeSpan(0, 3, 0);
            }


            else
            {
                time = enTime.TenMinutes;
                _selectedTime = new TimeSpan(0, 10, 0);
            }
        }
        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {


            //if (cbTime.SelectedIndex == 0)
            //{
            //    time = enTime.OneMinute;
            //    _selectedTime = new TimeSpan(0, 1, 0);
            //}


            //else if (cbTime.SelectedIndex == 1)
            //{
            //    time = enTime.ThreeMinutes;
            //    _selectedTime = new TimeSpan(0, 3, 0);
            //}


            //else
            //{
            //    time = enTime.TenMinutes;
            //    _selectedTime = new TimeSpan(0, 10, 0);
            //}


            enTime time = enTime.TenMinutes;

            SetSelectedTime( ref time);

            lblBlackTime.Text = GetTimeWord(time);
            lblWhiteTime.Text = GetTimeWord(time);
        }

        private void blackTimer_Tick(object sender, EventArgs e)
        {
            StartTimer(lblBlackTime, blackTimer);
        }

        private void whiteTimer_Tick(object sender, EventArgs e)
        {
            StartTimer(lblWhiteTime, whiteTimer);
        }

        private void StartGame()
        {
            whiteTimer.Start();

            _currentTurn = enChoice.White;

            lblPlayerTurn.Text = _whitePlayer;

            btnPlay.Enabled = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        public static bool IsCorrectColor(stPieceInfo piece, enColors color)
        {
            return (piece.color == color.ToString());
        }

        private void btna2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a2", btna2);

            clsPawn.MovePawnForOneStep(btna3, btna2, false, piece);

            //if (/*_currentTurn == enChoice.Black && */_tag == btnb3.Tag.ToString())
            //    clsPawn.CapturePawn(btnb3, btna2);

            Button_Click(sender, e);

        }
        
        private void btna3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a3", btna3);

            clsPawn.MovePawnForOneStep(btna2, btna3, true, piece);
            clsPawn.MovePawnForOneStep(btna4, btna3, false, piece);


            Button_Click(sender, e);
        }

        private void btnb1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b1", btnb1);

            clsPawn.MovePawnForOneStep(btnb2, btnb1, false, piece);
            
            //if (/*_currentTurn == enChoice.Black && */_tag == btna2.Tag.ToString())
            //    clsPawn.CapturePawn(btna2, btnb1);

            Button_Click(sender, e);

           
        }

        private void btnb3_Click(object sender, EventArgs e)
        {
           

            stPieceInfo piece = SeparateTagInfo("b3", btnb3);

            clsPawn.MovePawnForOneStep(btnb2, btnb3, true, piece);
            clsPawn.MovePawnForOneStep(btnb4, btnb3, false, piece);

            //if (/*_currentTurn == enChoice.White && */_tag == btna2.Tag.ToString())
            //    clsPawn.CapturePawn(btna2, btnb3);

            Button_Click(sender, e);
        }

        
        private void btnc3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c3", btnc3);

            clsPawn.MovePawnForOneStep(btnc2, btnc3, true, piece);
            clsPawn.MovePawnForOneStep(btnc4, btnc3, false, piece);

            
            Button_Click(sender, e);
        }

        private void btnd3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d3", btnd3);

            clsPawn.MovePawnForOneStep(btnd2, btnd3, true, piece);
            clsPawn.MovePawnForOneStep(btnd4, btnd3, false, piece);


            Button_Click(sender, e);
        }

        private void btne3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e3", btne3);

            clsPawn.MovePawnForOneStep(btne2, btne3, true, piece);
            clsPawn.MovePawnForOneStep(btne4, btne3, false, piece);

            
            Button_Click(sender, e);
        }

        private void btnf3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f3", btnf3);

            clsPawn.MovePawnForOneStep(btnf2, btnf3, true, piece);
            clsPawn.MovePawnForOneStep(btnf4, btnf3, false, piece);

           

            Button_Click(sender, e);
        }

        private void btng3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g3", btng3);

            clsPawn.MovePawnForOneStep(btng2, btng3, true, piece);
            clsPawn.MovePawnForOneStep(btng4, btng3, false, piece);


            Button_Click(sender, e);
        }

        private void btnh3_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h3", btnh3);

            clsPawn.MovePawnForOneStep(btnh2, btnh3, true, piece);
            clsPawn.MovePawnForOneStep(btnh4, btnh3, false, piece);

            

            Button_Click(sender, e);
        }
        
        private void btna4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a4", btna4);

            clsPawn.MovePawnForOneStepOrTwo(btna2, btna3, btna4, piece);
            clsPawn.MovePawnForOneStep(btna5, btna4, false, piece);

            

            Button_Click(sender, e );
        }

        private void btnb4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b4", btnb4);

            clsPawn.MovePawnForOneStepOrTwo(btnb2, btnb3, btnb4, piece);
            clsPawn.MovePawnForOneStep(btnb5, btnb4, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnc4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c4", btnc4);

            clsPawn.MovePawnForOneStepOrTwo(btnc2, btnc3, btnc4, piece);
            clsPawn.MovePawnForOneStep(btnc5, btnc4, false, piece);


            Button_Click(sender, e);
        }

        private void btnd4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d4", btnd4);

            clsPawn.MovePawnForOneStepOrTwo(btnd2, btnd3, btnd4, piece);
            clsPawn.MovePawnForOneStep(btnd5, btnd4, false, piece);

           

            Button_Click(sender, e);
        }

        private void btne4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e4", btne4);

            clsPawn.MovePawnForOneStepOrTwo(btne2, btne3, btne4, piece);
            clsPawn.MovePawnForOneStep(btne5, btne4, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnf4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f4", btnf4);

            clsPawn.MovePawnForOneStepOrTwo(btnf2, btnf3, btnf4, piece);
            clsPawn.MovePawnForOneStep(btnf5, btnf4, false, piece);

            

            Button_Click(sender, e);
        }

        private void btng4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g4", btng4);

            clsPawn.MovePawnForOneStepOrTwo(btng2, btng3, btng4, piece);
            clsPawn.MovePawnForOneStep(btng5, btng4, false, piece);


            Button_Click(sender, e);
        }

        private void btnh4_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h4", btnh4);

            clsPawn.MovePawnForOneStepOrTwo(btnh2, btnh3, btnh4, piece);
            clsPawn.MovePawnForOneStep(btnh5, btnh4, false, piece);


            Button_Click(sender, e);
        }

        private void btnb2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b2", btnb2);

            clsPawn.MovePawnForOneStep(btnb3, btnb2, false, piece);

            Button_Click(sender, e);
        }

        private void btnc2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c2", btnc2);

            clsPawn.MovePawnForOneStep(btnc3, btnc2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnd2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d2", btnd2);

            clsPawn.MovePawnForOneStep(btnd3, btnd2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btne2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e2", btne2);

            clsPawn.MovePawnForOneStep(btne3, btne2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnf2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f2", btnf2);

            clsPawn.MovePawnForOneStep(btnf3, btnf2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btng2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g2", btng2);

            clsPawn.MovePawnForOneStep(btng3, btng2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnh2_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h2", btnh2);

            clsPawn.MovePawnForOneStep(btnh3, btnh2, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnc1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c1", btnc1);

            clsPawn.MovePawnForOneStep(btnc2, btnc1, false, piece);

      

            Button_Click(sender, e);
        }

        private void btna1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a1", btna1);

            clsPawn.MovePawnForOneStep(btna2, btna1, false, piece);

            Button_Click(sender, e);
        }

        private void btng1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g1", btng1);

            clsPawn.MovePawnForOneStep(btng2, btng1, false, piece);

            

            Button_Click(sender, e);
        }

        private void btnd1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d1", btnd1);

            clsPawn.MovePawnForOneStep(btnd2, btnd1, false, piece);

           

            Button_Click(sender, e);
        }

        private void btne1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e1", btne1);

            clsPawn.MovePawnForOneStep(btne2, btne1, false, piece);

           

            Button_Click(sender, e);
        }

        private void btnf1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f1", btnf1);

            clsPawn.MovePawnForOneStep(btnf2, btnf1, false, piece);

           

            Button_Click(sender, e);
        }

        private void btnh1_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h1", btnh1);

            clsPawn.MovePawnForOneStep(btnh2, btnh1, false, piece);

            

            Button_Click(sender, e);
        }

        private void btna5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a5", btna5);

            clsPawn.MovePawnForOneStep(btna4, btna5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btna7, btna6, btna5, piece);


            Button_Click(sender, e);
        }

        private void btnb5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b5", btnb5);

            clsPawn.MovePawnForOneStep(btnb4, btnb5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btnb7, btnb6, btnb5, piece);


            Button_Click(sender, e);
        }

        private void btnc5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c5", btnc5);

            clsPawn.MovePawnForOneStep(btnc4, btnc5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btnc7, btnc6, btnc5, piece);


            Button_Click(sender, e);
        }

        private void btnd5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d5", btnd5);

            clsPawn.MovePawnForOneStep(btnd4, btnd5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btnd7, btnd6, btnd5, piece);


            Button_Click(sender, e);
        }

        private void btne5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e5", btne5);

            clsPawn.MovePawnForOneStep(btne4, btne5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btne7, btne6, btne5, piece);


            Button_Click(sender, e);
        }

        private void btnf5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f5", btnf5);

            clsPawn.MovePawnForOneStep(btnf4, btnf5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btnf7, btnf6, btnf5, piece);


            Button_Click(sender, e);
        }

        private void btng5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g5", btng5);

            clsPawn.MovePawnForOneStep(btng4, btng5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btng7, btng6, btng5, piece);


            Button_Click(sender, e);
        }

        private void btnh5_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h5", btnh5);

            clsPawn.MovePawnForOneStep(btnh4, btnh5, false, piece);
            clsPawn.MovePawnForOneStepOrTwo(btnh7, btnh6, btnh5, piece);


            Button_Click(sender, e);
        }

        

        private void btna6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a6", btna6);

            clsPawn.MovePawnForOneStep(btna5, btna6, false, piece);
            clsPawn.MovePawnForOneStep(btna7, btna6, true, piece);


            Button_Click(sender, e);

        }

        private void btnb6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b6", btnb6);

            clsPawn.MovePawnForOneStep(btnb5, btnb6, false, piece);
            clsPawn.MovePawnForOneStep(btnb7, btnb6, true, piece);


            Button_Click(sender, e);
        }

        private void btnc6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c6", btnc6);

            clsPawn.MovePawnForOneStep(btnc5, btnc6, false, piece);
            clsPawn.MovePawnForOneStep(btnc7, btnc6, true, piece);


            Button_Click(sender, e);
        }

        private void btnd6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d6", btnd6);

            clsPawn.MovePawnForOneStep(btnd5, btnd6, false, piece);
            clsPawn.MovePawnForOneStep(btnd7, btnd6, true, piece);


            Button_Click(sender, e);
        }

        private void btne6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e6", btne6);

            clsPawn.MovePawnForOneStep(btne5, btne6, false, piece);
            clsPawn.MovePawnForOneStep(btne7, btne6, true, piece);


            Button_Click(sender, e);
        }

        private void btnf6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f6", btnf6);

            clsPawn.MovePawnForOneStep(btnf5, btnf6, false, piece);
            clsPawn.MovePawnForOneStep(btnf7, btnf6, true, piece);


            Button_Click(sender, e);
        }

        private void btng6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g6", btng6);

            clsPawn.MovePawnForOneStep(btng5, btng6, false, piece);
            clsPawn.MovePawnForOneStep(btng7, btng6, true, piece);


            Button_Click(sender, e);
        }

        private void btnh6_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h6", btnh6);

            clsPawn.MovePawnForOneStep(btnh5, btnh6, false, piece);
            clsPawn.MovePawnForOneStep(btnh7, btnh6, true, piece);


            Button_Click(sender, e);
        }

        private void btna7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a7", btna7);

            clsPawn.MovePawnForOneStep(btna6, btna7, false, piece);


            Button_Click(sender, e);
        }

        private void btnb7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b7", btnb7);

            clsPawn.MovePawnForOneStep(btnb6, btnb7, false, piece);


            Button_Click(sender, e);
        }

        private void btnc7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c7", btnc7);

            clsPawn.MovePawnForOneStep(btnc6, btnc7, false, piece);


            Button_Click(sender, e);
        }

        private void btnd7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d7", btnd7);

            clsPawn.MovePawnForOneStep(btnd6, btnd7, false, piece);


            Button_Click(sender, e);
        }

        private void btne7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e7", btne7);

            clsPawn.MovePawnForOneStep(btne6, btne7, false, piece);


            Button_Click(sender, e);
        }

        private void btnf7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f7", btnf7);

            clsPawn.MovePawnForOneStep(btnf6, btnf7, false, piece);


            Button_Click(sender, e);
        }

        private void btng7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g7", btng7);

            clsPawn.MovePawnForOneStep(btng6, btng7, false, piece);


            Button_Click(sender, e);
        }

        private void btnh7_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h7", btnh7);

            clsPawn.MovePawnForOneStep(btnh6, btnh7, false, piece);


            Button_Click(sender, e);
        }

        private void btna8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("a8", btna8);

            clsPawn.MovePawnForOneStep(btna7, btna8, false, piece);


            Button_Click(sender, e);
        }

        private void btnb8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("b8", btnb8);

            clsPawn.MovePawnForOneStep(btnb7, btnb8, false, piece);


            Button_Click(sender, e);
        }

        private void btnc8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("c8", btnc8);

            clsPawn.MovePawnForOneStep(btnc7, btnc8, false, piece);


            Button_Click(sender, e);
        }

        private void btnd8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("d8", btnd8);

            clsPawn.MovePawnForOneStep(btnd7, btnd8, false, piece);


            Button_Click(sender, e);
        }

        private void btne8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("e8", btne8);

            clsPawn.MovePawnForOneStep(btne7, btne8, false, piece);


            Button_Click(sender, e);
        }

        private void btnf8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("f8", btnf8);

            clsPawn.MovePawnForOneStep(btnf7, btnf8, false, piece);


            Button_Click(sender, e);
        }

        private void btng8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("g8", btng8);

            clsPawn.MovePawnForOneStep(btng7, btng8, false, piece);


            Button_Click(sender, e);
        }

        private void btnh8_Click(object sender, EventArgs e)
        {
            stPieceInfo piece = SeparateTagInfo("h8", btnh8);

            clsPawn.MovePawnForOneStep(btnh7, btnh8, false, piece);


            Button_Click(sender, e);
        }

        private void ResetGame()
        {
            //Images
            SetDefaultImagesInButtons();

            RemoveImageFromButton(btna3);
            RemoveImageFromButton(btnb3);
            RemoveImageFromButton(btnc3);
            RemoveImageFromButton(btnd3);
            RemoveImageFromButton(btne3);
            RemoveImageFromButton(btnf3);
            RemoveImageFromButton(btng3);
            RemoveImageFromButton(btnh3);
            RemoveImageFromButton(btna4);
            RemoveImageFromButton(btnb4);
            RemoveImageFromButton(btnc4);
            RemoveImageFromButton(btnd4);
            RemoveImageFromButton(btne4);
            RemoveImageFromButton(btnf4);
            RemoveImageFromButton(btng4);
            RemoveImageFromButton(btnh4);
            RemoveImageFromButton(btna5);
            RemoveImageFromButton(btnb5);
            RemoveImageFromButton(btnc5);
            RemoveImageFromButton(btnd5);
            RemoveImageFromButton(btne5);
            RemoveImageFromButton(btnf5);
            RemoveImageFromButton(btng5);
            RemoveImageFromButton(btnh5);
            RemoveImageFromButton(btna6);
            RemoveImageFromButton(btnb6);
            RemoveImageFromButton(btnc6);
            RemoveImageFromButton(btnd6);
            RemoveImageFromButton(btne6);
            RemoveImageFromButton(btnf6);
            RemoveImageFromButton(btng6);
            RemoveImageFromButton(btnh6);

            //Timer
            //whiteTimer.Stop();
            //blackTimer.Stop();

            //cbTime.SelectedItem = "10 min";

            //_selectedTime = new TimeSpan(0, 10, 0);

            //lblBlackTime.Text = SetSelectedTime(enTime.TenMinutes);
            //lblWhiteTime.Text = SetSelectedTime(enTime.TenMinutes);

            ResetTime(whiteTimer, lblWhiteTime);
            ResetTime(blackTimer, lblBlackTime);

            //Score
            _whiteScore = 0;
            _blackScore = 0;

            lblBlackScore.Text = _blackScore.ToString();
            lblWhiteScore.Text = _whiteScore.ToString();

            btnPlay.Enabled = true;
        }

        private void ResetTime(Timer timer, Label lblTime)
        {
            timer.Stop();

            enTime time = enTime.TenMinutes;
            SetSelectedTime(ref time);

            //cbTime.SelectedItem = "10 min";
            //_selectedTime = new TimeSpan(0, 10, 0);

            lblTime.Text = GetTimeWord(time);
        }

       
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetGame();
        }
    }
}