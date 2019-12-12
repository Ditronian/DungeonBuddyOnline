using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for RequestRow
/// </summary>
public class RequestRow : TableRow
{
    private JoinRequest request;

    public RequestRow(JoinRequest request)
    {
        this.request = request;
    }

    public JoinRequest Request { get => request; set => request = value; }
}