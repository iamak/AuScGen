// ***********************************************************************
// <copyright file="Mouse.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Mouse class</summary>
// ***********************************************************************

using System.Drawing;
using MouseKeyboardLibrary;

namespace AuScGen.CommonUtilityPlugin
{
    /// <summary>
    /// Mouse
    /// </summary>
    public class Mouse
    {
        /// <summary>
        /// Wheels up.
        /// </summary>
        /// <param name="delta">The delta.</param>
        public static void WheelUp(int delta)
        {
            MouseSimulator.MouseWheel(delta);
        }

        /// <summary>
        /// Hovers the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public static void Hover(Point point)
        {
            MouseSimulator.Position = point;
        }
    }
}
