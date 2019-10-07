using AirBench.FormModels;
using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBench.Repository
{
    public interface IBenchApiRepository
    {
        Task<List<BenchList>> GetBenchList();
    }
}
