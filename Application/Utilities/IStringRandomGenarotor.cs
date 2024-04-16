using Domain.ProductNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities
{
    public interface IStringRandomGenarotor<T>
        where T : class, IStringRandomGenarotor<T>
    {
        public string GenrateRandomString(Product p);
    }
}
