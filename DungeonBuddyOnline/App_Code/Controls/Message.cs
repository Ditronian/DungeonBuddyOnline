using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Message
/// </summary>
public class Message
{
    private string text;
    private Color color;

    public Message(string text, Color color)
    {
        this.text = text;
        this.color = color;
    }

    public string Text { get => text; set => text = value; }
    public Color Color { get => color; set => color = value; }
}