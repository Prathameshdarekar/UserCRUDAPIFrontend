using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<ActionResult> Index()
        {
            List<User> users = await FetchUsersFromApi();
            return View(users);
        }

        private async Task<List<User>> FetchUsersFromApi()
        {
            var response = await client.GetAsync("https://localhost:44384/api/users");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<List<User>>();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var response = await client.PostAsJsonAsync("https://localhost:44384/api/users", user);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error adding user.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            user.Id = id; // Ensure the user ID is set correctly
            var response = await client.PutAsJsonAsync($"https://localhost:44384/api/users/{id}", user);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error updating user.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var response = await client.DeleteAsync($"https://localhost:44384/api/users/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error deleting user.");
            }

            return RedirectToAction("Index");
        }
    }
}
