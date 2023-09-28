using doAn.database;
using doAn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace doAn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataConnect _dbConnect;
        public string _UserNameCookie;

        public List<DoctorModel> DoctorModel { get; private set; }

        public HomeController(ILogger<HomeController> logger, DataConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }
        
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookie = cclams.Value;
                }
            }

            if (_UserNameCookie != null)
            {
                var AcountUser = from us in _dbConnect.UserEntity
                                 where us.nameUser == _UserNameCookie
                                 select new UserModel()
                                 {
                                     idUser = us.idUser,
                                     nameUser = us.nameUser,
                                     passwordUser = us.passwordUser,
                                     phoneNumber = us.phoneNumber,
                                     emailUser = us.emailUser,
                                     statusJob = us.statusJob,
                                     isAdmin = us.isAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                    TempData["IdUser"] = us.idUser;
                    TempData["UserName"] = us.nameUser;
                    TempData["AccountPass"] = us.passwordUser;
                    TempData["CheckAdmin"] = us.isAdmin;
                }
            }
            var services = from s in _dbConnect.SevicesEntity
                           select new ServiceModal()
                           {
                               idService = s.idService,
                               descriptionServices = s.descriptionServices,
                               nameService = s.nameService
                           };

            var doctor = from s in _dbConnect.DoctorEntity
                         select new DoctorModel() 
                         {
                             idDoctor = s.idDoctor,
                             imgDoctor = s.imgDoctor,
                             nameDoctor = s.nameDoctor,
                             expertise = s.expertise
                         };
            var Model = new ModelCommon()
            {
                ServiceModal = services.ToList(),
                 DoctorModel = doctor.ToList(),
            };
            return View(Model);
        }

        // handle Login
        public IActionResult Login()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginRes([FromBody] UserModel userInfo)
        {
            var query = from us in _dbConnect.UserEntity
                        where us.nameUser == userInfo.nameUser && us.passwordUser == userInfo.passwordUser
                        select new UserModel()
                        {
                            idUser = us.idUser,
                            nameUser = us.nameUser,
                            passwordUser = us.passwordUser,
                            phoneNumber = us.phoneNumber,
                            emailUser = us.emailUser,
                            statusJob = us.statusJob,
                            isAdmin = us.isAdmin,
                        };

            var user = query.FirstOrDefault(x => x.nameUser == userInfo.nameUser
            && x.passwordUser == userInfo.passwordUser);
            if (user == null)
            {
                return Ok(2);
            }

            var clanims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.nameUser),

                new Claim(ClaimTypes.NameIdentifier, user.nameUser),
            };


            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    string s = cclams.Value;
                }
            }
            var identy = new ClaimsIdentity(clanims, CookieAuthenticationDefaults.AuthenticationScheme);

            var princical = new ClaimsPrincipal(identy);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princical);

            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookie = cclams.Value;
                }
            }
            return Redirect("/Home/Index");
        }
        public IActionResult Register()
        {
            
            return View();
        }

        [Authorize]
        public  IActionResult Appointment()
        {
            int idUser = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookie = cclams.Value;
                }
            }

            if (_UserNameCookie != null)
            {
                var AcountUser = from us in _dbConnect.UserEntity
                                 where us.nameUser == _UserNameCookie
                                 select new UserModel()
                                 {
                                     idUser = us.idUser,
                                     nameUser = us.nameUser,
                                     passwordUser = us.passwordUser,
                                     phoneNumber = us.phoneNumber,
                                     emailUser = us.emailUser,
                                     statusJob = us.statusJob,
                                     isAdmin = us.isAdmin,
                                 };
                foreach (var us in AcountUser)
                {
                    idUser = us.idUser;
                    TempData["IdUser"] = us.idUser;
                    TempData["UserName"] = us.nameUser;
                    TempData["AccountPass"] = us.passwordUser;
                    TempData["CheckAdmin"] = us.isAdmin;
                }
            }

            var users = from U in _dbConnect.UserEntity
                        where U.idUser == idUser
                        select new UserModel() 
                        {
                            idUser = U.idUser,
                            nameUser = U.nameUser,
                            emailUser = U.emailUser,
                        };


            var doctors = from D in _dbConnect.DoctorEntity
                          select new DoctorModel() 
                          {
                              idDoctor = D.idDoctor,
                              nameDoctor = D.nameDoctor,
                          };

            var Services = from S in _dbConnect.SevicesEntity
                           select new ServiceModal()
                           {
                               idService = S.idService,
                               nameService = S.nameService,
                           };

            var model = new ModelCommon
            {
                DoctorModel = doctors.ToList(),
                ServiceModal = Services.ToList(),
                UserModel = users.ToList(),

            };
            return View(model);
        }
      
        // handle register
        [HttpPost]
        public IActionResult RegisterUsers([FromBody] UserModel userModel)
        { 
            if(userModel.passwordUser != userModel.confirmPassword)
            {
                return Ok(2);
            }
            var newUser = new lhUserEntity
            {
                nameUser = userModel.nameUser,
                passwordUser = userModel.passwordUser,
                emailUser = userModel.emailUser,
                statusJob = "Custormer",
                phoneNumber = userModel.phoneNumber,
                isAdmin = false,
            };
            _dbConnect.UserEntity.Add(newUser);
            var lh = _dbConnect.SaveChanges();
            return Ok(lh);
        }

        [HttpPost]
        public IActionResult BookAppointment([FromBody] AppoinmentModel appoinmentModel)
        {
            var add = new lhAppoimentEntity
            {
                nameService = appoinmentModel.nameService,
                emailUser = appoinmentModel.emailUser,
                appointmentDate = DateTime.Now.ToString("dd/mm/yy"),
                nameDoctor = appoinmentModel.nameDoctor,
                nameUser = appoinmentModel.nameUser,

            };
            _dbConnect.AppoimentEntity.Add(add);
            var x = _dbConnect.SaveChanges();
            return Ok(x);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}