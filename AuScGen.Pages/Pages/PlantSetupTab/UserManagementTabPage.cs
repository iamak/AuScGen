using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ArtOfTest.WebAii.Controls.HtmlControls;
using System.Collections.ObjectModel;
using ArtOfTest.WebAii.ObjectModel;
using System.Threading;
using System.Windows.Forms;
using ArtOfTest.WebAii.Core;

namespace Ecolab.Pages.Pages.PlantSetupTab
{
    public class UserManagementTabPage : PageBase
    {
         private string guiMap;

         public UserManagementTabPage(List<object> utilsList)
            : base(utilsList, "UserManagementTab.xml")
        {
            guiMap = string.Concat(GuiMapPath, "UserManagementTab.xml");
        }

         public HtmlControl UserManagementTab
         {
             get
             {
                 return GetHtmlControl<HtmlControl>("tabUserManagement");
             }
         }

         public CommonControls.EcolabDataGrid UserManagementTable
         {
             get
             {
                 return new CommonControls.EcolabDataGrid(Telerik, guiMap, "UserManagementTable");
             }
         }

         public HtmlControl AddUser
         {
             get
             {
                 return GetHtmlControl<HtmlControl>("AddUser");
             }
         }

         public HtmlInputText txtFirstName
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtFirstName");
             }
         }

         public HtmlInputText txtLastName
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtLastName");
             }
         }

         public HtmlInputText txtUserId
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtUserId");
             }
         }

         public HtmlInputText txtEmail
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEmail");
             }
         }

         public HtmlInputText txtContactNo
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtContactNo");
             }
         }

         public HtmlInputPassword txtPassword
         {
             get
             {
                 return GetHtmlControl<HtmlInputPassword>("txtPassword");
             }
         }

         public HtmlSelect ddlUserRole
         {
             get
             {
                 return GetHtmlControl<HtmlSelect>("ddlUserRole");
             }
         }

         public HtmlInputText txtEditFirstName
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEditFirstName");
             }
         }

         public HtmlInputText txtEditLastName
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEditLastName");
             }
         }

         public HtmlInputText txtEditUserId
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEditUserId");
             }
         }

         public HtmlInputText txtEditEmail
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEditEmail");
             }
         }

         public HtmlInputText txtEditContactNo
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtEditContactNo");
             }
         }

         public HtmlInputPassword txtEditPassword
         {
             get
             {
                 return GetHtmlControl<HtmlInputPassword>("txtEditPassword");
             }
         }

         public HtmlSelect ddlEditUserRole
         {
             get
             {
                 return GetHtmlControl<HtmlSelect>("ddlEditUserRole");
             }
         }

         public HtmlSpan ddlUserRoleS
         {
             get
             {
                 return GetHtmlControl<HtmlSpan>("ddlUserRole");
             }
         }

         public HtmlSpan Save
         {
             get
             {
                 return GetHtmlControl<HtmlSpan>("Save");
             }
         }

         public HtmlButton Cancel
         {
             get
             {
                 return GetHtmlControl<HtmlButton>("Cancel");
             }
         }

         public HtmlInputText txtFirstNameInLine
         {
             get
             {
                 return GetHtmlControl<HtmlInputText>("txtFirstNameInLine");
             }
         }

         public HtmlDiv userManagementMsgDiv
         {
             get
             {
                 return GetHtmlControl<HtmlDiv>("userManagementMsgDiv");
             }
         }

         public HtmlControl UserManagementGrid
         {
             get
             {
                 return GetHtmlControl<HtmlControl>("UserManagementTable");
             }
         }

         public HtmlControl Formerror
         {
             get
             {
                 return GetHtmlControl<HtmlControl>("form-error");
             }
         }

         public HtmlDiv userErrorMsg
         {
             get
             {
                 return GetHtmlControl<HtmlDiv>("userErrorMsg");
             }
         }
       

         public bool isRecordExist(string strUserID)
         {
             ICollection<Element> eChild = UserManagementGrid.Find.AllByXPath(@"//tbody/tr");
             foreach (Element e in eChild)
             {
                 if (e.ChildNodes[3].InnerText == strUserID)
                 {
                     return true;
                 }
             }
             return false;
         }

         public void AddingUser(string strFirstName, string strLastName, string strUserId, string strEmailId, string strContactNumber, string strPassword,
             string strUserRole)
         {
             AddUser.Click();
             MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             txtFirstName.Focus();
             txtFirstName.MouseClick();
             txtFirstName.TypeText(strFirstName);
             txtLastName.TypeText(strLastName);
             txtUserId.TypeText(strUserId);
             txtEmail.TypeText(strEmailId);
             txtContactNo.TypeText(strContactNumber);
             txtPassword.Focus();
             txtPassword.Text = strPassword;
             //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             ddlUserRole.Focus();
             ddlUserRole.SelectByText(strUserRole,true);
             MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             Save.Focus();
             Save.Click();
         }

         public void UpdatingUser(string strFirstName, string strLastName, string strUserId, string strEmailId, string strContactNumber, string strPassword,
            string strUserRole)
         {
             MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             //MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             txtEditFirstName.Focus();
             txtEditFirstName.MouseClick();
             txtEditFirstName.TypeText(strFirstName);
             txtEditLastName.TypeText(strLastName);
             txtEditUserId.TypeText(strUserId);
             txtEditEmail.TypeText(strEmailId);
             txtEditContactNo.TypeText(strContactNumber);
             txtEditPassword.Focus();
             txtEditPassword.Text = strPassword;
             ddlEditUserRole.Focus();
             ddlEditUserRole.SelectByText(strUserRole);
             MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             Save.Focus();
             Save.Click();

         }

         public void InlineEditingUser(string strRowText , string strFirstName)
         {
             MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
             HtmlControl ctrl = UserManagementTable.SelectedRows(strRowText)[0];
             ICollection<Element> eList = ctrl.Find.AllByXPath(@"//td[2]/input");
             foreach(Element e in eList)
             {
                 (new HtmlControl(e)).Focus();
                 (new HtmlControl(e)).TypeText(strFirstName);
                 MouseKeyboardLibrary.KeyboardSimulator.KeyPress(System.Windows.Forms.Keys.Tab);
                 break;
             }         
         }

        


    }
}
