﻿using System.Net;
using FastCGI;
using VbHelpers;

namespace fastcgi_service;

internal abstract class Program
{
    private static string GetHttpHeaders()
    {
        return "HTTP/1.1 200 ok\nContent-Type:text/html\n\n";
    }
    private static void Main()
    {
        HtmlWriter htmlWriter = new();
        var app = new FCGIApplication();
        app.OnRequestReceived += (_, request) =>
        {
            request.WriteResponseASCII(GetHttpHeaders());
            request.WriteResponseASCII(htmlWriter.GetHtml(request));
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