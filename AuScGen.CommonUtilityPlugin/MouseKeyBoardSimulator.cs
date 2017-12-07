// ***********************************************************************
// <copyright file="MouseKeyBoardSimulator.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>MouseKeyBoardSimulator class</summary>
// ***********************************************************************
using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows.Forms;
using Framework;

namespace AuScGen.CommonUtilityPlugin
{
	/// <summary>
	/// MouseKeyBoardSimulator
	/// </summary>
	[Export(typeof(IPlugin))]
    public class MouseKeyBoardSimulator : IPlugin
    {
        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="value">The value.</param>
        public static void SetText(string value)
        {
            //Modified all chars to lower case....to avoid writing code for Upper cases ....
            char[] chars = value.ToLower(CultureInfo.CurrentCulture).ToCharArray();
            foreach (char ch in chars)
            {
                if (char.IsUpper(ch))
                {
                    MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.CapsLock);
                }
                MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Clear);
                switch (ch)
                {
                    case 'a':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.A);
                        break;
                    case 'b':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.B);
                        break;
                    case 'c':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.C);
                        break;
                    case 'd':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.D);
                        break;
                    case 'e':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.E);
                        break;
                    case 'f':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.F);
                        break;
                    case 'g':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.G);
                        break;
                    case 'h':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.H);
                        break;
                    case 'i':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.I);
                        break;
                    case 'j':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.J);
                        break;
                    case 'k':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.K);
                        break;
                    case 'l':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.L);
                        break;
                    case 'm':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.M);
                        break;
                    case 'n':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.N);
                        break;
                    case 'o':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.O);
                        break;
                    case 'p':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.P);
                        break;
                    case 'q':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Q);
                        break;
                    case 'r':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.R);
                        break;
                    case 's':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.S);
                        break;
                    case 't':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.T);
                        break;
                    case 'u':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.U);
                        break;
                    case 'v':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.V);
                        break;
                    case 'w':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.W);
                        break;
                    case 'x':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.X);
                        break;
                    case 'y':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Y);
                        break;
                    case 'z':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Z);
                        break;
                    case '.':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.OemPeriod);
                        break;
                    default:
                        break;
                }
                if (char.IsUpper(ch))
                {
                    MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.CapsLock);
                }
            }
            //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.A);
        }

        /// <summary>
        /// Sets the numeric.
        /// </summary>
        /// <param name="numeric">The numeric.</param>
        public static void SetNumeric(string numeric)
        {
            char[] Numericchars = numeric.ToCharArray();
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.Clear);
            foreach (char ch in Numericchars)
            {
                switch (ch)
                {
                    case '0':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad0);
                        break;
                    case '1':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad1);
                        break;
                    case '2':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad2);
                        break;
                    case '3':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad3);
                        break;
                    case '4':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad4);
                        break;
                    case '5':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad5);
                        break;
                    case '6':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad6);
                        break;
                    case '7':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad7);
                        break;
                    case '8':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad8);
                        break;
                    case '9':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.NumPad9);
                        break;
                    case '.':
                        MouseKeyboardLibrary.KeyboardSimulator.KeyPress(Keys.OemPeriod);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Keys the press.
        /// </summary>
        /// <param name="key">The key.</param>
        public void KeyPress(Keys key)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyPress(key);
        }

        /// <summary>
        /// Keys down.
        /// </summary>
        /// <param name="key">The key.</param>
        public void KeyDown(Keys key)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyDown(key);
        }

        /// <summary>
        /// Keys up.
        /// </summary>
        /// <param name="key">The key.</param>
        public void KeyUp(Keys key)
        {
            MouseKeyboardLibrary.KeyboardSimulator.KeyUp(key);
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                return "Keyboard Simulator";
            }
            set
            {
                Description = value;
            }
        }
    }
}
