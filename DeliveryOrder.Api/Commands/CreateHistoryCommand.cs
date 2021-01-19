using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateHistoryCommand : IRequest<int>
    {
        ICreateHistory History { get; }
        public CreateHistoryCommand() { }
        public CreateHistoryCommand(ICreateHistory history)
        {
            History = history;
        }
    }
}
