using Microsoft.AspNet.Identity;
using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PMS.Custom_Classes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedCalims;

        public CustomAuthorizeAttribute(params string[] claims)
        {
            this.allowedCalims = claims;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var currentUser = "roshan.v";
            //var currentUser = httpContext.User.Identity.GetUserName();

            using (PMSEntities db = new PMSEntities())
            {
                List<string> allClaims = new List<string>();
                //AspNetUsers adminUser = (from u in db.AspNetUsers where u.UserName.Equals(currentUser) && u.IsActive.Equals(true) select u).FirstOrDefault<>();
                List<Claim> usrClaims = (from u in db.AspNetUsers
                                         join uc in db.UserClaims on u.Id equals uc.UserId
                                         join agc in db.AccessGroupClaims on uc.AccessGroupClaimId equals agc.Id
                                         join c in db.Claim on agc.ClaimId equals c.ClaimId
                                         where u.UserName.Equals(currentUser) && u.IsActive.Equals(true) && uc.IsActive.Equals(true)
                                         && agc.IsActive.Equals(true) && c.IsActive.Equals(true)
                                         select c).ToList();

                for(int i = 0; i < usrClaims.Count; i++)
                {
                    var claimsList = new JavaScriptSerializer().Deserialize<List<string>>(usrClaims[i].ClaimValue).ToList();

                    if(usrClaims[i].SubOperation != null)
                    {
                        for (int j = 0; j < claimsList.Count; j++)
                        {
                            allClaims.Add(claimsList[j] + "/" + usrClaims[i].SubOperation);
                        }
                    }
                    else
                    {
                        for (int j = 0; j < claimsList.Count; j++)
                        {
                            allClaims.Add(claimsList[j]);
                        }
                    }
                }

                if (allClaims.Any(c => allowedCalims.Any(ac => ac == c)))
                {
                    authorize = true;
                }
                else
                {
                    authorize = false;
                }

                return authorize;
            }
        }
    }
}