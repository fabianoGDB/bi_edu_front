using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Importador.Front.Enum
{
    public enum ImportStatusCode
    {
        Pending = 0,
        Processing = 1,
        Finished = 2,
        Error = 3
    }

    public enum StageStatus
    {
        Waiting = 0,
        Running = 1,
        Done = 2,
        Error = 3
    }
}