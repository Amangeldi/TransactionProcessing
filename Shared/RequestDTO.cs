using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class RequestDTO
    {
        public string Client { get; set; } = null!;
        public int TransactionsCount { get; set; }
    }
}
