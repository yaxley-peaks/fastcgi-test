
Imports System.Xml.Linq
Imports FastCgi

Public MustInherit Class HtmlWriter
    public Shared Function GetHtml(r as Request) As String
        Dim params as List(Of String) = r.Parameters.Keys.ToList()
        Dim x as XElement =
<html>
    <head>
        <title>CGI Test</title>
    </head>
    <body>
        <h1> This is a CGI test. </h1>
        <p>This page is served by FastCGI implemented in .NET 8</p>
        <h2>CGI Parameters</h2>
        <ul>
            <%= params.Select(Function(p) <li> <pre><%=p %>: <%= r.GetParameterASCII(p) %> </pre></li>) %>
        </ul>
    </body>
</html>
        Return x.ToString()
    End Function
End class
