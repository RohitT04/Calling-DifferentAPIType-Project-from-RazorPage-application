using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Razordemo.Database;
using System.Data;
using System.Text;
namespace Razordemo.Pages.Account
{
    public class testModel : PageModel
    {

        HttpClientHandler _clientHandler = new HttpClientHandler();
        public IDbConnection _db;
        [BindProperty]
        public List<Profile> pro { get; set; }
        [BindProperty]
        public Profile pro1 { get; set; }
        [BindProperty]
        public List<Users> pro2 { get; set; }
        [BindProperty]
        public Users pro3 { get; set; }
        public testModel(IWebHostEnvironment appEnvironment)
        {
            _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyError) => { return true; };
            _appEnvironment = appEnvironment;
        }

        public async Task<ActionResult> OnGet()
        {
            using (var httpclient = new HttpClient(_clientHandler))
            {
                using (var response = await httpclient.GetAsync("http://localhost:5031/api/Profile/GetProfile"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    pro = JsonConvert.DeserializeObject<List<Profile>>(apiresponse);
                    return Page();
                }
            }
        }

        public async Task<ActionResult> OnGetList()
        {
            using (var httpclient = new HttpClient(_clientHandler))
            {
                using (var response = await httpclient.GetAsync("http://localhost:5222/weatherforecast/GETLIST"))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    pro2 = JsonConvert.DeserializeObject<List<Users>>(apiresponse);
                    return Page();
                }
            }
        }

        public async Task<ActionResult> OnPost(Profile p)
        {
            Profile _p = new Profile();

            using (var httpclient = new HttpClient(_clientHandler))
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpclient.PostAsync("http://localhost:5031/api/Profile/Deletereview", stringContent))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    _p = JsonConvert.DeserializeObject<Profile>(apiresponse);
                    return Page();
                }
            }
        }
        public async Task<ActionResult> OnPostYo(Users p)
        {
            using (var httpclient = new HttpClient(_clientHandler))
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpclient.DeleteAsync("http://localhost:5031/api/Profile/Deletereview/" + Convert.ToInt64(p.UserId.ToString())))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    return Page();
                }
            }
        }

        private readonly IWebHostEnvironment _appEnvironment;
        public async Task<ActionResult> OnPostHi(Users p)
        {
            Users _p = new Users();
            var files = HttpContext.Request.Form.Files;
            foreach (var Image in files)
            {
                if (Image != null && Image.Length > 0)
                {
                    var file = Image;
                    var uploads = Path.Combine(_appEnvironment.WebRootPath, "Uploads");
                    if (file.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            file.CopyToAsync(fileStream);                           //Here FileStream is used to get the image information
                                                                                    //emp.image = fileName;
                                                                                    //byte[] bArr = StreamToByteArray(fileStream);  //Convert Image into Array
                                                                                    //string result = string.Join("", bArr);                  //Convert Image Array into the String
                                                                                    // string base64String = Convert.ToBase64String(bArr);     //Convert Array into base64 string
                                                                                    // modelobj.adimage = base64String;
                        }
                    }
                }
            }
            using (var httpclient = new HttpClient(_clientHandler))
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(p), Encoding.UTF8, "application/json");
                using (var response = await httpclient.PostAsync("http://localhost:5031/api/Profile/Addlist", stringContent))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    _p = JsonConvert.DeserializeObject<Users>(apiresponse);
                    if (response.IsSuccessStatusCode)
                    {
                        return Page();
                    }
                    return Page();
                }
            }
        }
    }
}
