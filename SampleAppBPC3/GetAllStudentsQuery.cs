using MediatR;
using Persistence.Models;

namespace SampleAppBPC3
{
    public class GetAllStudentsQuery: IRequest<List<StudentDTO>>
    {
    }
}
