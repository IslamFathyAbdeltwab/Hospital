using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Repositroyes.Interfaces
{
    public interface IDbInitlizer
    {
        Task Initialize();
        Task SeedData();
    }
}
