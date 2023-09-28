using doAn.Controllers;
using doAn.database;
using doAn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace doAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly DataConnect _dbConnect;
        public string _UserNameCookie;

        public List<DoctorModel> DoctorModel { get; private set; }

        public AdminController(ILogger<AdminController> logger, DataConnect dbConnect)
        {
            _logger = logger;
            _dbConnect = dbConnect;
        }
        [Authorize]
        [Area("Admin")]
        // handle check admin 
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookie = cclams.Value;
                }
            }

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
                if (us.isAdmin == false)
                {
                    return Redirect("/Home/Index");
                }
                else
                {
                    TempData["IdUser"] = us.idUser;
                    TempData["NameUser"] = us.nameUser;
                    TempData["PasswordUser"] = us.passwordUser;
                    TempData["PhoneNumber"] = us.phoneNumber;
                    TempData["EmailUser"] = us.emailUser;
                    TempData["StatusJob"] = us.statusJob;
                    TempData["CheckAdmin"] = us.isAdmin;
                  
                }
            }
            return View();
        }
        [Area("Admin")]
        public IActionResult TablePatients()
        {
            var appoinments = from A in _dbConnect.AppoimentEntity
                             select new AppoinmentModel() 
                             {
                                 idAppointments = A.idAppointments,
                                 appointmentDate = A.appointmentDate,
                                 nameUser = A.nameUser,
                                 emailUser = A.emailUser,
                                 nameService = A.nameService,
                                 nameDoctor = A.nameDoctor,
                             };
            List<AppoinmentModel> useappoinment = appoinments.ToList();
            return View(useappoinment);
        }
        [Authorize]
        [Area("Admin")]
        public IActionResult SetService()
        {
            var Services = from S in _dbConnect.SevicesEntity
                           select new ServiceModal()
                           {
                               idService = S.idService,
                               nameService = S.nameService,
                               descriptionServices = S.descriptionServices,
                               price = S.price,
                           };
            List<ServiceModal> sevice = Services.ToList();
            return View(sevice);
        }
        public IActionResult TableUsers()
        {
            var users = from u in _dbConnect.UserEntity
                        select new UserModel()
                        {
                            idUser = u.idUser,
                            nameUser = u.nameUser,
                            passwordUser = u.passwordUser,
                            phoneNumber = u.phoneNumber,
                            emailUser = u.emailUser,
                            statusJob = u.statusJob,
                            isAdmin = u.isAdmin,
                        };
            List<UserModel> user = users.ToList();
            return View(user);
        }
        public IActionResult ListOfDoctors()
        {
            var Doctors = from D in _dbConnect.DoctorEntity
                          select new DoctorModel()
                          {
                              idDoctor = D.idDoctor,
                              imgDoctor = D.imgDoctor,
                              nameDoctor = D.nameDoctor,
                              expertise = D.expertise,
                              infoContact = D.infoContact,
                          };
            List<DoctorModel> doctor = Doctors.ToList();
            return View(doctor);
        }

      
        

        
    }
}
