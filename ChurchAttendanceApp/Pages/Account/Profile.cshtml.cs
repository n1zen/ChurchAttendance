using ChurchAttendanceApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChurchAttendanceApp.Pages.Account;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public ProfileModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    [BindProperty]
    public ProfileInput Input { get; set; } = new();

    [BindProperty]
    public PasswordInput PasswordChange { get; set; } = new();

    public class ProfileInput
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class PasswordInput
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }

        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm New Password")]
        public string? ConfirmNewPassword { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        Input = new ProfileInput
        {
            UserName = user.UserName!,
            Email = user.Email!
        };

        return Page();
    }

    public async Task<IActionResult> OnPostProfileAsync()
    {
        if (!ModelState.IsValid) return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        // update username
        if (user.UserName != Input.UserName)
        {
            var setUserName = await _userManager.SetUserNameAsync(user, Input.UserName);
            if (!setUserName.Succeeded)
            {
                foreach (var error in setUserName.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Page();
            }
        }

        // update email
        if (user.Email != Input.Email)
        {
            var setEmail = await _userManager.SetEmailAsync(user, Input.Email);
            if (!setEmail.Succeeded)
            {
                foreach (var error in setEmail.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return Page();
            }
        }

        await _signInManager.RefreshSignInAsync(user); // refresh cookie
        StatusMessage = "Profile updated successfully.";
        TempData["SuccessMessage"] = $"{user.UserName}'s profile has been updated successfully.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostPasswordAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound();

        if (string.IsNullOrEmpty(PasswordChange.CurrentPassword) ||
            string.IsNullOrEmpty(PasswordChange.NewPassword))
        {
            ModelState.AddModelError(string.Empty, "Please fill in all password fields.");
            return Page();
        }

        var result = await _userManager.ChangePasswordAsync(
            user,
            PasswordChange.CurrentPassword,
            PasswordChange.NewPassword);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Password changed successfully.";
        TempData["SuccessMessage"] = $"{user.UserName}'s Password has been updated successfully.";
        return RedirectToPage();
    }
}