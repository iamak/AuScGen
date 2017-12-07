// ***********************************************************************
// <copyright file="Wait.cs" company="EPAM">
//     Copyright © AuScGen, All Rights Reserved.
// </copyright>
// <summary>Wait class</summary>
// ***********************************************************************
using System;
using System.Collections;
using ArtOfTest.WebAii.Controls.HtmlControls;

namespace AuScGen.Pages.Utils
{
	/// <summary>
	///		Class Wait
	/// </summary>
	public class Wait
	{
		/// <summary>
		/// The telerik
		/// </summary>
		private TelerikPlugin.TelerikFramework Telerik;
		/// <summary>
		/// Initializes a new instance of the <see cref="Wait"/> class.
		/// </summary>
		/// <param name="telerik">The telerik.</param>
		public Wait(TelerikPlugin.TelerikFramework telerik)
		{
			Telerik = telerik;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public delegate bool WaitForTrueAction();
		/// <summary>
		/// Waitfors the action.
		/// </summary>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public bool WaitforAction(WaitForTrueAction decisionAction, int maximumWaitTime)
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;

			while (false == decisionAction() && timeElapsed < maximumWaitTime)
			{
				Telerik.ActiveBrowser.RefreshDomTree();
				timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
			}

			return decisionAction();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public delegate HtmlControl WaitForHtmlControlAction();
		/// <summary>
		/// Waitfors the action.
		/// </summary>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="maxWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public HtmlControl WaitForAction(WaitForHtmlControlAction decisionAction, int maxWaitTime)
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;

			while (null == decisionAction() && timeElapsed < maxWaitTime)
			{
				Telerik.ActiveBrowser.RefreshDomTree();
				timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
			}

			return decisionAction();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public delegate object WaitForObjectAction();
		/// <summary>
		/// Waitfors the action.
		/// </summary>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public object WaitForAction(WaitForObjectAction decisionAction, int maximumWaitTime)
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;

			while (null == decisionAction() && timeElapsed < maximumWaitTime)
			{
				Telerik.ActiveBrowser.RefreshDomTree();
				timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
			}

			return decisionAction();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">Objects for wai</typeparam>
		/// <returns></returns>
		public delegate T WaitForObjectAction<T>();
		/// <summary>
		/// Waitfors the action.
		/// </summary>
		/// <typeparam name="T">WaitForObjectAction</typeparam>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public T WaitForAction<T>(WaitForObjectAction decisionAction, int maximumWaitTime)
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;
			if (!typeof(T).Name.Contains("ReadOnlyCollection"))
			{
				while (null == decisionAction() && timeElapsed < maximumWaitTime)
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
				}
			}
			else
			{
				while (null == decisionAction() && timeElapsed < maximumWaitTime / 2)
				{
					Telerik.ActiveBrowser.RefreshDomTree();
					timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
				}

				if (null != decisionAction())
				{
					WaitforAction(() =>
					{
						return (int)typeof(T).GetProperty("Count").GetValue(decisionAction()) > 0;
					}, Config.PageClassSettings.Default.MaxTimeoutValue / 2);
				}
			}

			return (T)decisionAction();
		}

		/// <summary>
		/// Waitfors the null action.
		/// </summary>
		/// <typeparam name="T">WaitForObjectAction</typeparam>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public T WaitForNullAction<T>(WaitForObjectAction decisionAction, int maximumWaitTime)
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;
			while (null != decisionAction() && timeElapsed < maximumWaitTime)
			{
				Telerik.ActiveBrowser.RefreshDomTree();
				timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
			}

			return (T)decisionAction();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T">Objects for wait</typeparam>
		/// <returns></returns>
		public delegate T WaitForCountAction<T>() where T : ICollection;
		/// <summary>
		/// Waitfors the count action.
		/// </summary>
		/// <typeparam name="T">WaitForCountAction</typeparam>
		/// <param name="decisionAction">The decision action.</param>
		/// <param name="countValue">The count value.</param>
		/// <param name="maximumWaitTime">The maximum wait time.</param>
		/// <returns></returns>
		public T WaitForCount<T>(WaitForCountAction<T> decisionAction, int countValue, int maximumWaitTime) where T : ICollection
		{
			DateTime start;
			double timeElapsed = 0;
			Telerik.ActiveBrowser.RefreshDomTree();

			start = DateTime.Now;
			while (decisionAction().Count != countValue && timeElapsed < maximumWaitTime)
			{
				Telerik.ActiveBrowser.RefreshDomTree();
				timeElapsed = ((TimeSpan)(DateTime.Now - start)).TotalMilliseconds;
			}
			//int test2 = test;
			return (T)decisionAction();
		}
	}
}
