using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;

namespace OctagonPlatform.PersistanceRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public IEnumerable<User> GetAllUsers(int partnerId)
        {
            try
            {
                return Table.Where(u => !u.Deleted && u.PartnerId == partnerId) //Seleccionar los que no esten borrados. Bloqueados sis
                    //.Include(x => x.Alerts)
                    //.Include(x => x.Reports)
                    .Include(x => x.Partner)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public UserFormViewModel RenderUserFormViewModel(int parentId)
        {
            //Revisar que dependencias tienen los usuarios para mostrar ademas de sus datos.
            // reportes y grupos.
            //alertas y notificaciones.
            try
            {
                var viewModel = new UserFormViewModel()
                {

                    Partners = Context.Partners.ToList(),
                    Status = StatusType.Status.Active,
                    Partner = Context.Partners.SingleOrDefault(x => x.Id == parentId),
                    SetOfPermissions = Context.SetOfPermissions.Include("Permissions").ToList()
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Permission> AddPermissionToUser(string[] permissions)
        {
            try
            {
                var permissionList = new List<Permission>();
                if (permissions == null) return permissionList;
                foreach (var t in permissions)
                {
                    var convertId = Convert.ToInt32(t);
                    permissionList.Add(Context.Permissions.FirstOrDefault(c => c.Id == convertId));
                }
                //si viene null se envia la instancia vacia.
                return permissionList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserEditFormViewModel UserToEdit(int id)
        {
            try
            {
                var result = Table.Where(x => x.Id == id)
                    .Include("Permissions")
                    .Include(x => x.Partner)
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
                        Partner = result.Partner,
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveUser(UserFormViewModel viewModel, string action)
        {
            try
            {

                if (action == "Edit")
                {
                    var user = Table.Include("Permissions").SingleOrDefault(c => c.Id == viewModel.Id && !c.Deleted);

                    if (user == null) throw new Exception("User does not exist in our records");
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
                else
                {
                    //pongo en single y con el delete = false para que cuando se seleccione un userName y existe dos usuarios iguales con delete true, el single da un Exception por venir mas de dos. 
                    var user = Table.SingleOrDefault(
                        x => x.UserName == viewModel.UserName || x.Email == viewModel.Email);
                    if (user != null && !user.Deleted) throw new Exception("User already exists in our records!!!");

                    if (user != null && user.Deleted)
                        Table.Remove(user);

                    var key = Cryptography.GenerateKey();
                    var hash = Cryptography.EncodePassword(viewModel.Password, key);

                    user = new User()
                    {
                        PartnerId = viewModel.PartnerId,
                        Email = viewModel.Email.Trim(),
                        LastName = viewModel.LastName.Trim(),
                        Name = viewModel.Name.Trim(),
                        Key = key,
                        Password = hash, // crear metodo privado que haga hash de la BD
                        Phone = viewModel.Phone.Trim(),
                        Status = viewModel.Status,
                        UserName = viewModel.UserName.Trim(),
                        IsLocked = viewModel.IsLocked,
                        Permissions = viewModel.Permissions,
                    };

                    Add(user);




                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Error Creating or Editing User");
            }
        }

        public User UserDetails(int id)
        {
            try
            {
                var userDetails = Table.Where(x => x.Id == id && !x.Deleted)
                    .Include(x => x.Partner)
                    .Include(x => x.Permissions)
                    .Include(x => x.BankAccounts)
                    .Include(x => x.Terminals)
                    .FirstOrDefault();

                return userDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel)
        {
            try
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
                    Partner = Context.Partners.SingleOrDefault(x => x.Id == viewModel.PartnerId),
                    Partners = Context.Partners,
                    Phone = viewModel.Phone,
                    Status = viewModel.Status,
                    UserName = viewModel.UserName,
                    SetOfPermissions = viewModel.SetOfPermissions,
                    Error = viewModel.Error,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> Search(string search, int partnerId)
        {
            try
            {
                var users = Table.Where(c => !c.Deleted && c.PartnerId == partnerId &&
                                             (c.Name.Contains(search) || c.Partner.BusinessName.Contains(search)))
                    .Include(x => x.Partner)
                    .ToList();

                //List<BAccountFVModel> viewModel = new List<BAccountFVModel>();

                //foreach (var item in bankAccounts)
                //{   //creado porque no se puede mapear una lista de tipos de objetos. Solo se mapea un tipo de objeto.
                //    viewModel.Add(Mapper.Map<BankAccount, BAccountFVModel>(item));
                //}

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private ICollection<BankAccount> CreateListBankAccount(string[] bankAccounts)
        {
            try
            {
                List<BankAccount> bankAccountList = new List<BankAccount>();
                if (bankAccounts == null) return bankAccountList;
                foreach (var t in bankAccounts)
                {
                    var convertId = Convert.ToInt32(t);
                    bankAccountList.Add(Context.BankAccounts.FirstOrDefault(c => c.Id == convertId));
                }
                return bankAccountList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserBAViewModel AddBankAccountToUser(string userId, string bankAccountId)
        {
            try
            {
                UserBAViewModel userBAViewModel = new UserBAViewModel();
                User user = new User();
                var userIdConvert = Convert.ToInt32(userId);
                var bankAccountIdConvert = Convert.ToInt32(bankAccountId);

                if (!string.IsNullOrEmpty(userId) && (userIdConvert > 0) &&
                    (!string.IsNullOrEmpty(bankAccountId))) //VALIDO QUE NO ESTE VACIO  
                {
                    user = Table
                        .Include(c => c.BankAccounts)
                        .Single(c => c.Id == userIdConvert);
                    BankAccount bankAccount = GetBankAccountById(bankAccountIdConvert);
                    if (!user.BankAccounts.Contains(bankAccount)
                    ) //si el usario no contiene esa cuenta de banco se la add.
                    {
                        user.BankAccounts.Add(bankAccount);
                        Edit(user);
                    }
                }
                userBAViewModel = Mapper.Map<User, UserBAViewModel>(user);
                userBAViewModel.UserId = userIdConvert;
                return userBAViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private BankAccount GetBankAccountById(int bankAccountId)
        {
            try
            {
                return Context.BankAccounts.Single(c => c.Id == bankAccountId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserBAViewModel GetAllBankAccount(string userId, bool toAttach)
        {
            try
            {
                List<BankAccount> bankAccounts = new List<BankAccount>();
                UserBAViewModel userBAViewModel = new UserBAViewModel();

                if (toAttach) //selecciono cuentas de bancos para usuarios
                {
                    int userIdConverted = Convert.ToInt32(userId);
                    User user = Table
                        .Include(c => c.BankAccounts)
                        .Single(c => c.Id == userIdConverted);

                    bankAccounts = user.BankAccounts.Select(c => c).ToList();
                }
                else //SINO, selecciono todas las cuentas de usuario.
                {
                    bankAccounts = Context.BankAccounts
                        .Where(c => c.Deleted == false).ToList();
                }
                //Mapeo que se le hace a los dos casos.
                userBAViewModel.UserId = Convert.ToInt32(userId);
                userBAViewModel.BankAccounts = bankAccounts;

                return userBAViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserBAViewModel DeAttachBankAccountToUser(int userId, int bankAccountId)
        {
            //pendiente por si se decide quitar mas de una cuenta de banco en una sola accion. por el momento voene vacio.
            //List<BankAccount> bankAccountsList = CreateListBankAccount(bankAccounts).ToList();
            try
            {
                User user = Table
                    .Include(c => c.BankAccounts)
                    .Single(c => c.Id == userId);

                var bankToRemove = user.BankAccounts.FirstOrDefault(c => c.Id == bankAccountId);
                if (bankToRemove != null)
                {
                    user.BankAccounts.Remove(bankToRemove);
                    Edit(user);
                }

                UserBAViewModel userBAViewModel = Mapper.Map<User, UserBAViewModel>(user);
                userBAViewModel.UserId = userId;

                return userBAViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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