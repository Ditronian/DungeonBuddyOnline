using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private int userID;
    private string userName;
    private Byte[] password;

    public int UserID { get => userID; set => userID = value; }
    public string UserName { get => userName; set => userName = value; }
    public Byte[] Password { get => password; set => password = value; }
}