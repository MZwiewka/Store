using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Store.Controllers
{
    [Authorize(Roles = "Administrator")]
    //[Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        private UserManager<User> userManager;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;
        private IUserValidator<User> userValidator;
        private RoleManager<IdentityRole> roleManager;
        private IWebHostEnvironment environment;

        public AdminController(IProductRepository repo, UserManager<User> uManager, IUserValidator<User> uValidator, IPasswordValidator<User> pValidator, IPasswordHasher<User> pHasher, RoleManager<IdentityRole> rManager, IWebHostEnvironment env)
        {
            repository = repo;
            userManager = uManager;
            userValidator = uValidator;
            passwordValidator = pValidator;
            passwordHasher = pHasher;
            roleManager = rManager;
            environment = env;
        }

        //Products

        public ViewResult ListProducts() => View(repository.Products);

        public ViewResult CreateProduct() => View("EditProduct", new Product());

        public ViewResult EditProduct(int productID) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == productID));

        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    string imagePath = $"{environment.ContentRootPath}\\MyStaticFiles\\Images\\Products\\{product.Name}";
                    Directory.CreateDirectory(imagePath);
                    product.ImagePath = $"/MyImages/Products/{product.Name}/picture.jpg";
                    using (FileStream x = new FileStream($"{imagePath}\\picture.jpg", FileMode.Create, FileAccess.Write))
                    {
                        img.CopyTo(x);

                    }
                }
                else if (product.ImagePath == null)
                {
                    product.ImagePath = "/MyImages/Products/default.png";
                }
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("ListProducts");
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            Product product = repository.DeleteProduct(productId);
            if (product != null)
            {
                TempData["message"] = $"{product.Name} was deleted";
            }
            return RedirectToAction("ListProducts");
        }

        //Roles

        public ViewResult ListRoles() => View(roleManager.Roles);

        public ViewResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<User> members = new List<User>();
            List<User> nonmembers = new List<User>();
            foreach (User user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }

            return View(new EditRole { Role = role, Members = members, NonMembers = nonmembers });
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(RoleModification model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    User user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    User user = await userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(ListRoles));
            }
            else
            {
                return await EditRole(model.RoleId);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return View("ListRoles", roleManager.Roles);
        }

        //Users

        public ViewResult ListUsers() => View(userManager.Users);

        public ViewResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUser model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email

                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("ListUsers");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string email, string password)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;

                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUsers");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found");
            }

            return View("ListUsers", userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}