using Employee.Infrastructure.Repositories;
using Employee.Infrastructure.Repositories.Base;
using G5.Domain.Repositories;
using G5.Domain.Repositories.Base;
using G5.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.utils
{
    public static class InfrastructureExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {  
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IPermissionRepository, PermissionRepository>();
        }
    }
}
