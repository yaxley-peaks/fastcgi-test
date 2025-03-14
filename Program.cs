using System.Net;
using FastCGI;
using VbHelpers;

namespace fastcgi_service;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = new FCGIApplication();
        app.OnRequestReceived += (sender, request) =>
        {
            request.WriteResponseASCII("HTTP/1.1 200 OK\n");
            request.WriteResponseASCII("Content-Type:text/html\n");
            request.WriteResponseASCII("\n\n");
            request.WriteResponseASCII(HtmlWriter.GetHtml());
            request.Close();
        };

        Console.WriteLine("Listening...");

        // Listen on ip 0.0.0.0 to appease docker
        app.Listen(new IPEndPoint(0L, 19000));
        while (!app.IsStopping)
            if (!app.Process())
                Thread.Sleep(1);
    }
}