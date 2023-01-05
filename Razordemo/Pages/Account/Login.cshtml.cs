using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razordemo.Database;
using System.Data;

namespace Razordemo.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly servicec _servicec;
        public IDbConnection _db;
        [BindProperty]
        public LoginModel1 loginModel { get; set; }

        public LoginModel(servicec servicec)
        {
            _servicec = servicec;
        }

        public IConfiguration configuration { get; }

        [Route("index")]
        [Route("")]
        [Route("~/")]

        public ActionResult OnGet()
        {
            return Page();
        }

        public ActionResult OnPost()
        {

            var user = _servicec.hi(loginModel.UserName, loginModel.Password);
            LoginRequest loginRequest = new() { UserName = loginModel.UserName, Password = loginModel.Password };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7209/");
                var postTask = client.PostAsJsonAsync<LoginRequest>("https://localhost:7209/account/login", loginRequest);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync().Result;
                    return Redirect(readTask);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }

            if (user != null)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
