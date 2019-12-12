using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Player_CustomPage : System.Web.UI.Page
{
    private String customPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Game for Page
        if (Session["userID"] == null) Response.Redirect("~/Login.aspx");
        if (Session["activeGame"] == null) Response.Redirect("~/Home.aspx");
    }

    //Note to me:  Page_PreRender is the last thing to happen before html is born, Without this PostBacks will wreck my table's textboxes.
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Session["customPath"] == null) Response.Redirect("~/Home.aspx");
        else customPath = (String)Session["customPath"];

        HtmlGenericControl displayedPage = new HtmlGenericControl();
        displayedPage.TagName = "object";
        displayedPage.ID = "loadedPage";

        //If I have cookie load desired cookie page, else load default login/home page
        
        displayedPage.Attributes.Add("data", customPath + "#view=FitH");

        ObjectPlaceHolder.Controls.Add(displayedPage);
    }
}