using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Autofac;
using Autofac.Core;
using MySql.Data.MySqlClient;
using Pl.Db.Model;

namespace Nelson.Dependency
{
    public class NelsonContainerBuilder
    {
        public static IContainer GetContainer()
        {
            var conn = "Server = mn02.webd.pl; Database = msit_pl; Uid = msit_pl; Pwd = dy-[zzV7xCGI; ";

            var builder = new ContainerBuilder();

            // Register individual components
            //builder.RegisterInstance(new TaskRepository()).As<ITaskRepository>();
            //builder.RegisterType<TaskController>();
            //builder.Register(c => new LogManager(DateTime.Now))
            //    .As<ILogger>();

            //// Scan an assembly for components
            //builder.RegisterAssemblyTypes(myAssembly)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();

            builder.Register(c => new MySqlConnection(conn)).As<IDbConnection>();
            builder.RegisterType<WebReader>();
            builder.RegisterType<ClubCommand>();
            builder.RegisterType<LeagueService>();
            builder.RegisterType<EventService>();


            return builder.Build();
        }
    }
}
