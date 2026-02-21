using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.AutoMapper
{
    public static class AutoMapperConfigration
    {
        public static void RegisterMappings(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DomainToViewModelProfile>();
                cfg.AddProfile<ViewModelToDomainProfile>();
            });
        }
    }
}
