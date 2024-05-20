using MediatR;

namespace SampleAppBPC3.Commands
{
    public class DeleteStudentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }


}
