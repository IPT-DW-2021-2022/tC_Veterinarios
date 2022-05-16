// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using Veterinarios.Data;
using Veterinarios.Models;

namespace Veterinarios.Areas.Identity.Pages.Account {
   public class RegisterModel : PageModel {

      private readonly SignInManager<ApplicationUser> _signInManager;
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly IUserStore<ApplicationUser> _userStore;
      private readonly IUserEmailStore<ApplicationUser> _emailStore;
      private readonly ILogger<RegisterModel> _logger;
      private readonly IEmailSender _emailSender;
      private readonly ApplicationDbContext _context;

      public RegisterModel(
          UserManager<ApplicationUser> userManager,
          IUserStore<ApplicationUser> userStore,
          SignInManager<ApplicationUser> signInManager,
          ILogger<RegisterModel> logger,
          IEmailSender emailSender,
          ApplicationDbContext context) {
         _userManager = userManager;
         _userStore = userStore;
         _emailStore = GetEmailStore();
         _signInManager = signInManager;
         _logger = logger;
         _emailSender = emailSender;
         _context = context;
      }

      /// <summary>
      /// este atributo contém os atributos que são enviados para a interface gráfica
      /// </summary>
      [BindProperty]
      public InputModel Input { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      public string ReturnUrl { get; set; }

      /// <summary>
      ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
      ///     directly from your code. This API may change or be removed in future releases.
      /// </summary>
      //     public IList<AuthenticationScheme> ExternalLogins { get; set; }


      /// <summary>
      /// como o INPUT é a 'variável' que 'leva' os atributos para a interface
      /// gráfica, a definição desses atributos é feita nesta classe
      /// </summary>
      public class InputModel {
         /// <summary>
         /// User name
         /// </summary>
         [Required]
         [EmailAddress]
         [Display(Name = "Email")]
         public string Email { get; set; }

         /// <summary>
         /// Password
         /// </summary>
         [Required(ErrorMessage = "Deve introduzir a sua {0}, por favor.")]
         [StringLength(20, ErrorMessage = "A {0} dever ter, pelo menos, {2} e um máximo de  {1} caracteres.", MinimumLength = 6)]
         [DataType(DataType.Password)]
         [Display(Name = "Password")]
         public string Password { get; set; }

         /// <summary>
         /// confirmação da password
         /// </summary>
         [DataType(DataType.Password)]
         [Display(Name = "Confirmar password")]
         [Compare("Password", ErrorMessage = "A password e a sua confirmação não são iguais.")]
         public string ConfirmPassword { get; set; }

         /// <summary>
         /// adicionar os dados do DONO, para serem enviados para a interface gráfica
         /// </summary>
         public Donos Dono { get; set; }

      } // fim da inner class 'InputModel'




      /// <summary>
      /// este método reage ao HTTP GET
      /// </summary>
      /// <param name="returnUrl"></param>
      /// <returns></returns>
      public void OnGet(string returnUrl = null) {
         ReturnUrl = returnUrl;
         //   ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      }



      /// <summary>
      /// Este método reage ao HTTP POST
      /// </summary>
      /// <param name="returnUrl"></param>
      /// <returns></returns>
      public async Task<IActionResult> OnPostAsync(string returnUrl = null) {

         returnUrl ??= Url.Content("~/");
         //     ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

         // avalia se os dados fornecidos pelo browser são bons.
         // Isto quer dizer, se respeitam as retrições do Model
         if (ModelState.IsValid) {

            // criar um objeto do tipo ApplicationUser
            var user = CreateUser();

            // atribuir a data+hora da criação do utilizador
            user.DataRegisto = DateTime.Now;

            //user.UserName = Input.Email;
            //user.NormalizedEmail = Input.Email.ToUpper();
            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            // adição do email aos dados do user
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            // criação efetiva do utilizador
            var result = await _userManager.CreateAsync(user, Input.Password);

            // avaliação se houve, ou não, sucesso na criação do utilizador
            if (result.Succeeded) {

               _logger.LogInformation("User created a new account with password.");

               // adicionar o utilizador à Role Cliente
               await _userManager.AddToRoleAsync(user, "Cliente");

               //**********************************************************
               // Não esquecer que ainda é necessário guardar os dados do DONO na BD
               //**********************************************************
               // atualizar o objeto do Input.Dono cos os dados em falta
               Input.Dono.Email = Input.Email;
               Input.Dono.UserId = user.Id; // este atributo será utilizado para 
                                            // ligar as duas bases de dados
                                            // a base de dados de negócio
                                            // e a base de dados de autenticação

               try {
                  // adicionar o 'DONO' à base de dados
                  _context.Add(Input.Dono);
                  await _context.SaveChangesAsync();
               }
               catch (Exception) {
                  // se aqui chego é pq não se conseguiu escrever os dados do DONO
                  // na BD, mas o 'user' foi criado.
                  // quais as ações a executar????

                  // é preciso decidir...
                  //  throw;
               }



               // conjunto de código usado para a operação de validação do email
               // do novo utilizador...
               var userId = await _userManager.GetUserIdAsync(user);
               var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
               var callbackUrl = Url.Page(
                   "/Account/ConfirmEmail",
                   pageHandler: null,
                   values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                   protocol: Request.Scheme);

               await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

               if (_userManager.Options.SignIn.RequireConfirmedAccount) {
                  return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
               }
               else {
                  await _signInManager.SignInAsync(user, isPersistent: false);
                  return LocalRedirect(returnUrl);
               }
            }
            foreach (var error in result.Errors) {
               ModelState.AddModelError(string.Empty, error.Description);
            }
         }

         // If we got this far, something failed, redisplay form
         return Page();
      }

      private ApplicationUser CreateUser() {
         try {
            return Activator.CreateInstance<ApplicationUser>();
         }
         catch {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
         }
      }

      private IUserEmailStore<ApplicationUser> GetEmailStore() {
         if (!_userManager.SupportsUserEmail) {
            throw new NotSupportedException("The default UI requires a user store with email support.");
         }
         return (IUserEmailStore<ApplicationUser>)_userStore;
      }
   }
}
