using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.BL;
using UserProfile.Infrastructure;
using UserProfile.Infrastructure.DBModel;
using UserProfile.UnitTest.Mocks;

namespace UserProfile.UnitTest
{
    public class AutofacContainer
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProfileRepositoryTest>()
                .As<IRepository<DbProfileTextFile>>();
           
            builder.RegisterType<ProfileService<DbProfileTextFile>>()
                .As<IProfileService>()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(IRepository<DbProfileTextFile>) && pi.Name == "repository",
                               (pi, ctx) => ctx.Resolve<IRepository<DbProfileTextFile>>());
            

            return builder.Build();
        }
    }
}
