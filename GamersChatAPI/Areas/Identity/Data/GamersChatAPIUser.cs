using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GamersChatAPI.Areas.Identity.Data;

// Add profile data for application users by adding properties to the GamersChatAPIUser class
public class GamersChatAPIUser : IdentityUser
{
    public GamersChatAPIUser(string userName) : base(userName) { }
}

