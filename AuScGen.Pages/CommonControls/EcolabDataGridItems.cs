using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.ObjectModel;
using System.Collections.ObjectModel;


namespace Ecolab.Pages.CommonControls
{
    public class EcolabDataGridItems : HtmlTableRow
    {
        private HtmlTableRow myRow;

        /// <summary>
        /// Constructor
        /// </summary>
        public EcolabDataGridItems() { }

        /// <summary>
        /// Overloaded Constructor
        /// </summary>
        /// <param name="gridelement"></param>
        public EcolabDataGridItems(HtmlTableRow gridelement)
           :base(gridelement.BaseElement)
        {
            myRow = gridelement;
        }

        /// <summary>
        /// GetColumnValues method returns the collection of all the value of the given row
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<string> GetColumnValues()
        {
            List<string> cellValues = new List<string>();
            ICollection<Element> cellList = myRow.ChildNodes;
            foreach (Element cell in cellList)
            {
                cellValues.Add(cell.InnerText);
            }
            return cellValues.AsReadOnly();
        }

        /// <summary>
        /// GetControls method returns the list of the subcontrols of the cell
        /// </summary>
        /// <returns></returns>
        public List<HtmlControl> GetEditableControls()
        {
            List<HtmlControl> controls = new List<HtmlControl>();
            ICollection<Element> cellList = myRow.ChildNodes;
            foreach (Element cell in cellList)
            {
                controls.Add(new HtmlControl(cell));
            }
            return controls;
        }

        /// <summary>
        /// GetControls method returns the list of the subcontrols of the cell
        /// </summary>
        /// <returns></returns>
        public IList<Element> GetButtons()
        {
            IList<Element> controls = new List<Element>();
            int nCount = 0;
            ICollection<Element> cellList = myRow.ChildNodes;
            foreach (Element cell in cellList)
            {
                if (cell.ChildNodes[0].TagName == "a")
                {
                    nCount = cell.Children.Count;
                    for (int i = 0; i <= nCount - 1; i++)
                    {
                        controls.Add(cell.ChildNodes[i]);
                    }
                    return controls;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the button controls.
        /// </summary>
        /// <returns></returns>
        public List<HtmlControl> GetButtonControls()
        {
            List<HtmlControl> buttonsList = new List<HtmlControl>();
            foreach (Element element in GetButtons())
            {
                buttonsList.Add(new HtmlControl(element));
            }
            return buttonsList;
        }

    }
}
