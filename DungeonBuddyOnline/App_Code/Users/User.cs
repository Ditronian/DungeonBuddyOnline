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
    private string email;
    private bool isConfirmed = false;
    private DateTime dateRegistered;
    private DateTime lastLoggedIn;

    public int UserID { get => userID; set => userID = value; }
    public string UserName { get => userName; set => userName = value; }
    public Byte[] Password { get => password; set => password = value; }
    public string Email { get => email; set => email = value; }
    public bool IsConfirmed { get => isConfirmed; set => isConfirmed = value; }
    public DateTime DateRegistered { get => dateRegistered; set => dateRegistered = value; }
    public DateTime LastLoggedIn { get => lastLoggedIn; set => lastLoggedIn = value; }
}