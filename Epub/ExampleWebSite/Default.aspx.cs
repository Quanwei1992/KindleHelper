using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eBdb.EpubReader;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Epub epub = new Epub(ConfigurationManager.AppSettings["EpubFilesPath"] + "65472.epub");
		Response.Clear();
		Response.Write(epub.GetContentAsHtml());
		Response.End();
    }
}