using DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetAll(string userId);
        void Create(string userId, List<int> idList);
    }
}
