using Microsoft.AspNet.Identity.EntityFramework;
using MovieGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieGames.Contracts
{
    public interface IAdmin
    {
         IEnumerable<ApplicationUser> GetUserList();
         IEnumerable<IdentityRole> GetRolesList();
         bool IsAdminUser();
    }
}
