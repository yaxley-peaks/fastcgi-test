using System.Net;
using FastCGI;

namespace fastcgi_service;


class Program
{
    static void Main(string[] args)
    {
        var app = new FCGIApplication();
        app.OnRequestReceived += (sender, request) =>
        {
            request.WriteResponseASCII("HTTP/1.1 200 OK\nContent-Type:text/html\n\nThis is some text!\n");
            request.Close();
        };
        
        Console.WriteLine("Listening...");
        
        // Listen on ip 0.0.0.0 to appease docker
        app.Listen(new IPEndPoint(0L, 19000));
        while (!app.IsStopping)
        {
            if (!app.Process())
            {
                Thread.Sleep(1);
            }
        }
        
    }
}