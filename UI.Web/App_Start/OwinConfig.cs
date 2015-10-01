using Events;
using Microsoft.AspNet.Identity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Web.App_Start
{
        public class OwinConfig
        {
            public void Configuration(IAppBuilder app)
            {
                app.ExecuteAsync().Wait();

                app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
                //app.UseFacebookAuthentication(new FacebookAuthenticationOptions
                //{
                //    AppId = FacebookConfigurationSection.Recruiting.AppId,
                //    AppSecret = FacebookConfigurationSection.Recruiting.AppSecret,
                //    Provider = new FacebookAuthProvider()
                //});
            }
    }
}
