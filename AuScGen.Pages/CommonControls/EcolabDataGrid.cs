using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;


namespace Ecolab.Pages.CommonControls
{
    public class EcolabDataGrid : HtmlTable
    {
        private Ecolab.TelerikPlugin.TelerikFramework telerikFramework;

        private string strLogicalName = string.Empty;

        private string myguiMap;

        /// <summary>
        /// EcolabDataGrid - Overloaded Constructor
        /// </summary>
        /// <param name="gridelement"></param>
        public EcolabDataGrid(HtmlTable gridelement)
            : base(gridelement.BaseElement) { }

        /// <summary>
        /// EcolabDataGrid
        /// </summary>
        /// <param name="telerik"></param>
        /// <param name="guiMapPath"></param>
        public EcolabDataGrid(Ecolab.TelerikPlugin.TelerikFramework telerik, string guiMapPath)
        {
            telerikFramework = telerik;
            myguiMap = guiMapPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EcolabDataGrid"/> class.
        /// </summary>
        /// <param name="telerik">The telerik.</param>
        /// <param name="guiMapPath">The GUI map path.</param>
        /// <param name="gridType">Type of the grid.</param>
        public EcolabDataGrid(Ecolab.TelerikPlugin.TelerikFramework telerik, string guiMapPath, string gridType)
            :this(telerik,guiMapPath)
        {
            telerikFramework = telerik;
            strLogicalName = gridType;
            myguiMap = string.Concat(guiMapPath);
        }

        /// <summary>
        /// MainTable
        /// </summary>
        public HtmlTable MainTable
        {
            get
            {
                return telerikFramework.WaitForControl<HtmlTable>(myguiMap, strLogicalName, Config.PageClassSettings.Default.MaxTimeoutValue);
            }
        }

        /// <summary>
        /// Rows meythod returns the list of the rowitems of the table
        /// </summary>
        public new List<EcolabDataGridItems> Rows
        {
            get
            {
                List<EcolabDataGridItems> itemList = new List<EcolabDataGridItems>();
                foreach (HtmlTableRow row in AllRows)
                {
                    itemList.Add(new EcolabDataGridItems(row));
                }

                return itemList;
            }
        }

        /// <summary>
        /// Selects the row based on the description
        /// </summary>
        /// <param name="Description">The description.</param>
        /// <returns></returns>
        public List<EcolabDataGridItems> SelectedRows(string Description)
        {
            List<EcolabDataGridItems> itemList = new List<EcolabDataGridItems>();
            foreach (HtmlTableRow row in AllRows)
            {
                if (row.InnerText.Contains(Description))
                {
                    itemList.Add(new EcolabDataGridItems(row));
                }
            }
            return itemList;
        }

        /// <summary>
        /// Select a specific the row based on the description
        /// </summary>
        /// <param name="Description">The description.</param>
        /// <returns></returns>
        public EcolabDataGridItems GetRow(string Description)
        {
            EcolabDataGridItems itemList = new EcolabDataGridItems();
            foreach (HtmlTableRow row in AllRows)
            {
                if (row.InnerText.Contains(Description))
                {
                    return (new EcolabDataGridItems(row));
                }
            }
            return null;
        }

        /// <summary>
        /// AllRows
        /// </summary>
        private new ReadOnlyCollection<HtmlTableRow> AllRows
        {
            get
            {
                return MainTable.AllRows;

            }
        }

    }
}
