using System;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace KaYi.Utilities
{
	public static class UI
	{
		public static void DisableControls(ControlCollection controls)
		{
			foreach (Control control in controls)
			{
				if (control is TextBox)
				{
					((TextBox)control).Enabled = false;
				}
				if (control is DropDownList)
				{
					((DropDownList)control).Enabled = false;
				}
			}
		}
	}
}
