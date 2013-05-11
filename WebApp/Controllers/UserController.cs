namespace OpenCat.Controllers
{
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using OpenCat.Data;
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    public class UserController : Controller
    {
        private UserRepository Repository { get; set; }

        public UserController()
        {
            Repository = new UserRepository();
        }

        private User EnsureUserExists(IAuthenticationResponse response)
        {
            var user = Repository.Read().SingleOrDefault(u => u.identifier == response.ClaimedIdentifier.ToString());
            if (user == null)
            {
                var claim = response.GetExtension<ClaimsResponse>();

                user = new User
                {
                    identifier = response.ClaimedIdentifier.ToString(),
                    name = claim.FullName,
                    email = claim.Email
                };

                return Repository.Create(user);
            }
            return user;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/");
        }

        public ActionResult Login()
        {
            var openid = new OpenIdRelyingParty();
            IAuthenticationResponse response = openid.GetResponse();

            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var user = EnsureUserExists(response);
                        FormsAuthentication.RedirectFromLoginPage(
                            user.email, false);
                        break;
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("loginIdentifier",
                            "Login was cancelled at the provider");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("loginIdentifier",
                            "Login failed using the provided OpenID identifier");
                        break;
                }
            }

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string loginIdentifier, string email, string password)
        {
            if (String.IsNullOrEmpty(loginIdentifier))
            {
                if (Repository.Verify(email, password))
                {
                    FormsAuthentication.RedirectFromLoginPage(email, false);
                }
                else
                {
                    ViewData["Message"] = "Authentication failed";                    
                }
                return View();
            }
            else if (!Identifier.IsValid(loginIdentifier))
            {
                ViewData["Message"] = "The specified login identifier is invalid"; 
                return View();
            }
            else
            {
                var openid = new OpenIdRelyingParty();
                IAuthenticationRequest request = openid.CreateRequest(
                    Identifier.Parse(loginIdentifier));

                // Require some additional data
                request.AddExtension(new ClaimsRequest
                {
                    BirthDate = DemandLevel.NoRequest,
                    Email = DemandLevel.Require,
                    FullName = DemandLevel.Require
                });

                return request.RedirectingResponse.AsActionResult();
            }
        }
    }
}
