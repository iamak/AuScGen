// ***********************************************************************
// <copyright file="Utility.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>UIAccess class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIAccess.WebControls;
using WebDriverWrapper;
/*
 * Created by SharpDevelop.
 * User: apal
 * Date: 24-06-2014
 * Time: 10:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace UIAccess
{
    /// <summary>
    /// Description of Utility.
    /// 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Gets the web control from i contol.
        /// </summary>
        /// <param name="control">a control.</param>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        /// <param name="conrolType">Type of a conrol.</param>
        /// <returns>WebControl.</returns>
        internal static WebControl GetWebControlFromIContol(IControl control, Browser browser, Locator locator, ControlType conrolType)
        {
            WebControl webControl = null;

            if (conrolType == ControlType.Button)
            {
                WebButton webButton = new WebButton(browser, locator);
                webButton.ControlObject = control;
                webControl = webButton;
            }

            if (conrolType == ControlType.EditBox)
            {
                WebEditBox webEditBox = new WebEditBox(browser, locator);
                webEditBox.ControlObject = control;
                webControl = webEditBox;
            }

            if (conrolType == ControlType.Custom)
            {
                webControl = new WebControl(browser, locator);
                webControl.ControlObject = control;
                //aWebControl = aWebEditBox;
            }
            if (conrolType == ControlType.Calender)
            {
                WebCalender webCalender = new WebCalender(browser, locator);
                webCalender.ControlObject = control;
                webControl = webCalender;
            }

            if (conrolType == ControlType.ComboBox)
            {
                WebComboBox webComboBox = new WebComboBox(browser, locator);
                webComboBox.ControlObject = control;
                webControl = webComboBox;
            }

            if (conrolType == ControlType.CheckBox)
            {
                WebCheckBox webCheckBox = new WebCheckBox(browser, locator);
                webCheckBox.ControlObject = control;
                webControl = webCheckBox;
            }

            if (conrolType == ControlType.Dialog)
            {
                WebDialog webDialog = new WebDialog(browser, locator);
                webDialog.ControlObject = control;
                webControl = webDialog;
            }

            if (conrolType == ControlType.Frame)
            {
                WebFrame webFrame = new WebFrame(browser, locator);
                webFrame.ControlObject = control;
                webControl = webFrame;
            }

            if (conrolType == ControlType.Image)
            {
                WebImage webImage = new WebImage(browser, locator);
                webImage.ControlObject = control;
                webControl = webImage;
            }

            if (conrolType == ControlType.Label)
            {
                WebLabel webLabel = new WebLabel(browser, locator);
                webLabel.ControlObject = control;
                webControl = webLabel;
            }

            if (conrolType == ControlType.Link)
            {
                WebLink webLink = new WebLink(browser, locator);
                webLink.ControlObject = control;
                webControl = webLink;
            }

            if (conrolType == ControlType.ListBox)
            {
                WebListBox webListBox = new WebListBox(browser, locator);
                webListBox.ControlObject = control;
                webControl = webListBox;
            }

            if (conrolType == ControlType.Page)
            {
                WebPage webPage = new WebPage(browser, locator);
                webPage.ControlObject = control;
                webControl = webPage;
            }

            if (conrolType == ControlType.RadioButton)
            {
                WebRadioButton webRadioButton = new WebRadioButton(browser, locator);
                webRadioButton.ControlObject = control;
                webControl = webRadioButton;
            }

            if (conrolType == ControlType.SpanArea)
            {
                WebSpanArea webSpanArea = new WebSpanArea(browser, locator);
                webSpanArea.ControlObject = control;
                webControl = webSpanArea;
            }

            if (conrolType == ControlType.WebTable)
            {
                WebTable webTable = new WebTable(browser, locator);
                webTable.ControlObject = control;
                webControl = webTable;
            }

            if (conrolType == ControlType.WebRow)
            {
                WebRow webRow = new WebRow(browser, locator);
                webRow.ControlObject = control;
                webControl = webRow;
            }

            if (conrolType == ControlType.WebCell)
            {
                WebCell webCell = new WebCell(browser, locator);
                webCell.ControlObject = control;
                webControl = webCell;
            }

            return webControl;
        }

        /// <summary>
        /// Gets the web controls from i control list.
        /// </summary>
        /// <param name="controlList">a i control list.</param>
        /// <param name="browser">a browser.</param>
        /// <param name="locator">a locator.</param>
        /// <param name="controlType">Type of a conrol.</param>
        /// <returns>List&lt;WebControl&gt;.</returns>
        internal static List<WebControl> GetWebControlsFromIControlList(IList<IControl> controlList, Browser browser, Locator locator, ControlType controlType)
        {
            List<WebControl> webControlList = new List<WebControl>();

            foreach (IControl control in controlList)
            {
                webControlList.Add(GetWebControlFromIContol(control, browser, locator, controlType));
            }

            return webControlList;
        }
    }
}