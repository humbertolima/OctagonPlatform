using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OctagonPlatform.PersistanceRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
       
        public IEnumerable<User> GetAllUsers()
        {
            return Table.Where(u => u.Deleted == false)  //Seleccionar los que no esten borrados. Bloqueados sis
                                                         //.Include(x => x.Alerts)
                                                         //.Include(x => x.Reports)
                .Include(x => x.Partner)
                .ToList();
        }


        public UserFormViewModel RenderUserFormViewModel()
        {
            //Revisar que dependencias tienen los usuarios para mostrar ademas de sus datos.
            // reportes y grupos.
            //alertas y notificaciones.

            var viewModel = new UserFormViewModel()
            {
                Partners = Context.Partners.ToList(),

                SetOfPermissions = Context.SetOfPermissions.Include("Permissions").ToList()
            };

            return viewModel;
        }

        public ICollection<Permission> AddPermissionToUser(string[] permissions)
        {
            var permissionList = new List<Permission>();
            if (permissions != null) 
            {
                foreach (var t in permissions)
                {
                    var convertId = Convert.ToInt32(t);
                    permissionList.Add(Context.Permissions.FirstOrDefault(c => c.Id == convertId));
                }
            }
            //si viene null se envia la instancia vacia.
            return permissionList;
        }

        public UserEditFormViewModel UserToEdit(int id)
        {
            var result = Table
                .Include("Permissions")
                .Single(c => c.Id == id);

            var mapping = new MappingProfile();

            //mapping.CreateMap(result, userEdit);

            if (result == null) throw new System.Exception("User don't exist. ");
            {
                var userEdit = new UserEditFormViewModel()
                {
                    Email = result.Email,
                    Id = result.Id,
                    IsLocked = result.IsLocked,
                    LastName = result.LastName,
                    Name = result.Name,
                    PartnerId = result.PartnerId,
                    Partners = Context.Partners,
                    Permissions = result.Permissions,
                    Phone = result.Phone,
                    Status = result.Status,
                    UserName = result.UserName,
                    SetOfPermissions = Context.SetOfPermissions.Include("Permissions").Select(c => c).ToList(),
                    PermissionsAssigned = new List<PermissionAssigned>()
                };


                //foreach (var setPerm in userEdit.PermissionsAll)
                //{
                //    PermissionAssigned permissionA = new PermissionAssigned();

                //    permissionA.Selected = false;
                //    permissionA.Type = setPerm.Type;
                //    permissionA.Text = setPerm.Name;
                //    permissionA.Value = setPerm.Id.ToString();

                //    Permission permiso = userEdit.Permissions.FirstOrDefault(c => c.Id == setPerm.Id);

                //    if (permiso != null) { permissionA.Selected = true; }

                //    permissionA.Group = new SelectListGroup { Name = setPerm.SetOfPermissionId.ToString()}; //el setOf permiso seria las filas en el view de permisos

                //    userEdit.PermissionsAssigned.Add(permissionA);
                //};

                return userEdit;
            }
        }

        public void SaveUser(UserFormViewModel viewModel, string action)
        {

            if (action == "Edit")
            {
                var user = Table.Include("Permissions").SingleOrDefault(c => c.Id == viewModel.Id);

                if (user != null)
                {
                    user.Email = viewModel.Email.Trim();
                    user.LastName = viewModel.LastName.Trim();
                    user.Name = viewModel.Name.Trim();
                    user.Phone = viewModel.Phone.Trim();
                    user.Status = viewModel.Status;
                    user.UserName = viewModel.UserName.Trim();
                    user.IsLocked = viewModel.IsLocked;
                    user.PartnerId = viewModel.PartnerId;

                    if (!string.IsNullOrEmpty(viewModel.Password))
                        user.Password = viewModel.Password.Trim();

                    user.Permissions = viewModel.Permissions;

                    Edit(user);
                }
            }
            else if (action == "Create")
            {
                //pongo en single y con el delete = false para que cuando se seleccione un userName y existe dos usuarios iguales con delete true, el single da un Exception por venir mas de dos. 

                var user = new User()
                {
                    PartnerId = viewModel.PartnerId,
                    Email = viewModel.Email.Trim(),
                    LastName = viewModel.LastName.Trim(),
                    Name = viewModel.Name.Trim(),
                    Password = viewModel.Password.Trim(), // crear metodo privado que haga hash de la BD
                    Phone = viewModel.Phone.Trim(),
                    Status = viewModel.Status,
                    UserName = viewModel.UserName.Trim(),
                    IsLocked = viewModel.IsLocked,
                    Permissions = viewModel.Permissions,
                };

                Add(user);
            }
        }

        public User UserDetails(int id)
        {
            var userDetails = Table.Where(x => x.Id == id)
                .Include(x => x.Partner)
                .Include(x => x.Permissions)
                .Include(x => x.BankAccounts)
                .Include(x => x.Terminals)
                .FirstOrDefault();

            return userDetails;
        }

        public void DeleteUser(int id)
        {
            Delete(id);
        }

        public UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel)
        {
            if (viewModel.SetOfPermissions == null)
            {
                viewModel.SetOfPermissions = Context.SetOfPermissions.Include("Permissions").ToList();
            }
            return new UserFormViewModel()
            {
                Email = viewModel.Email,
                Id = viewModel.Id,
                IsLocked = viewModel.IsLocked,
                LastName = viewModel.LastName,
                Name = viewModel.Name,
                PartnerId = viewModel.PartnerId,
                Partners = Context.Partners,
                Phone = viewModel.Phone,
                Status = viewModel.Status,
                UserName = viewModel.UserName,
                SetOfPermissions = viewModel.SetOfPermissions,
                Error = viewModel.Error,
            };
        }

        public IEnumerable<User> Search(string search)
        {
            var users = Table.Where(c => !c.Deleted && (c.Name.Contains(search) || c.Partner.BusinessName.Contains(search)))
                .Include(x => x.Partner)
                .ToList();

            //List<BAccountFVModel> viewModel = new List<BAccountFVModel>();

            //foreach (var item in bankAccounts)
            //{   //creado porque no se puede mapear una lista de tipos de objetos. Solo se mapea un tipo de objeto.
            //    viewModel.Add(Mapper.Map<BankAccount, BAccountFVModel>(item));
            //}

            return users;
        }

        public IEnumerable<BankAccount> GetAllBankAccount()
        {
            IEnumerable<SelectListItem> result = _context.Cities
              .Where(c => c.StateId == stateId)
              .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //public List<UserBAViewModel> GetBAOfUser()
        //{
        //    var bankAccounts = Context.BankAccounts.ToList();
        //    //Sugerido por RSHARPER
        //    var viewModel = bankAccounts.Select(item => Mapper.Map<BankAccount, UserBAViewModel>(item)).ToList();

        //    return viewModel;
        //}
        //public async Task<User> Validate(User user)
        //{
        //    var result = await Context.Users.SingleAsync(c => c.UserName == user.UserName);
        //    return result;
        //}
    }
}