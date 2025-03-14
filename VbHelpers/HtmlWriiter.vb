
Imports System.Xml.Linq
Imports FastCgi

Public  Class HtmlWriter
    
    private ReadOnly _startInfo as ProcessStartInfo = New ProcessStartInfo(fileName := "/usr/bin/env")

    Public Sub New()
         With _startInfo
             .RedirectStandardOutput = True
             .UseShellExecute = False
             .CreateNoWindow = True
             end With
    End Sub

    Private Function RunCommand(command as String) As String
        Dim process =  new Process()
        _startInfo.Arguments = command
        process.StartInfo = Me._startInfo
        process.Start()
        Dim result = process.StandardOutput.ReadToEnd()
        return result
    End Function
    private  Function GetUname() As String
        Return RunCommand("uname -a")
    End Function
    
    public  Function GetHtml(r as Request) As String
        Dim params as List(Of String) = r.Parameters.Keys.ToList()
        Dim x as XElement =
<html>
    <head>
        <title>CGI Test</title>
    </head>
    <body>
        <h1> This is a CGI test. </h1>
        <p>This page is served by FastCGI implemented in .NET 8</p>
        <h2>System Info</h2>
        <h3><pre>uname -a</pre></h3>
        <pre><%= GetUname() %></pre>
        <h2>CGI Parameters</h2>
        <ul>
            <%= params.Select(Function(p) <li> <pre><%=p %>: <%= r.GetParameterASCII(p) %> </pre></li>) %>
        </ul>
    </body>
</html>
        Return x.ToString()
    End Function
End class
