using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FashionNet.Modelos;
using FashionNet.Utilidades;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace FashionNet.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "La {0} debe ser al menos de {2} y máximo {1} caracteres de longitud.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Debe ingresar al menos un nombre")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "Debe ingresar el apellido")]
            public string Apellido { get; set; }

            [Phone]
            [Required(ErrorMessage = "Debe ingresar el nro de celular")]
            [Display(Name = "Nro. Celular")]
            public string NumeroCelular { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Usuario {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    Apellido = Input.Apellido,
                    PhoneNumber = Input.NumeroCelular,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //Validamos si los roles existen, sino los creamos
                    if (!await _roleManager.RoleExistsAsync(CNT.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(CNT.Admin));
                        await _roleManager.CreateAsync(new IdentityRole(CNT.Usuario));
                    }
                    //Obtenemos el rol seleccionado
                    string rol = Request.Form["radUsuarioRole"].ToString();

                    //Validamos si el rol seleccionado es Admin y si lo es lo agregamos
                    if (rol == CNT.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, CNT.Admin);
                    }
                    else
                    {
                        if (rol == CNT.Usuario)
                        {
                            await _userManager.AddToRoleAsync(user, CNT.Usuario);
                        }
                    }

                    _logger.LogInformation("Usuario creado exitosamente.");

                    //Agregamos el usuario a la base de datos
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
