using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Security.Claims;
using WebClient.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.Configure<IdentityOptions>(configuration.GetSection("AzureAd"));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
               .AddMicrosoftIdentityWebApp(options =>
               {
                   configuration.Bind("AzureAD", options);
                   options.ResponseType = OpenIdConnectResponseType.Code;
                   options.Events ??= new OpenIdConnectEvents();
                   options.Events.OnTokenValidated += OnTokenValidatedFunc;
                   options.Events.OnAuthorizationCodeReceived += OnAuthorizationCodeReceivedFunc;
               });

async Task OnAuthorizationCodeReceivedFunc(AuthorizationCodeReceivedContext ctx)
{
    try
    {
        //string userId = ctx.Principal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        //var request = ctx.HttpContext.Request;
        //var currentUri = UriHelper.BuildAbsolute(request.Scheme, request.Host, request.PathBase, request.Path);
        //var credential = new ClientCredential(ctx.Options.ClientId, ctx.Options.ClientSecret);
        //var authContext = new AuthenticationContext(ctx.Options.Authority);
        //var result = await authContext.AcquireTokenByAuthorizationCodeAsync(
        //           ctx.ProtocolMessage.Code, new Uri(currentUri), credential, ctx.Options.Resource);
        //ctx.HandleCodeRedemption(result.AccessToken, result.IdToken);

        //var _authorityAPI = "https://login.microsoftonline.com/7e59cca2-3b49-4008-8ec5-c6558225d6cf";
        //HttpClient httpClient = new HttpClient();
        //TokenClientOptions tokenClientOptions = new TokenClientOptions()
        //{
        //    ClientId = ctx.Options.ClientId,
        //    ClientSecret = ctx.Options.ClientSecret,
        //    Address = string.Format("{0}/oauth2/v2.0/token", _authorityAPI)
        //};
        //var tokenClient = new TokenClient(httpClient, tokenClientOptions);

        //var tokenResponse = tokenClient.RequestAuthorizationCodeTokenAsync("c261c512-624d-40fa-90b1-fbdfc5a7c87a", "https://localhost:7167/signin-oidc").Result;
        //authorizationCtx.HandleCodeRedemption(tokenResponse.AccessToken, tokenResponse.IdentityToken);

        await Task.CompletedTask.ConfigureAwait(false);
    }
    catch (Exception e)
    {
        throw;
    }
   
}

async Task OnTokenValidatedFunc(TokenValidatedContext ctx)
{
    await Task.CompletedTask.ConfigureAwait(false);
}

builder.Services.AddRazorPages()
    //.AddMvcOptions(options =>
    //{
    //    var policy = new AuthorizationPolicyBuilder()
    //                  .RequireAuthenticatedUser()
    //                  .Build();
    //    options.Filters.Add(new AuthorizeFilter(policy));
    //})
    .AddMicrosoftIdentityUI();

HttpClient.DefaultProxy = new WebProxy(new Uri("http://localhost:8866"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
