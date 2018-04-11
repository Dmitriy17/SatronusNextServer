using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SatronusNextServer.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SatronusNextServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService interaction;
        //private readonly AccountService account;
        private readonly IUserRepository userRepository;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            this.interaction = interaction;
            this.userRepository = userRepository;
        }
    }
}
