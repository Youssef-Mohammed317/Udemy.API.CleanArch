using CleanArch.Domain.Commands;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Models;
using MediatR;

namespace CleanArch.Domain.CommandHandler
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly ICourseRepository _courseRepository;

        public CreateCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
            };

            _courseRepository.Add(course);

            _courseRepository.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
