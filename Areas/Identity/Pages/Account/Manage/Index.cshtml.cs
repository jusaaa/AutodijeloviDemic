// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutodijeloviDemic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutodijeloviDemic.Validation;

namespace AutodijeloviDemic.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
            [Required]
            [StringLength(15, MinimumLength = 3, ErrorMessage = "Korisničko ime može sadržavati najmanje 3, a najviše 15 karaktera!")]
            [RegularExpression(@"^[a-zA-Z0-9_.]+$", ErrorMessage = "Korisničko ime mogu sadržavati slova,brojevi,podvlaka i tačka!")]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Phone]
            [PhoneNumber(ErrorMessage = "Telefonski format nije ispravan!")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [StringLength(50, MinimumLength = 3, ErrorMessage = "Ime može sadržavati najmanje 3, a najviše 50 karaktera!")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [StringLength(50, MinimumLength = 3, ErrorMessage = "Prezime može sadržavati najmanje 3, a najviše 50 karaktera!")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [StringLength(150, ErrorMessage = "Adresa može imati najviše 150 karaktera!")]
            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var appUser = user as ApplicationUser;

            Input = new InputModel
            {
                UserName = user.UserName,
                PhoneNumber = phoneNumber,
                FirstName = appUser?.FirstName,
                LastName = appUser?.LastName,
                Address = appUser?.Address,
                City = appUser?.City,
                Country = appUser?.Country
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.UserName != user.UserName)
            {
                var userNameExists = await _userManager.FindByNameAsync(Input.UserName);
                if (userNameExists != null)
                {
                    ModelState.AddModelError(string.Empty, "User Name already taken.");
                    return Page();
                }

                user.UserName = Input.UserName;
                var setUserNameResult = await _userManager.UpdateAsync(user);
                if (!setUserNameResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Unexpected error when trying to set user name.");
                    return Page();
                }
            }


            var appUser = user as ApplicationUser;
            if (appUser != null)
            {
                appUser.FirstName = Input.FirstName;
                appUser.LastName = Input.LastName;
                appUser.Address = Input.Address;
                appUser.City = Input.City;
                appUser.Country = Input.Country;

                var updateResult = await _userManager.UpdateAsync(appUser);
                if (!updateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to update profile.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}