using BearerToken.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace BearerToken
{
    public class StartUp
    {
        //Owin app Entry Point:Startup class--Configuration method.
        //IAppBuilder is an Interface that build PipeLine for request
        public void Configuration(IAppBuilder app)
        {
            //Cors(Cross-origin Resource Sharing ) enable so that Api Can get Request from different domain Like(React/Angular/Postman)
            //AllowAll give Access to all Domain
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //OAuthAuthorizationServerOptions হলো একটি configuration class,
            // যা বলে দেয় — তোমার OAuth Authorization Server(অর্থাৎ token issuing system) কীভাবে কাজ করবে।
            //“Token কোথায় দেওয়া হবে, কতক্ষণ valid থাকবে, কাদের দেওয়া যাবে, কে validate করবে…”

            //এইসব নিয়মগুলো সেট করা হয় OAuthAuthorizationServerOptions ক্লাসে।
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                //মানে হলো HTTPS ছাড়া(মানে HTTP দিয়েও) token request করা যাবে। এটা development / test environment এর জন্য ঠিক আছে। কিন্তু production এ এটা false রাখতে হয়,যেন শুধু secure HTTPS ব্যবহার হয়।
                TokenEndpointPath = new PathString("/token"),
                // /token endpoint তৈরি হচ্ছে যেখানে client তার username/password পাঠাবে token পাওয়ার জন্য।
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                //এটা হলো একটি custom class (যেটা তোমার প্রোজেক্টে থাকবে) যেখানে তুমি লিখবে কীভাবে user authenticate হবে।
//অর্থাৎ, এখানে username/password যাচাই করা হয় এবং token issue করা হয়।
                Provider = new AppAuthorizationServiceProvider()
            };
            //এই লাইনটি pipeline-এ token issuing server middleware যোগ করছে।
            //এটা / token endpoint handle করবে token generate korba
            app.UseOAuthAuthorizationServer(options);
            //এই লাইনটি pipeline - এ token validation middleware যোগ করছে।
//যখন কোনো API call করা হবে token সহ, তখন এই middleware token verify করে user কে authenticate করবে।
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //একটি নতুন Web API configuration object তৈরি হচ্ছে।
//এটার মাধ্যমে routing, formatters, filters ইত্যাদি কনফিগার করা হয়।
            HttpConfiguration httpConfiguration = new HttpConfiguration();
         //এই লাইনটি তোমার Web API routes এবং configuration register করছে।
            WebApiConfig.Register(httpConfiguration);
        }
    }
}