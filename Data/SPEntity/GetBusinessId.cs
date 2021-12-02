using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomRoles.Data.SPEntity
{
    [Keyless]
    public class GetBusinessId
    {
        public int BusinessId { get; set; }
    }
}
