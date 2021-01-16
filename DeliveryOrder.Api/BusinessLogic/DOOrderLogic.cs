using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;

using PostgresClient.Exceptions;
using SqlKata;
namespace GoLogs.Services.DeliveryOrder.Api.BusinessLogic
{
    public class DOOrderLogic : IDOOrderLogic
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;
        public DOOrderLogic(DOOrderContext context, IProblemCollector problemCollector, IMapper mapper)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        public async Task CreateDOOrderAsync(DOOrder newDoOrder)
        {
            var cid = newDoOrder.CargoOwnerId;
            var a = await _context.Doorders.AllAsync(new Query().Where(nameof(DOOrder.CargoOwnerId), cid));
            int lastId = a.Count +1;
            var doNumber = "DO" + lastId;
            newDoOrder.DoOrderNumber = doNumber;            
            try
            {
                await _context.Doorders.InsertAsync(newDoOrder);
            }
            catch (System.Exception)
            {
                throw;
            }
        }       

        public async Task<IList<DOOrder>> GetAllDOOrderAsync()
        {
            return await _context.Doorders.AllAsync(new Query().ForPage(1, 3));
        }

        public async Task<DOOrder> GetAllDOOrderByDoNumberAsync(string doOrderNumber)
        {
            return await _context.Doorders.FirstOrDefaultAsync(new Query().Where(nameof(DOOrder.DoOrderNumber), doOrderNumber));
        }

        public async Task<DOOrder> GetDOOrderByIdAsync(int Id)
        {           
                return await _context.Doorders.GetAsync(Id);                        
        }
    }
}
