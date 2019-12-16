using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite_CustomPageTool : System.Web.UI.Page
{
    Game game;
    CustomPageList pages;
    PageTable pageTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Set Game for Page
        if (Session["userID"] == null) Response.Redirect("~/Login");
        if (Session["activeGame"] == null) Response.Redirect("~/Home");

        //Get Active Game
        game = (Game)Session["activeGame"];
        gameNameLabel.Text = game.GameName;

        //Load CustomPages State if appropriate
        if (Session["savedContent"] != null && ((CustomPageList)Session["savedContent"]).GameID != game.GameID) Session.Remove("savedContent");  //Handles if there is savedContent from a wrong game.
        if (Session["savedContent"] == null) loadPages(); //Handles a fresh load with no saved content
        else pages = (CustomPageList)Session["savedContent"];  //Handles there is saved content from this game.

        loadPageTable();

        //Little tool for relaying messages thru redirects
        if (Session["message"] != null)
        {
            Message message = (Message)Session["message"];
            angryLabel.ForeColor = message.Color;
            angryLabel.Text = message.Text;
            Session.Remove("message");
        }
        else angryLabel.Text = "&nbsp;";
    }

    //Loads the custom pages from the database
    private void loadPages()
    {
        PagesTable pagesTable = new PagesTable(new DatabaseConnection());
        pages = pagesTable.getGamePages(game.GameID);
    }

    //Loads the Object Table
    private void loadPageTable()
    {
        pageTable = new PageTable(pages);
        PageTablePlaceHolder.Controls.Add(pageTable);
    }

    //Saves any changes to the Pages Table
    protected void saveButton_Click(object sender, EventArgs e)
    {
        PagesTable pagesTable = new PagesTable(new DatabaseConnection());

        pageTable.saveContentChanges();
        pages.Pages = pageTable.getContent();

        //Foreach tableRow do the applicable database command (update/insert/delete) to mirror what the user has done in the table.
        int deleted = 0;
        foreach (ObjectTableRow objRow in pageTable.ObjectRows)
        {
            CustomPage page = (CustomPage)objRow.Obj;

            if (page.MarkedForDeletion == true) //delete page from database AND file structure
            {
                pagesTable.deleteCustomPage(page.PageID);
                File.Delete(Server.MapPath("~/") + page.PageURL);
                pages.Pages.Remove(page.SortIndex);
                deleted++;
            }
            else
            {
                page.SortIndex -= deleted;
                pagesTable.updateCustomPage(page); //update page
            }
        }
        
        //Remove Content
        Session.Remove("savedContent");
        Session["message"] = new Message("Pages Saved!", System.Drawing.Color.Green);

        //Reload page to clear any nonsense before loading
        Response.Redirect("CustomPageTool");
    }

    //Uploads PDF files and makes new custom pages
    protected void uploadButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.PageUploader.HasFile)
            {
                if (pageNameTextBox.Text != "")
                {
                    if (PageUploader.PostedFile.ContentType == "application/pdf")
                    {
                        if (PageUploader.PostedFile.ContentLength < 26214400)
                        {
                            string filename = Path.GetFileName(PageUploader.FileName);
                            string folderUrl = Server.MapPath("~/Resources\\") + game.GameID;
                            string url = folderUrl + "\\" + filename;

                            if (!Directory.Exists(folderUrl)) Directory.CreateDirectory(folderUrl);

                            //If a file already exists, delete it and overwrite
                            if (File.Exists(url))
                            {
                                File.Delete(url);
                                PageUploader.SaveAs(url);
                                Session["message"] = new Message("File already exists, overwrote with provided file.", System.Drawing.Color.Green);
                            }
                            //Else make the new file, and add it to the database and pages list
                            else
                            {
                                //Upload new file
                                PageUploader.SaveAs(url);

                                //Make CustomPage at end of existing pages
                                CustomPage page = new CustomPage();
                                page.PageName = pageNameTextBox.Text;
                                page.GameID = game.GameID;
                                page.PageURL = "Resources/" + game.GameID + "/" + PageUploader.FileName;
                                page.SortIndex = pages.Pages.Count;

                                PagesTable pagesTable = new PagesTable(new DatabaseConnection());
                                int pageID = pagesTable.insertCustomPage(page);
                                if (pageID > 0) page.PageID = pageID;

                                //Save Content and add to table
                                pageTable.saveContentChanges();
                                pages.Pages = pageTable.getContent();

                                pages.Pages.Add(page.SortIndex, page);

                                Session["message"] = new Message("Upload successful!", System.Drawing.Color.Green);
                            }
                        }
                        else Session["message"] = new Message("Upload failed: The file has to be less than 25 mb!", System.Drawing.Color.Red);
                    }
                    else  Session["message"] = new Message("Upload failed: Only PDF files are accepted!", System.Drawing.Color.Red);
                }
                else Session["message"] = new Message("Upload failed: A Page Name must be provided!", System.Drawing.Color.Red);
            }
            else Session["message"] = new Message("Upload failed: You must select a file to upload!", System.Drawing.Color.Red);
        }
        catch (Exception)
        {
            Session["message"] = new Message("Upload failed: Unable to upload this file.", System.Drawing.Color.Red);
        }

        this.Page.Session["savedContent"] = pages;

        //Reload page to clear any nonsense before loading
        this.Page.Response.Redirect("CustomPageTool");
    }
}