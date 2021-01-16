using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Models;
namespace GoLogs.Services.DeliveryOrder.Api.BusinessLogic
{
    public interface IDOOrderLogic
    {
        Task CreateDOOrderAsync(DOOrder newDoOrder);
        Task<IList<DOOrder>> GetAllDOOrderAsync();
        Task<DOOrder> GetAllDOOrderByDoNumberAsync(string doOrderNumber);
        Task<DOOrder> GetDOOrderByIdAsync(int Id);
    }
}
