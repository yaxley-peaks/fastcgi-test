
Imports System.Xml.Linq

Public Class HtmlWriter
    public Shared Function GetHtml() As String
        Dim x as XElement =
                <html>
                <body>
                <h1> hello world </h1>
                <p>This is some text</p>
                </body>
                </html>
        Return x.ToString()
    End Function
End class
