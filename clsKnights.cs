using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class clsKnights
    {

        public bool IsValidMoveForKnight(Guna2Button previousPressedButton, Guna2Button currentButton)
        {

            sbyte buttonWidth = 100, buttonHeight = 101;

            sbyte moveX = (sbyte)Convert.ToByte((sbyte)(currentButton.Location.X -
                previousPressedButton.Location.X) / buttonWidth);


            sbyte moveY = (sbyte)Convert.ToByte((sbyte)(currentButton.Location.Y -
                previousPressedButton.Location.Y) / buttonHeight);


            return (Math.Abs(moveX) == 2 && Math.Abs(moveY) == 1) || (Math.Abs(moveX) == 1 && Math.Abs(moveY) == 2);

            

            //return ((moveX == 1 || moveX == -1 || moveX == 2 || moveX == -2) &&
            //    (moveY == 1 || moveY == -1 || moveY == 2 || moveY == -2));


            //return ((moveX == 1 || moveX == -1 || moveX == 2 || moveX == -2) &&
            //            (moveY == 1 || moveY == 2));
        }
    }
}
