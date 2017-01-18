using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANCBE.Models.Identity
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {
    }
}
