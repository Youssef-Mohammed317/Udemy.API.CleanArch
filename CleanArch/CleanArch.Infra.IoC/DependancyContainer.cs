using AutoMapper;
using CleanArch.Application.AutoMapper;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.CommandHandler;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Bus;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.IoC
{
    public class DependancyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain InMemoryBus MediatR Layer
            services.AddScoped<IMediatRHandler, InMemoryBus>();

            // Domain Handlers
            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>, CreateCourseCommandHandler>();

            // CleanArch.Application Layer
            services.AddScoped<ICourseService, CourseService>();

            // CleanArch.Infra.Data Layer
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<UniversityDbContext>();

            // Auto Mapper
            AutoMapperConfigration.RegisterMappings(services);
        }
    }
}
