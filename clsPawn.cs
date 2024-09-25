using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chess.Form1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Chess
{
    public class clsPawn
    {
        public static bool IsValidMoveForWhitePawn(Guna2Button previousPressedButton, Guna2Button targetButton, bool isInitialMove)
        {
            int buttonWidth = 100, buttonHeight = 101;

            int moveX = Math.Abs(targetButton.Location.X - previousPressedButton.Location.X) / buttonWidth;
            int moveY = Math.Abs(targetButton.Location.Y - previousPressedButton.Location.Y) / buttonHeight;

            if (isInitialMove)
                return (moveX == 0 && (moveY == 1 || moveY == 2));

            return (moveX == 0 && moveY == 1);
        }

        public static bool IsValidMoveForBlackPawn(Guna2Button previousPressedButton, Guna2Button targetButton, bool isInitialMove)
        {
            int buttonWidth = 100, buttonHeight = 101;

            int moveX = Math.Abs(targetButton.Location.X - previousPressedButton.Location.X) / buttonWidth;
            int moveY = (previousPressedButton.Location.Y - targetButton.Location.Y) / buttonHeight;

            if (isInitialMove)
                return (moveX == 0 && (moveY == -1 || moveY == -2));

            return (moveX == 0 && moveY == -1);
        }

        private static bool CanMove(Guna2Button currentButton)
        {
            return (currentButton.Tag.ToString() == "");
        }

        public static void IntroducingThePawnOneStepForward(Guna2Button previousPressedButton, Guna2Button currentButton, stPieceInfo piece)
        {
            if (IsCorrectColor(piece, enColors.w))
            {
                currentButton.Image = previousPressedButton.Image;
                previousPressedButton.Image = null;
            }

            else if (IsCorrectColor(piece, enColors.b))
            {
                currentButton.Image = previousPressedButton.Image;
                previousPressedButton.Image = null;
            }


            //targetButton.Image = previousPressedButton.Image;
            //previousPressedButton.Image = null;
        }

        public static void MovePawnForOneStep(Guna2Button previousPressedButton, Guna2Button currentButton, bool isInitialMove, stPieceInfo previousPiece)
        {
            if (CanMove(currentButton) && _tag == previousPressedButton.Tag.ToString())
            {
                if (IsCorrectColor(previousPiece, enColors.w) && IsValidMoveForWhitePawn(previousPressedButton, currentButton, isInitialMove))
                {
                    IntroducingThePawnOneStepForward(previousPressedButton, currentButton, previousPiece);
                }

                else if (IsCorrectColor(previousPiece, enColors.b) && IsValidMoveForBlackPawn(previousPressedButton, currentButton, isInitialMove))
                {
                    IntroducingThePawnOneStepForward(previousPressedButton, currentButton, previousPiece);
                }

                else if (IsValidCaptureMove(previousPressedButton, currentButton))
                {
                    IntroducingThePawnOneStepForward(previousPressedButton, currentButton, previousPiece);
                }
            }


            //correct method
            //if (CanMove(currentButton) && Form1._tag == previousPressedButton.Tag.ToString())
            //{
            //    if (IsValidMoveForWhitePawn(previousPressedButton, currentButton, isInitialMove))
            //        IntroducingThePawnOneStepForward(previousPressedButton, currentButton);
            //}


            //if (CanMove(currentButton) && Form1._tag == previousPressedButton.Tag.ToString())
            //{
            //    if (Form1.IsCorrectImage(previousPressedButton, Form1._whitePawnImage) &&
            //        IsValidMoveForPawn(previousPressedButton, currentButton, isInitialMove))
            //    {
            //        IntroducingThePawnOneStepForward(previousPressedButton, currentButton);
            //    }
            //    else if (Form1.IsCorrectImage(previousPressedButton, Form1._blackPawnImage) &&
            //             IsValidMoveForBlackPawn(previousPressedButton, currentButton, isInitialMove))
            //    {
            //        IntroducingThePawnOneStepForward(previousPressedButton, currentButton);
            //    }
            //}
        }

        public static void MovePawnForOneStepOrTwo(Guna2Button previousPressedButtonInRow2, Guna2Button previousPressedButtonInRow3, Guna2Button currentButton, stPieceInfo previousPiece)
        {
            if (CanMove(currentButton) && (_tag == previousPressedButtonInRow2.Tag.ToString() || _tag == previousPressedButtonInRow3.Tag.ToString()))
            {
                if (IsValidMoveForWhitePawn(previousPressedButtonInRow3, currentButton, false))
                {
                    IntroducingThePawnOneStepForward(previousPressedButtonInRow3, currentButton, previousPiece);

                    return;
                }

                if (IsValidMoveForWhitePawn(previousPressedButtonInRow2, currentButton, true))
                {
                    IntroducingThePawnOneStepForward(previousPressedButtonInRow2, currentButton, previousPiece);

                    return;
                }
            }
        }

        private static bool IsValidCaptureMove(Guna2Button attackerButton, Guna2Button targetButton)
        {
            int buttonWidth = 100, buttonHeight = 101;

            int moveX = (targetButton.Location.X - attackerButton.Location.X) / buttonWidth;
            int moveY = (targetButton.Location.Y - attackerButton.Location.Y) / buttonHeight;

            if (targetButton.Image == null || attackerButton.Image == targetButton.Image)
                return false;


            if (_currentTurn == enChoice.White)
            {
                if (ReferenceEquals(targetButton.Image, _whitePawnImage))
                    return false;
            }
                

            return (Math.Abs(moveX) == 1 && Math.Abs(moveY) == 1);
        }

        public static void CapturePawn(Guna2Button attackerButton, Guna2Button targetButton)
        {
            if (IsValidCaptureMove(attackerButton, targetButton))
            {
                targetButton.Image = attackerButton.Image;
                attackerButton.Image = null;
            }
        }
    }
}
