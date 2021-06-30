using System;
using System.Threading.Tasks;
using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Persistence.Interfaces;

namespace The_Foo_Bar_.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

             services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
  })
  .AddCookie()
  .AddOpenIdConnect("Auth0", options =>
  {
    options.Authority = $"https://{Configuration["Auth0:Domain"]}";
    options.ClientId = Configuration["Auth0:ClientId"];
    options.ClientSecret = Configuration["Auth0:ClientSecret"];
    options.ResponseType = "code";
    options.Scope.Clear();
    options.Scope.Add("openid profile email");
    options.CallbackPath = new PathString("/callback");
    options.ClaimsIssuer = "Auth0";
    options.Events = new OpenIdConnectEvents
    {
      OnRedirectToIdentityProviderForSignOut = (context) =>
        {
          var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

          var postLogoutUri = context.Properties.RedirectUri;
          if (!string.IsNullOrEmpty(postLogoutUri))
          {
            if (postLogoutUri.StartsWith("/"))
            {
              var request = context.Request;
              postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
            }
            logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
          }

          context.Response.Redirect(logoutUri);
          context.HandleResponse();

          return Task.CompletedTask;
        }
    };
  });

  services.AddHttpContextAccessor();
  services.AddTransient<IDataAccess, DataAccess>();
  services.AddTransient<IPostData, PostData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}