namespace OpenCat.Controllers
{
    using DotNetOpenAuth.Messaging;
    using DotNetOpenAuth.OpenId;
    using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
    using DotNetOpenAuth.OpenId.RelyingParty;
    using OpenCat.Data;
    using OpenCat.Models;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;

    public class UserController : Controller
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        private Repository<User> Repository { get; set; }

        public UserController()
        {
            Repository = new Repository<User>();
        }

        private void EnsureUserExists(IAuthenticationResponse response)
        {
            if (!Repository.Get().Any(u => u.identifier == response.ClaimedIdentifier.ToString()))
            {
                var claim = response.GetExtension<ClaimsResponse>();

                var user = new User
                {
                    identifier = response.ClaimedIdentifier.ToString(),
                    name = claim.FullName,
                    email = claim.Email
                };

                user.GeneratePassword();
                user.ComputeGravatar();

                Repository.Create(user);
            }
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
                        EnsureUserExists(response);
                        FormsAuthentication.RedirectFromLoginPage(
                            response.ClaimedIdentifier, false);
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
        public ActionResult Login(string loginIdentifier)
        {
            if (!Identifier.IsValid(loginIdentifier))
            {
                ModelState.AddModelError("loginIdentifier",
                            "The specified login identifier is invalid");
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
