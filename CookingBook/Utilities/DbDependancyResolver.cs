using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac;
using DatabaseLib.Interfaces;
using DatabaseLib.DBClients;
using DatabaseLib.Access;


namespace CookingBook.Utilities
{
    class DbDependancyResolver
    {
        public static T Resolve<T,b>()
        {
            var builder = new ContainerBuilder();

            //
            builder.RegisterType<b>().As<IDbClient>().
                WithParameters(new Parameter[] { new NamedParameter("dbName", ""), new NamedParameter("dbUserName", ""), new NamedParameter("dbPassword", ""), new NamedParameter("dbAddress", MainWindow.DbPath) });
            builder.RegisterType<DbHandle>().As<IDbHandle>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var handle = scope.Resolve<T>();

                return handle;
            }
        }


    }
}
