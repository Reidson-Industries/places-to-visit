﻿using System;

namespace PlacesToVisit.ConsoleAppHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://*:1337/" : args[0];
            var appHost = new ConsoleAppHost()
                .Init()
                .Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadKey();
        }
    }
}
