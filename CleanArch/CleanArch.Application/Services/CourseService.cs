using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Commands;
using CleanArch.Domain.Core.Bus;
using CleanArch.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMediatRHandler _bus;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepository courseRepository, IMediatRHandler bus, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public void Create(CourseViewModel course)
        {
            var createCourseCommand = _mapper.Map<CreateCourseCommand>(course);
            _bus.SendCommand(createCourseCommand);
        }

        public IEnumerable<CourseViewModel> GetCourses()
        {

            return _courseRepository.GetCourses().ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider);
        }
    }
}
