using AuScGen.CommonUtilityPlugin;
using AuScGen.WhiteFramework;
using Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White.UIItems.TabItems;

namespace WhiteTestApp
{
	/// <summary>
	/// Class Program
	/// </summary>
    public abstract class Testbase
    {
		/// <summary>
		/// The container
		/// </summary>
        static protected ContainerAccess Container;
		/// <summary>
		/// The white
		/// </summary>
        static private WhitePlugin white;
		/// <summary>
		/// The GUI map path
		/// </summary>
        static protected string GuiMapPath = Directory.GetCurrentDirectory() + @"\GuiMaps\";

		/// <summary>
		/// Initializes a new instance of the <see cref="Testbase"/> class.
		/// </summary>
        public Testbase()
        {
            Container = new ContainerAccess();
            //White = Container.GetPlugin<WhitePlugin>();
            //white.ProcessName = "calc";
            //white.AppWindowName = "Calculator";


        }

		/// <summary>
		/// Gets the white.
		/// </summary>
		/// <value>
		/// The white.
		/// </value>
        public WhitePlugin White
        {
            get
            {
                if (null == white)
                {
                    white = Container.GetPlugin<WhitePlugin>();
                    white.ProcessName = "calc";
                    white.AppWindowName = "Calculator";

                    //white.ProcessName = "WinFormsTestApp";
                    //white.AppWindowName = "Form1";
                }
                return white;
            }
        }

		/// <summary>
		/// Gets the database access.
		/// </summary>
		/// <value>
		/// The database access.
		/// </value>
        public DataAccess DBAccess
        {
            get
            {
                return Container.GetPlugin<DataAccess>();
            }
        }
    }

	/// <summary>
	/// 
	/// </summary>
    public class Test : Testbase
    {
		/// <summary>
		/// Invokes the test.
		/// </summary>
        public void InvokeTest()
        {
            White.MapPath = GuiMapPath + "GuiMap.xml";

            White.Button("Button9").Click();

            White.Button("AddButton").Click();

            White.Button("Button7").Click();

            White.Button("EqualButton").Click();

            Console.WriteLine(White.Label("ResultField").Text);

            
            //List<AuScGen.WhiteFramework.TabPage> test = White.Tab("ControlsTab").Pages();
            //test[3].Select();

            //List<BaseControl> test2 = test.FirstOrDefault().Children.FirstOrDefault().Children.FirstOrDefault().Children;

            //List<AutomationElement> test3 = GetChildren(test2.FirstOrDefault());

            //White.Button("Button9").Click();
            //var test = GetChildren(test1.AutomationElement);
            

            //SearchCriteria aSearch = SearchCriteria.ByAutomationId("buttonInGroupBox");
            //var Test = White.GroupBox("Form1Pane").Get<TestStack.White.UIItems.Button>(aSearch);

            //Rectangle rect = White.Button("").Bounds;

            //White.Hyperlink("").HookEvents()            
        }

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <param name="element_in">The element_in.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">element_in</exception>
        internal static List<AutomationElement> GetChildren(AutomationElement element_in)
        {
            //Trace.TraceInformation(String.Format("{0}: Try to get MSUIA children from AutomationElement...", ""));
            if (null == element_in)
            {
                throw new ArgumentNullException("element_in");
            }
            List<AutomationElement> aList = new List<AutomationElement>(0);
            AutomationElement aChild = TreeWalker.RawViewWalker.GetFirstChild(element_in);

            while (null != aChild)
            {
                aList.Add(aChild);
                aChild = TreeWalker.RawViewWalker.GetNextSibling(aChild);
            }
            //Trace.TraceInformation(String.Format("{0}: MSUIA children from AutomationElement found.", ""));
            return aList;
        }
    }

	/// <summary>
	/// 
	/// </summary>
    class Program
    {
		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            Test myTest = new Test();
            myTest.InvokeTest();

            //KeyWordTable keywordTable = new KeyWordTable();
            //keywordTable.KeywordTablePath = Directory.GetCurrentDirectory() + @"\KeyWordTable\" + @"KeywordTable.xml";


            ////TestCase test = new TestCase();
            ////DriverScript.TestDriver testDriver = new TestDriver(keywordTable, test);
            ////testDriver.RunTest();

            //DataDriver testData = new DataDriver();
            //TestCase2 test2 = new TestCase2();
            //testData.Getdata();
            //testData.Getdata();
            //testData.Getdata();
            //DriverScript.TestDriver testDriver2 = new TestDriver(keywordTable, test2, testData);
            //testDriver2.RunDataDrivenTest();

            Console.Read();
        }
    }
}
