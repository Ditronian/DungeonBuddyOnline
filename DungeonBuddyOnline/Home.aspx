<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="WebSite_Home" MasterPageFile="~/Main.master"%>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link rel="stylesheet" type="text/css" href="CSS/Home.css" />
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="BodyPlaceHolder">
        <section id="topBar">
            <div id="topBarLeft">
                <span id="logoSpan"> <label>DungeonBuddy Online</label></span>
            </div>
            <div id="topBarRight">
                <span id="gameNameLabel">Home</span>
            </div>
        </section>

        <div id="centerDiv" class="highlightDiv">
            <div id="innerDiv">
                <h1 class="pageHeader">Welcome to DungeonBuddy!</h1>
                <div id="normalText">
                    My name is David, and I am here to help guide you on your D&D travels.
                    <p></p>
                    <b class="Jeeves">What is DungeonBuddy?</b><br />
                    DungeonBuddy is a compilation of dynamic Web Tools created to help Dungeon Masters and Players run smooth games of Dungeons and Dragon 5e.
                    <p></p>
                    <br />
                    <b class="Jeeves">What Tools exist for Dungeon Masters?</b><br />
                    Making the lives of Dungeon Masters' easier is the main goal behind DungeonBuddy.  Once you have created a game, DungeonBuddy will allow you to create and save
                    your own Monster Encounters, generate random or custom NPCs and Magic Item Shops, manage your game's party, and more!  On top of all that, DungeonBuddy will also let you share your own
                    personal PDF files as seperate pages for your players!  Have you built your own World, but can't find an easy way to share it with your Players?  DungeonBuddy has you covered!
                    <p></p>
                    <br />
                    <b class="Jeeves">How do I Create a game?</b><br />
                    Creating a new game on DungeonBuddy and becoming a new Dungeon Master couldn't be easier!  Just click the 'Create Game' Link in the Left-Hand sidebar, enter in the required details, and create!
                    <p></p>
                    <br />
                    <b class="Jeeves">What can I do as a Player?</b><br />
                    DungeonBuddy acts as an interface between the Dungeon Master's Game content and the game's Players.  With this in mind, Players can view the Game's Party, and can edit their personal character's 
                    stats, along with being able to view all of the Game Pages that their Dungeon Master wants to make available to them.
                    <p></p>
                    <br />
                    <b class="Jeeves">How do I join a game?</b><br />
                    To join all you need to do is click on the 'Join Game' link in the Left-Hand sidebar.  Once you've found the game you would like to join, select it and enter some basic Character Information for your Dungeon Master
                     to consider.  Now just hit Submit Join Request and have your Dungeon Master accept the request from their Game's Party page!
                     <p></p>
                    <asp:Button ID="deleteAccountButton" runat="server" Text="Delete Account" CssClass="largeButton"  OnClientClick="return confirm('Are you sure you would like to delete your account?')" OnClick="deleteAccountButton_Click" BackColor="Firebrick" />
                    <br />
                    <asp:Label ID="angryLabel" runat="server" Text="&nbsp;"></asp:Label>
                    <br />
                    <footer>
                        <small>&copy; Copyright 2019, David A. Gereau</small>
                    </footer>
                </div>
            </div>
        </div>
</asp:Content>