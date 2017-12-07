// ***********************************************************************
// <copyright file="AutoITExtension.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>AutoITExtension class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoIt;
using Framework;

namespace AuScGen.CommonUtilityPlugin
{
	/// <summary>
	/// AutoITExtension
	/// </summary>
	[Export(typeof(IPlugin))]
	public class AutoITExtension : IPlugin
	{
		/// <summary>
		/// The thistitle
		/// </summary>
		private string thistitle;
		/// <summary>
		/// The thiswindow handle
		/// </summary>
		private IntPtr thiswindowHandle;

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoITExtension"/> class.
		/// </summary>
		public AutoITExtension()
		{
			thistitle = string.Empty;
			thiswindowHandle = GetWindowHandle(thistitle, string.Empty, 0);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoITExtension"/> class.
		/// </summary>
		/// <param name="windowTitle">The window title.</param>
		/// <param name="text">The text.</param>
		public AutoITExtension(string windowTitle, string text)
		{
			text = string.Empty;
			thistitle = windowTitle;
			thiswindowHandle = GetWindowHandle(windowTitle, text, 0);
		}

		/// <summary>
		/// Gets the dialog.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public AutoITExtension GetDialog(string title, string text, int timeout)
		{
			text = string.Empty;
			timeout = 5000;
			thistitle = title;
			thiswindowHandle = GetWindowHandle(title, text, timeout);
			return this;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AutoITExtension"/> class.
		/// </summary>
		/// <param name="windowHandle">The window handle.</param>
		/// <param name="maxLen">The maximum length.</param>
		public AutoITExtension(IntPtr windowHandle, int maxLen)
		{
			maxLen = 65535;
			thiswindowHandle = windowHandle;
			WinGetTitle(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Waits for window.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="timeout">The time out.</param>
		/// <returns>System.Int32.</returns>
		public static bool WaitForWindow(string title, int timeout)
		{
			DateTime start = DateTime.Now;

			while (!IsWinExists(title) && (DateTime.Now - start).TotalMilliseconds < timeout) ;

			return (DateTime.Now - start).TotalMilliseconds < timeout;
		}

		/// <summary>
		/// Gets the window handle.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static IntPtr GetWindowHandle(string title, string text, int timeout)
		{
			timeout = 10000;
			WaitForWindow(title, timeout);
			return AutoItX.WinGetHandle(title, text);
		}

		/// <summary>
		/// Determines whether [is win exists] [the specified title].
		/// </summary>
		/// <param name="title">The title.</param>
		/// <returns></returns>
		private static bool IsWinExists(string title)
		{
			return (int)AutoItX.WinGetHandle(title) != 0 ? true : false;
		}

		/// <summary>
		/// Automatics it set option.
		/// </summary>
		/// <param name="option">The option.</param>
		/// <param name="optionValue">The option value.</param>
		/// <returns>System.Int32.</returns>
		public static int AutoItSetOption(string option, int optionValue)
		{
			return AutoItX.AutoItSetOption(option, optionValue);
		}

		/// <summary>
		/// Clips the get.
		/// </summary>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ClipGet(int maxLen)
		{
			maxLen = 1048576;
			return AutoItX.ClipGet(maxLen);
		}

		/// <summary>
		/// Clips the put.
		/// </summary>
		/// <param name="text">The text.</param>
		public static void ClipPut(string text)
		{
			AutoItX.ClipPut(text);
		}

		/// <summary>
		/// Controls the click.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="button">The button.</param>
		/// <param name="numberOfClicks">The number of clicks.</param>
		/// <param name="xpath">The x path.</param>
		/// <param name="ypath">The y path.</param>
		/// <returns>System.Int32.</returns>
		public int ControlClick(string control, string button, int numberOfClicks, int xpath, int ypath)
		{
			control = string.Empty;
			button = "left";
			numberOfClicks = 1;
			xpath = -2147483647;
			ypath = -2147483647;
			return AutoItX.ControlClick(thiswindowHandle, ControlGetHandle(control), button, numberOfClicks, xpath, ypath);
		}

		/// <summary>
		/// Controls the click.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="button">The button.</param>
		/// <param name="numberOfClicks">The number clicks.</param>
		/// <param name="xpath">The x path.</param>
		/// <param name="ypath">The y path.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlClick(string title, string text, string control, string button, int numberOfClicks, int xpath, int ypath)
		{
			button = "left";
			numberOfClicks = 1;
			xpath = -2147483647;
			ypath = -2147483647;
			return AutoItX.ControlClick(title, text, control, button, numberOfClicks, xpath, ypath);
		}

		/// <summary>
		/// Controls the command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="extra">The extra.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public string ControlCommand(string command, string extra, int maxLen, string control)
		{
			control = string.Empty;
			maxLen = 65535;
			return AutoItX.ControlCommand(thiswindowHandle, ControlGetHandle(control), command, extra, maxLen);
		}

		/// <summary>
		/// Controls the command.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="command">The command.</param>
		/// <param name="extra">The extra.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlCommand(string title, string text, string control, string command, string extra, int maxLen)
		{
			maxLen = 65535;
			return AutoItX.ControlCommand(title, text, control, command, extra, maxLen);
		}

		/// <summary>
		/// Controls the disable.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlDisable(string control)
		{
			control = string.Empty;
			return AutoItX.ControlDisable(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the disable.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns></returns>
		public static int ControlDisable(string title, string text, string control)
		{
			return AutoItX.ControlDisable(title, text, control);
		}

		/// <summary>
		/// Controls the enable.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlEnable(string control)
		{
			control = string.Empty;
			return AutoItX.ControlEnable(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the enable.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlEnable(string title, string text, string control)
		{
			return AutoItX.ControlEnable(title, text, control);
		}

		/// <summary>
		/// Controls the focus.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlFocus(string control)
		{
			control = string.Empty;
			return AutoItX.ControlFocus(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the focus.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlFocus(string title, string text, string control)
		{
			return AutoItX.ControlFocus(title, text, control);
		}

		/// <summary>
		/// Controls the get focus.
		/// </summary>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public string ControlGetFocus(int maxLen)
		{
			maxLen = 65535;
			return AutoItX.ControlGetFocus(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Controls the get focus.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlGetFocus(string title, string text, int maxLen)
		{
			title = !string.IsNullOrEmpty(title) ? title : string.Empty;
			text = !string.IsNullOrEmpty(text) ? text : string.Empty;
			maxLen = maxLen == 0 ? maxLen : 65535;
			return AutoItX.ControlGetFocus(title, text, maxLen);
		}

		/// <summary>
		/// Controls the get handle.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public IntPtr ControlGetHandle(string control)
		{
			control = string.Empty;
			return AutoItX.ControlGetHandle(thiswindowHandle, control);
		}

		/// <summary>
		/// Controls the get handle as text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlGetHandleAsText(string title, string text, string control, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			control = string.Empty;
			maxLen = 65535;
			return AutoItX.ControlGetHandleAsText(title, text, control, maxLen);
		}

		/// <summary>
		/// Controls the get position.
		/// </summary>
		/// <param name="winHandle">The win handle.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public Rectangle ControlGetPosition(IntPtr winHandle, string control)
		{
			control = !string.IsNullOrEmpty(control) ? control : string.Empty;
			return AutoItX.ControlGetPos(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the get position.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public static Rectangle ControlGetPosition(string title, string text, string control)
		{
			title = string.Empty;
			text = string.Empty;
			control = string.Empty;
			return AutoItX.ControlGetPos(title, text, control);
		}

		/// <summary>
		/// Controls the get text.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public string ControlGetText(string control, int maxLen)
		{
			control = string.Empty;
			maxLen = 65535;
			return AutoItX.ControlGetText(thiswindowHandle, ControlGetHandle(control), maxLen);
		}

		/// <summary>
		/// Controls the get text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlGetText(string title, string text, string control, int maxLen)
		{
			maxLen = 65535;
			return AutoItX.ControlGetText(title, text, control, maxLen);
		}

		/// <summary>
		/// Controls the hide.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlHide(string control)
		{
			control = string.Empty;
			return AutoItX.ControlHide(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the hide.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlHide(string title, string text, string control)
		{
			return AutoItX.ControlHide(title, text, control);
		}

		/// <summary>
		/// Controls the ListView.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="extra1">The extra1.</param>
		/// <param name="extra2">The extra2.</param>
		/// <param name="control">The control.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public string ControlListView(string command, string extra1, string extra2, string control, int maximumLength)
		{
			control = string.Empty;
			maximumLength = 65535;
			return AutoItX.ControlListView(thiswindowHandle, ControlGetHandle(control), command, extra1, extra2, maximumLength);
		}

		/// <summary>
		/// Controls the ListView.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="command">The command.</param>
		/// <param name="extra1">The extra1.</param>
		/// <param name="extra2">The extra2.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlListView(string title, string text, string control, string command, string extra1, string extra2, int maximumLength)
		{
			maximumLength = 65535;
			return AutoItX.ControlListView(title, text, control, command, extra1, extra2, maximumLength);
		}

		/// <summary>
		/// Controls the move.
		/// </summary>
		/// <param name="xpath">The x path.</param>
		/// <param name="ypath">The y path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlMove(int xpath, int ypath, int width, int height, string control)
		{
			width = -1;
			height = -1;
			control = string.Empty;
			return AutoItX.ControlMove(thiswindowHandle, ControlGetHandle(control), xpath, ypath, width, height);
		}

		/// <summary>
		/// Controls the move.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="xpath">The x path.</param>
		/// <param name="ypath">The y path.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlMove(string title, string text, string control, int xpath, int ypath, int width, int height)
		{
			width = -1;
			height = -1;
			return AutoItX.ControlMove(title, text, control, xpath, ypath, width, height);
		}

		/// <summary>
		/// Controls the send.
		/// </summary>
		/// <param name="sendText">The send text.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlSend(string sendText, int mode, string control)
		{
			control = string.Empty;
			mode = 0;
			return AutoItX.ControlSend(thiswindowHandle, ControlGetHandle(control), sendText, mode);
		}

		/// <summary>
		/// Controls the send.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="sendText">The send text.</param>
		/// <param name="mode">The mode.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlSend(string title, string text, string control, string sendText, int mode)
		{
			mode = 0;
			return AutoItX.ControlSend(title, text, control, sendText, mode);
		}

		/// <summary>
		/// Controls the set text.
		/// </summary>
		/// <param name="controlText">The control text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlSetText(string controlText, string control)
		{
			control = string.Empty;
			return AutoItX.ControlSetText(thiswindowHandle, ControlGetHandle(control), controlText);
		}
		/// <summary>
		/// Controls the set text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="controlText">The control text.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlSetText(string title, string text, string control, string controlText)
		{
			return AutoItX.ControlSetText(title, text, control, controlText);
		}
		/// <summary>
		/// Controls the show.
		/// </summary>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public int ControlShow(string control)
		{
			control = string.Empty;
			return AutoItX.ControlShow(thiswindowHandle, ControlGetHandle(control));
		}

		/// <summary>
		/// Controls the show.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <returns>System.Int32.</returns>
		public static int ControlShow(string title, string text, string control)
		{
			return AutoItX.ControlShow(title, text, control);
		}

		/// <summary>
		/// Controls the TreeView.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="extra1">The extra1.</param>
		/// <param name="extra2">The extra2.</param>
		/// <param name="control">The control.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public string ControlTreeView(string command, string extra1, string extra2, string control, int maximumLength)
		{
			control = string.Empty;
			maximumLength = 65535;
			return AutoItX.ControlTreeView(thiswindowHandle, ControlGetHandle(control), command, extra1, extra2, maximumLength);
		}

		/// <summary>
		/// Controls the TreeView.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="control">The control.</param>
		/// <param name="command">The command.</param>
		/// <param name="extra1">The extra1.</param>
		/// <param name="extra2">The extra2.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.Int32.</returns>
		public static string ControlTreeView(string title, string text, string control, string command, string extra1, string extra2, int maximumLength)
		{
			maximumLength = maximumLength != 0 ? maximumLength : 65535;
			return AutoItX.ControlTreeView(title, text, control, command, extra1, extra2, maximumLength);
		}

		/// <summary>
		/// Drives the map add.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <param name="share">The share.</param>
		/// <param name="flags">The flags.</param>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns>System.Int32.</returns>
		public static string DriveMapAdd(string device, string share, int flags, string user, string password)
		{
			flags = flags != 0 ? flags : 0;
			user = !string.IsNullOrEmpty(user) ? user : string.Empty;
			password = !string.IsNullOrEmpty(password) ? password : string.Empty;
			return AutoItX.DriveMapAdd(device, share, flags, user, password);
		}

		/// <summary>
		/// Drives the map delete.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns>System.Int32.</returns>
		public static int DriveMapDel(string device)
		{
			return AutoItX.DriveMapDel(device);
		}

		/// <summary>
		/// Drives the map get.
		/// </summary>
		/// <param name="device">The device.</param>
		/// <returns>System.Int32.</returns>
		public static string DriveMapGet(string device)
		{
			return AutoItX.DriveMapGet(device);
		}

		/// <summary>
		/// Errors the code.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public static int ErrorCode()
		{
			return AutoItX.ErrorCode();
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public static void Init()
		{
			AutoItX.Init();
		}

		/// <summary>
		/// Determines whether this instance is admin.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public static int IsAdmin()
		{
			return AutoItX.IsAdmin();
		}

		/// <summary>
		/// Mouses the click.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="xpath">The xpath.</param>
		/// <param name="ypath">The ypath.</param>
		/// <param name="numberOfClicks">The number clicks.</param>
		/// <param name="speed">The speed.</param>
		/// <returns>System.Int32.</returns>
		public static int MouseClick(string button, int xpath, int ypath, int numberOfClicks, int speed)
		{
			button = "LEFT";
			xpath = -2147483647;
			ypath = -2147483647;
			numberOfClicks = 1;
			speed = -1;
			return AutoItX.MouseClick(button, xpath, ypath, numberOfClicks, speed);
		}

		/// <summary>
		/// Mouses the click drag.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="xpath1">The xpath 1.</param>
		/// <param name="ypath1">The ypath 1.</param>
		/// <param name="xpath2">The xpath 2.</param>
		/// <param name="ypath2">The ypath 2.</param>
		/// <param name="speed">The speed.</param>
		/// <returns>System.Int32.</returns>
		public static int MouseClickDrag(string button, int xpath1, int ypath1, int xpath2, int ypath2, int speed)
		{
			speed = -1;
			return AutoItX.MouseClickDrag(button, xpath1, ypath1, xpath2, ypath2, speed);
		}

		/// <summary>
		/// Mouses down.
		/// </summary>
		/// <param name="button">The button.</param>
		public static void MouseDown(string button)
		{
			button = "LEFT";
			AutoItX.MouseDown(button);
		}
		/// <summary>
		/// Mouses the get cursor.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public static int MouseGetCursor()
		{
			return AutoItX.MouseGetCursor();
		}

		/// <summary>
		/// Mouses the get position.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public static Point MouseGetPosition()
		{
			return AutoItX.MouseGetPos();
		}

		/// <summary>
		/// Mouses the move.
		/// </summary>
		/// <param name="xpath">The x.</param>
		/// <param name="ypath">The y.</param>
		/// <param name="speed">The speed.</param>
		/// <returns>System.Int32.</returns>
		public static int MouseMove(int xpath, int ypath, int speed)
		{
			speed = -1;
			return AutoItX.MouseMove(xpath, ypath, speed);
		}

		/// <summary>
		/// Mouses up.
		/// </summary>
		/// <param name="button">The button.</param>
		public static void MouseUp(string button)
		{
			button = "LEFT";
			AutoItX.MouseUp(button);
		}

		/// <summary>
		/// Mouses the wheel.
		/// </summary>
		/// <param name="direction">The direction.</param>
		/// <param name="numberOfClicks">The number clicks.</param>
		public static void MouseWheel(string direction, int numberOfClicks)
		{
			AutoItX.MouseWheel(direction, numberOfClicks);
		}

		/// <summary>
		/// Pixels the checksum.
		/// </summary>
		/// <param name="rect">The rect.</param>
		/// <param name="step">The step.</param>
		/// <returns>System.UInt32.</returns>
		public static uint PixelChecksum(Rectangle rect, int step)
		{
			step = 1;
			return AutoItX.PixelChecksum(rect, step);
		}

		/// <summary>
		/// Pixels the color of the get.
		/// </summary>
		/// <param name="xpath">The xpath.</param>
		/// <param name="ypath">The ypath.</param>
		/// <returns>System.Int32.</returns>
		public static int PixelGetColor(int xpath, int ypath)
		{
			return AutoItX.PixelGetColor(xpath, ypath);
		}

		/// <summary>
		/// Pixels the search.
		/// </summary>
		/// <param name="rect">The rect.</param>
		/// <param name="color">The color.</param>
		/// <param name="shade">The shade.</param>
		/// <param name="step">The step.</param>
		/// <returns>Point.</returns>
		public static Point PixelSearch(Rectangle rect, int color, int shade, int step)
		{
			shade = 0;
			step = 1;
			return AutoItX.PixelSearch(rect, color, shade, step);
		}

		/// <summary>
		/// Processes the close.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <returns>System.Int32.</returns>
		public static int ProcessClose(string process)
		{
			return AutoItX.ProcessClose(process);
		}

		/// <summary>
		/// Processes the exists.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <returns>System.Int32.</returns>
		public static int ProcessExists(string process)
		{
			return AutoItX.ProcessExists(process);
		}

		/// <summary>
		/// Processes the set priority.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <param name="priority">The priority.</param>
		/// <returns>System.Int32.</returns>
		public static int ProcessSetPriority(string process, int priority)
		{
			return AutoItX.ProcessSetPriority(process, priority);
		}

		/// <summary>
		/// Processes the wait.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int ProcessWait(string process, int timeout)
		{
			return AutoItX.ProcessWait(process, timeout);
		}

		/// <summary>
		/// Processes the wait close.
		/// </summary>
		/// <param name="process">The process.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int ProcessWaitClose(string process, int timeout)
		{
			return AutoItX.ProcessWaitClose(process, timeout);
		}

		/// <summary>
		/// Runs the specified program.
		/// </summary>
		/// <param name="program">The program.</param>
		/// <param name="direction">The dir.</param>
		/// <param name="showFlag">The show flag.</param>
		/// <returns>System.Int32.</returns>
		public static int Run(string program, string direction, int showFlag)
		{
			showFlag = 1;
			return AutoItX.Run(program, direction, showFlag);
		}

		/// <summary>
		/// Runs as.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="domain">The domain.</param>
		/// <param name="password">The password.</param>
		/// <param name="logOnFlag">The log on flag.</param>
		/// <param name="program">The program.</param>
		/// <param name="direction">The dir.</param>
		/// <param name="showFlag">The show flag.</param>
		/// <returns>System.Int32.</returns>
		public static int RunAs(string user, string domain, string password, int logOnFlag, string program, string direction, int showFlag)
		{
			showFlag = 1;
			return AutoItX.RunAs(user, domain, password, logOnFlag, program, direction, showFlag);
		}
		/// <summary>
		/// Runs as wait.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="domain">The domain.</param>
		/// <param name="password">The password.</param>
		/// <param name="logOnFlag">The log on flag.</param>
		/// <param name="program">The program.</param>
		/// <param name="direction">The dir.</param>
		/// <param name="showFlag">The show flag.</param>
		/// <returns>System.Int32.</returns>
		public static int RunAsWait(string user, string domain, string password, int logOnFlag, string program, string direction, int showFlag)
		{
			showFlag = showFlag != 0 ? showFlag : 1;
			return AutoItX.RunAsWait(user, domain, password, logOnFlag, program, direction);
		}

		/// <summary>
		/// Runs the wait.
		/// </summary>
		/// <param name="program">The program.</param>
		/// <param name="direction">The dir.</param>
		/// <param name="showFlag">The show flag.</param>
		/// <returns>System.Int32.</returns>
		public static int RunWait(string program, string direction, int showFlag)
		{
			showFlag = 1;
			return AutoItX.RunWait(program, direction, showFlag);
		}

		/// <summary>
		/// Sends the specified send text.
		/// </summary>
		/// <param name="sendText">The send text.</param>
		/// <param name="mode">The mode.</param>
		public static void Send(string sendText, int mode)
		{
			mode = 0;
			AutoItX.Send(sendText, mode);
		}

		/// <summary>
		/// Shutdowns the specified flag.
		/// </summary>
		/// <param name="flag">The flag.</param>
		/// <returns>System.Int32.</returns>
		public static int Shutdown(int flag)
		{
			return AutoItX.Shutdown(flag);
		}

		/// <summary>
		/// Sleeps the specified milliseconds.
		/// </summary>
		/// <param name="milliseconds">The milliseconds.</param>
		public static void Sleep(int milliseconds)
		{
			AutoItX.Sleep(milliseconds);
		}
		/// <summary>
		/// Statuses the bar get text.
		/// </summary>
		/// <param name="part">The part.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.String.</returns>
		public string StatusBarGetText(int part, int maximumLength)
		{
			part = 1;
			maximumLength = 65535;
			return AutoItX.StatusBarGetText(thiswindowHandle, part, maximumLength);
		}

		/// <summary>
		/// Statuses the bar get text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="part">The part.</param>
		/// <param name="maximumLength">The maximum length.</param>
		/// <returns>System.String.</returns>
		public static string StatusBarGetText(string title, string text, int part, int maximumLength)
		{
			title = string.Empty;
			text = string.Empty;
			part = 1;
			maximumLength = 65535;
			return AutoItX.StatusBarGetText(title, text, part, maximumLength);
		}

		/// <summary>
		/// Tools the tip.
		/// </summary>
		/// <param name="tip">The tip.</param>
		/// <param name="xpath">The xpath.</param>
		/// <param name="ypath">The ypath.</param>
		public static void ToolTip(string tip, int xpath, int ypath)
		{
			xpath = -2147483647;
			ypath = -2147483647;
			AutoItX.ToolTip(tip, xpath, ypath);
		}

		/// <summary>
		/// Wins the activate.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinActivate()
		{
			return AutoItX.WinActivate(thiswindowHandle);
		}

		/// <summary>
		/// Wins the activate.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinActivate(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinActivate(title, text);
		}

		/// <summary>
		/// Wins the active.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinActive()
		{
			return AutoItX.WinActive(thiswindowHandle);
		}

		/// <summary>
		/// Wins the active.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinActive(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinActive(title, text);
		}

		/// <summary>
		/// Wins the close.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinClose()
		{
			return AutoItX.WinClose(thiswindowHandle);
		}

		/// <summary>
		/// Wins the close.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinClose(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinClose(title, text);
		}

		/// <summary>
		/// Wins the exists.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinExists()
		{
			return AutoItX.WinExists(thiswindowHandle);
		}

		/// <summary>
		/// Wins the exists.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinExists(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinExists(title, text);
		}

		/// <summary>
		/// Wins the get caret position.
		/// </summary>
		/// <returns>Point.</returns>
		public static Point WinGetCaretPosition()
		{
			return AutoItX.WinGetCaretPos();
		}

		/// <summary>
		/// Wins the get class list.
		/// </summary>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns></returns>
		public string WinGetClassList(int maxLen)
		{
			maxLen = 65535;
			return AutoItX.WinGetClassList(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Wins the get class list.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public static string WinGetClassList(string title, string text, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			maxLen = 65535;
			return AutoItX.WinGetClassList(title, text, maxLen);
		}

		/// <summary>
		/// Wins the size of the get client.
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle WinGetClientSize()
		{
			return AutoItX.WinGetClientSize(thiswindowHandle);
		}

		/// <summary>
		/// Wins the size of the get client.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>Rectangle.</returns>
		public static Rectangle WinGetClientSize(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinGetClientSize(title, text);
		}

		/// <summary>
		/// Wins the get handle.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>IntPtr.</returns>
		public static IntPtr WinGetHandle(string title, string text)
		{
			text = string.Empty;
			title = string.Empty;
			return AutoItX.WinGetHandle(title, text);
		}

		/// <summary>
		/// Wins the get handle as text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public static string WinGetHandleAsText(string title, string text, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			maxLen = 65535;
			return AutoItX.WinGetHandleAsText(title, text, maxLen);
		}

		/// <summary>
		/// Wins the get position.
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle WinGetPosition()
		{
			return AutoItX.WinGetPos(thiswindowHandle);
		}

		/// <summary>
		/// Wins the get position.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>Rectangle.</returns>
		public static Rectangle WinGetPosition(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinGetPos(title, text);
		}

		/// <summary>
		/// Wins the get process.
		/// </summary>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.UInt32.</returns>
		public uint WinGetProcess(int maxLen)
		{
			maxLen = 65535;
			return AutoItX.WinGetProcess(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Wins the get process.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.UInt32.</returns>
		public static uint WinGetProcess(string title, string text, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			maxLen = 65535;
			return AutoItX.WinGetProcess(title, text, maxLen);
		}

		/// <summary>
		/// Wins the state of the get.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinGetState()
		{
			return AutoItX.WinGetState(thiswindowHandle);
		}

		/// <summary>
		/// Wins the state of the get.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinGetState(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinGetState(title, text);
		}

		/// <summary>
		/// Wins the get text.
		/// </summary>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public string WinGetText(int maxLen)
		{
			maxLen = 65535;
			return AutoItX.WinGetText(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Wins the get text.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public static string WinGetText(string title, string text, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			maxLen = 65535;
			return AutoItX.WinGetText(title, text, maxLen);
		}

		/// <summary>
		/// Wins the get title.
		/// </summary>
		/// <param name="winHandle">The win handle.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public string WinGetTitle(IntPtr winHandle, int maxLen)
		{
			maxLen = maxLen != 0 ? maxLen : 65535;
			return AutoItX.WinGetTitle(thiswindowHandle, maxLen);
		}

		/// <summary>
		/// Wins the get title.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="maxLen">The maximum length.</param>
		/// <returns>System.String.</returns>
		public static string WinGetTitle(string title, string text, int maxLen)
		{
			title = string.Empty;
			text = string.Empty;
			maxLen = 65535;
			return AutoItX.WinGetTitle(title, text, maxLen);
		}

		/// <summary>
		/// Wins the kill.
		/// </summary>
		/// <returns>System.Int32.</returns>
		public int WinKill()
		{
			return AutoItX.WinKill(thiswindowHandle);
		}

		/// <summary>
		/// Wins the kill.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <returns>System.Int32.</returns>
		public static int WinKill(string title, string text)
		{
			title = string.Empty;
			text = string.Empty;
			return AutoItX.WinKill(title, text);
		}

		/// <summary>
		/// Wins the minimize all.
		/// </summary>
		public static void WinMinimizeAll()
		{
			AutoItX.WinMinimizeAll();
		}
		/// <summary>
		/// Wins the minimize all undo.
		/// </summary>
		public static void WinMinimizeAllUndo()
		{
			AutoItX.WinMinimizeAllUndo();
		}

		/// <summary>
		/// Wins the move.
		/// </summary>
		/// <param name="xpath">The xpath.</param>
		/// <param name="ypath">The ypath.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>System.Int32.</returns>
		public int WinMove(int xpath, int ypath, int width, int height)
		{
			width = -1;
			height = -1;
			return AutoItX.WinMove(thiswindowHandle, xpath, ypath, width, height);
		}

		/// <summary>
		/// Wins the move.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="xpath">The xpath.</param>
		/// <param name="ypath">The ypath.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <returns>System.Int32.</returns>
		public static int WinMove(string title, string text, int xpath, int ypath, int width, int height)
		{
			width = -1;
			height = -1;
			return AutoItX.WinMove(title, text, xpath, ypath, width, height);
		}

		/// <summary>
		/// Wins the seton top.
		/// </summary>
		/// <param name="flag">The flag.</param>
		/// <returns>System.Int32.</returns>
		public int WinSetonTop(int flag)
		{
			return AutoItX.WinSetOnTop(thiswindowHandle, flag);
		}

		/// <summary>
		/// Wins the seton top.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="flag">The flag.</param>
		/// <returns>System.Int32.</returns>
		public static int WinSetonTop(string title, string text, int flag)
		{
			return AutoItX.WinSetOnTop(title, text, flag);
		}

		/// <summary>
		/// Wins the state of the set.
		/// </summary>
		/// <param name="flags">The flags.</param>
		/// <returns>System.Int32.</returns>
		public int WinSetState(int flags)
		{
			return AutoItX.WinSetState(thiswindowHandle, flags);
		}

		/// <summary>
		/// Wins the state of the set.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="flags">The flags.</param>
		/// <returns>System.Int32.</returns>
		public static int WinSetState(string title, string text, int flags)
		{
			return AutoItX.WinSetState(title, text, flags);
		}

		/// <summary>
		/// Wins the set title.
		/// </summary>
		/// <param name="newTitle">The new title.</param>
		/// <returns></returns>
		public int WinSetTitle(string newTitle)
		{
			return AutoItX.WinSetTitle(thiswindowHandle, newTitle);
		}

		/// <summary>
		/// Wins the set title.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="newTitle">The new title.</param>
		/// <returns>System.Int32.</returns>
		public static int WinSetTitle(string title, string text, string newTitle)
		{
			return AutoItX.WinSetTitle(title, text, newTitle);
		}

		/// <summary>
		/// Wins the set trans.
		/// </summary>
		/// <param name="trans">The trans.</param>
		/// <returns>System.Int32.</returns>
		public int WinSetTrans(int trans)
		{
			return AutoItX.WinSetTrans(thiswindowHandle, trans);
		}

		/// <summary>
		/// Wins the set trans.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="trans">The trans.</param>
		/// <returns>System.Int32.</returns>
		public static int WinSetTrans(string title, string text, int trans)
		{
			return AutoItX.WinSetTrans(title, text, trans);
		}

		/// <summary>
		/// Wins the wait.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public int WinWait(int timeout)
		{
			return AutoItX.WinWait(thiswindowHandle, timeout);
		}

		/// <summary>
		/// Wins the wait.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int WinWait(string title, string text, int timeout)
		{
			title = string.Empty;
			text = string.Empty;
			timeout = 0;
			return AutoItX.WinWait(title, text, timeout);
		}

		/// <summary>
		/// Wins the wait active.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public int WinWaitActive(int timeout)
		{
			timeout = 0;
			return AutoItX.WinWaitActive(thiswindowHandle, timeout);
		}

		/// <summary>
		/// Wins the wait active.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int WinWaitActive(string title, string text, int timeout)
		{
			title = string.Empty;
			text = string.Empty;
			timeout = 0;
			return AutoItX.WinWaitActive(title, text, timeout);
		}

		/// <summary>
		/// Wins the wait close.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public int WinWaitClose(int timeout)
		{
			timeout = 0;
			return AutoItX.WinWaitClose(thiswindowHandle, timeout);
		}

		/// <summary>
		/// Wins the wait close.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int WinWaitClose(string title, string text, int timeout)
		{
			title = string.Empty;
			text = string.Empty;
			timeout = 0;
			return AutoItX.WinWaitClose(title, text, timeout);
		}

		/// <summary>
		/// Wins the wait not active.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public int WinWaitNotActive(int timeout)
		{
			timeout = 0;
			return AutoItX.WinWaitNotActive(thiswindowHandle, timeout);
		}

		/// <summary>
		/// Wins the wait not active.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="text">The text.</param>
		/// <param name="timeout">The timeout.</param>
		/// <returns>System.Int32.</returns>
		public static int WinWaitNotActive(string title, string text, int timeout)
		{
			title = string.Empty;
			text = string.Empty;
			timeout = 0;
			return AutoItX.WinWaitNotActive(title, text, timeout);
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
	}
}
