// ***********************************************************************
// <copyright file="Actions.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>Actions class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIAccess.WebControls;
using WebDriverWrapper;

namespace UIAccess
{
    /// <summary>
    /// Actions
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// The this control access
        /// </summary>
        private ControlAccess thisControlAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="Actions"/> class.
        /// </summary>
        /// <param name="controlAccess">The control access.</param>
        public Actions(ControlAccess controlAccess)
        {
            thisControlAccess = controlAccess;
        }

        /// <summary>
        /// Moves to element.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void MoveToElement(WebControl webElement)
        {
           
            thisControlAccess.Action.MoveToElement(webElement.ControlObject);
            
        }

        /// <summary>
        /// Moves to element.
        /// </summary>
        /// <param name="offsetX">The off set x.</param>
        /// <param name="offsetY">The off set y.</param>
        public void MoveToElement(int offsetX, int offsetY)
        {
            thisControlAccess.Action.MoveToElement(offsetX, offsetY);
        }

        /// <summary>
        /// Drags the drop.
        /// </summary>
        /// <param name="target">The target.</param>
        public void DragDrop(WebControl target)
        {
            thisControlAccess.Action.DragDrop(target.ControlObject);
        }

        /// <summary>
        /// Drags the drop to offset.
        /// </summary>
        /// <param name="offsetX">The off set x.</param>
        /// <param name="offsetY">The off set y.</param>
        public void DragDropToOffset(int offsetX, int offsetY)
        {
            thisControlAccess.Action.DragDropToOffset(offsetX, offsetY);
        }

        /// <summary>
        /// Natives the select.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void NativeSelect(WebControl webElement)
        {
            thisControlAccess.Action.NativeSelect(webElement.ControlObject);
        }

        /// <summary>
        /// Sends the keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void SendKeys(string keys)
        {
            thisControlAccess.Action.SendKeys(keys);
        }

        /// <summary>
        /// Sends the keys.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <param name="keys">The keys.</param>
        public void SendKeys(WebControl webElement, string keys)
        {
            thisControlAccess.Action.SendKeys(webElement.ControlObject, keys);
        }

        /// <summary>
        /// Clicks the and hold.
        /// </summary>
        public void ClickAndHold()
        {
            thisControlAccess.Action.ClickAndHold();
        }

        /// <summary>
        /// Clicks the and hold.
        /// </summary>
        /// <param name="webElement">The web element.</param>
        public void ClickAndHold(WebControl webElement)
        {
            thisControlAccess.Action.ClickAndHold(webElement.ControlObject);
        }

        /// <summary>
        /// Moves the by offset.
        /// </summary>
        /// <param name="offsetX">The x offset.</param>
        /// <param name="offsetY">The y offset.</param>
        public void MoveByOffset(int offsetX, int offsetY)
        {
            thisControlAccess.Action.MoveByOffset(offsetX, offsetY);
        }

    }
}
