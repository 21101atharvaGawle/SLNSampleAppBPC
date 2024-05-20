using MediatR;
using Persistence.Models;

namespace SampleAppBPC3
{
    public class GetStudentByIdQuery : IRequest<StudentDTO>
    {
        public int Id { get; }

        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
