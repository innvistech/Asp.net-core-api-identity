using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AngularWithCoreDemo.Helpers;
using AngularWithCoreDemo.Models;
using AngularWithCoreDemo.Models.AccountViewModels;
using AngularWithCoreDemo.Models.WebApi;
using AngularWithCoreDemo.Options;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AngularWithCoreDemo.Controllers.WebApi
{
    [Authorize]
    [Route("api/user")]
    public class UserController : DemoController
    {
        #region Properties

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public UserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginModel), Constants.Http200)]
        public async Task<IActionResult> Login(
            [FromBody]CredentialsBindingModel credentials
        )
        {
                if (!ModelState.IsValid) { throw new Exception(); }

                ClaimsIdentity identity = GetClaimsIdentity(credentials.UserName, credentials.Password).Result;
                if (identity == null) { throw new Exception("Please enter valid credentials"); }

                JwtTokenViewModel jwt = Tokens.GenerateJwt(identity, _jwtFactory, _jwtOptions).Result;
                LoginModel model = new LoginModel
                {
                    Content = jwt
                };
                return Success(model);
        }


        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(SuccessViewModel), Constants.Http200)]
        public IActionResult Register(
            [FromBody]RegisterUserBindingModel model
        )
        {
            // get the user to verifty
            var userToVerify = _userManager.FindByNameAsync(model.Email);
            if (userToVerify.Result != null) { throw new Exception("User Alrey Exist"); }

            ApplicationUser userIdentity = _mapper.Map<ApplicationUser>(model);
            IdentityResult result = _userManager.CreateAsync(userIdentity, model.Password).Result;
            var rModel = new SuccessViewModel
            {
                Detail = "User Succesfully Resgister"
            };
            return Success(rModel);
        }

        #endregion

        #region Helpers

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            var checkPassword = await _userManager.CheckPasswordAsync(userToVerify, password);
            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        #endregion
    }
}