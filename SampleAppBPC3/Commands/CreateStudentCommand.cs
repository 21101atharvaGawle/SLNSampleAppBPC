using MediatR;
using Persistence.Models;

namespace SampleAppBPC3.Commands
{
    public class CreateStudentCommand : IRequest<Unit>
    {
        public StudentDTO Student { get; set; }
    }

}
