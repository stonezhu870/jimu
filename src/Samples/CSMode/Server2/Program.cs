﻿using Jimu.Client;
using Jimu.Server;
using System;
using Autofac;
using Jimu;
using IServices;

namespace Server2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World, Service2 start...");

            var hostClientBuilder = new ApplicationClientBuilder(new Autofac.ContainerBuilder());
            IApplication hostClient = null;

            var hostServerBuilder = new ApplicationServerBuilder(new Autofac.ContainerBuilder());
            hostServerBuilder.AddServiceModule(x =>
            {
                x.Register(c => hostClient.Container.Resolve<IServiceProxy>()).As<IServiceProxy>();
            });
            var hostServer = hostServerBuilder.Build();
            hostServer.RunAction(x =>
            {
                hostClient = hostClientBuilder.Build();
                hostClient.Run();
            });

            hostServer.Run();
            Console.ReadLine();
        }
    }
}