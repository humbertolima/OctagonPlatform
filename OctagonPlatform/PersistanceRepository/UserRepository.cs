using AutoMapper;
using OctagonPlatform.Helpers;
using OctagonPlatform.Models;
using OctagonPlatform.Models.FormsViewModels;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace OctagonPlatform.PersistanceRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public IEnumerable<User> GetAllUsers(int partnerId)
        {
            try
            {
                var parent = Table.SingleOrDefault(x => x.Id == partnerId && !x.Deleted);
                if (parent == null) throw new Exception("Parent not found. ");

                return Table.Where(u => !u.Deleted && u.PartnerId == partnerId) //Seleccionar los que no esten borrados. Bloqueados sis
                    //.Include(x => x.Alerts)
                    //.Include(x => x.Reports)
                    .Include(x => x.Partner)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Users not found.");
            }
        }



        public UserFormViewModel RenderUserFormViewModel(int parentId)
        {
            //Revisar que dependencias tienen los usuarios para mostrar ademas de sus datos.
            // reportes y grupos.
            //alertas y notificaciones.
            try
            {
                var parent = Context.Partners.SingleOrDefault(x => x.Id == parentId && !x.Deleted);
                if (parent == null) throw new Exception("Parent not found. ");

                var viewModel = new UserFormViewModel()
                {

                    Partners = Context.Partners.Where(x => (x.Id == parentId || x.ParentId == parentId) && !x.Deleted).ToList(),
                    Status = StatusType.Status.Active,
                    Partner = parent,
                    SetOfPermissions = Context.SetOfPermissions.Include("Permissions").ToList()
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public ICollection<Permission> AddPermissionToUser(string[] permissions)
        {
            try
            {
                var permissionList = new List<Permission>();

                if (permissions == null) return permissionList;

                permissionList.AddRange(permissions.Select(t => Convert.ToInt32(t))
                    .Select(convertId => Context.Permissions
                    .FirstOrDefault(c => c.Id == convertId)));

                //si viene null se envia la instancia vacia.
                return permissionList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
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

                

                if (result == null) throw new Exception("User not found. ");
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
                        Partners = Context.Partners.Where(x => (x.Id == result.PartnerId || x.ParentId == result.PartnerId) && !x.Deleted).ToList(),
                        Permissions = result.Permissions,
                        Phone = result.Phone,
                        Status = result.Status,
                        UserName = result.UserName,
                        SetOfPermissions = Context.SetOfPermissions.Include("Permissions").Select(c => c).ToList(),
                        PermissionsAssigned = new List<PermissionAssigned>()
                    };


                    return userEdit;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public void SaveUser(UserFormViewModel viewModel, string action)
        {
            try
            {
                var currrentuser = Table.SingleOrDefault(
                    x => string.Equals(x.UserName.Trim().ToLower(), viewModel.UserName.Trim().ToLower()) || x.Email == viewModel.Email);
                if (currrentuser != null)
                {
                    if (currrentuser.Id != viewModel.Id)
                        throw new Exception("User already exists. ");

                }

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
                        {
                            var key = Cryptography.GenerateKey();
                            var hash = Cryptography.EncodePassword(viewModel.Password, key);
                            user.Password = hash;
                            user.Key = key;
                        }
                        if (viewModel.Permissions != null)
                            user.Permissions = viewModel.Permissions;

                        Edit(user);
                    }

                    
                }
                else
                {
                    //pongo en single y con el delete = false para que cuando se seleccione un userName y existe dos usuarios iguales con delete true, el single da un Exception por venir mas de dos. 
                    
                    var key = Cryptography.GenerateKey();
                    var hash = Cryptography.EncodePassword(viewModel.Password, key);

                    var userResult = new User()
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
                        IsLocked = viewModel.IsLocked
                        
                    };
                    if (viewModel.Permissions != null)
                        userResult.Permissions = viewModel.Permissions;
                    Add(userResult);




                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Please check the entered values. ");
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
                if(userDetails == null) throw new Exception("User not found. ");
                return userDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                var user = Table.SingleOrDefault(x => x.Id == id && !x.Deleted);
                if(user == null) throw new Exception("User not found. ");
                Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public UserFormViewModel InitializeNewFormViewModel(UserFormViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("Model not found.");
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
                    Partners = Context.Partners.Where(x =>(x.Id == viewModel.PartnerId || x.ParentId == viewModel.PartnerId) && !x.Deleted).ToList(),
                    Phone = viewModel.Phone,
                    Status = viewModel.Status,
                    UserName = viewModel.UserName,
                    SetOfPermissions = viewModel.SetOfPermissions,
                    Permissions = new List<Permission>(),
                    Error = viewModel.Error
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
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
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        private ICollection<BankAccount> CreateListBankAccount(string[] bankAccounts)
        {
            try
            {
                var bankAccountList = new List<BankAccount>();
                if (bankAccounts == null) return bankAccountList;

                bankAccountList.AddRange(bankAccounts.Select(t => Convert.ToInt32(t))
                    .Select(convertId => Context.BankAccounts
                    .FirstOrDefault(c => c.Id == convertId)));

                return bankAccountList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public UserBAViewModel AddBankAccountToUser(string userId, string bankAccountId)
        {
            try
            {
                var user = new User();
                var userIdConvert = Convert.ToInt32(userId);
                var bankAccountIdConvert = Convert.ToInt32(bankAccountId);

                if (!string.IsNullOrEmpty(userId) && (userIdConvert > 0) &&
                    (!string.IsNullOrEmpty(bankAccountId))) //VALIDO QUE NO ESTE VACIO  
                {
                    user = Table
                        .Include(c => c.BankAccounts)
                        .Single(c => c.Id == userIdConvert);
                    var bankAccount = GetBankAccountById(bankAccountIdConvert);
                    if (!user.BankAccounts.Contains(bankAccount)
                    ) //si el usario no contiene esa cuenta de banco se la add.
                    {
                        user.BankAccounts.Add(bankAccount);
                        Edit(user);
                    }
                }
                var userBaViewModel = Mapper.Map<User, UserBAViewModel>(user);
                userBaViewModel.UserId = userIdConvert;
                return userBaViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        private BankAccount GetBankAccountById(int bankAccountId)
        {
            try
            {
                var ba = Context.BankAccounts.Single(c => c.Id == bankAccountId && !c.Deleted);
                if(ba == null) throw new Exception("Bank account not found. ");
                return ba;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public UserBAViewModel GetAllBankAccount(string userId, bool toAttach)
        {
            try
            {
                List<BankAccount> bankAccounts;
                var userBaViewModel = new UserBAViewModel();

                if (toAttach) //selecciono cuentas de bancos para usuarios
                {
                    var userIdConverted = Convert.ToInt32(userId);
                    var user = Table
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
                userBaViewModel.UserId = Convert.ToInt32(userId);
                userBaViewModel.BankAccounts = bankAccounts;

                return userBaViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
            }
        }

        public UserBAViewModel DeAttachBankAccountToUser(int userId, int bankAccountId)
        {
            //pendiente por si se decide quitar mas de una cuenta de banco en una sola accion. por el momento voene vacio.
            //List<BankAccount> bankAccountsList = CreateListBankAccount(bankAccounts).ToList();
            try
            {
                var user = Table
                    .Include(c => c.BankAccounts)
                    .Single(c => c.Id == userId);

                var bankToRemove = user.BankAccounts.FirstOrDefault(c => c.Id == bankAccountId);
                if (bankToRemove != null)
                {
                    user.BankAccounts.Remove(bankToRemove);
                    Edit(user);
                }

                var userBaViewModel = Mapper.Map<User, UserBAViewModel>(user);
                userBaViewModel.UserId = userId;

                return userBaViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "User not found. ");
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