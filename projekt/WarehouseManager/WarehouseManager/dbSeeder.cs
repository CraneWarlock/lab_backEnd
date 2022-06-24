using Microsoft.AspNetCore.Identity;
using WarehouseManager.Entites;

namespace WarehouseManager
{
    public class dbSeeder
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public dbSeeder(WarehauseManagerDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void Seed()
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Companies.Any())
            {
                var companies = GetCompanies();
                _dbContext.Companies.AddRange(companies);
                _dbContext.SaveChanges();
            }

            if (!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            } 
        }

        private IEnumerable<Company> GetCompanies()
        {
            var companies = new List<Company>()
            {
                new Company()
                {
                    Name = "Stockpile Comp.",
                    Description = "Stockpile Comp. is a polish company that specializes in material storage",
                    Address = "ul.Żwir 1, Kraków  30-300",
                    Locations = new List<Location>()
                    {
                        new Location()
                        {
                            LocationName = "Stockpile Comp. Location 1",
                            Description = "Main storage location",
                            Address = "ul.Żwir 2, Kraków 30-300",
                            Warehouses = new List<Warehouse>()
                            {
                                new Warehouse()
                                {
                                    WarehouseName = "Main Storage Hall",
                                    Description = "Storage hall for pallets and crates",
                                    StorageType = StorageType.Hall,
                                    CurrentCapacity = 10,
                                    MaximumCapacity = 100,
                                    WarehousesCargo = new List<WarehouseCargo>()
                                    {
                                        new WarehouseCargo()
                                        {
                                            CargoName = "Pallete of copper",
                                            CargoType = CargoType.Palette,
                                            Volume = 5,
                                            Description = "Pallete of flawless copper ingots"
                                        },
                                        new WarehouseCargo()
                                        {
                                            CargoName = "Crate of bismuth",
                                            CargoType = CargoType.Crate,
                                            Volume = 5,
                                            Description = "Crate of bismuth ingots"
                                        }
                                    }
                                },
                                new Warehouse()
                                {
                                    WarehouseName = "Main Storage Silo",
                                    Description = "Main silo for bulk materials",
                                    StorageType = StorageType.Silo,
                                    CurrentCapacity = 50,
                                    MaximumCapacity = 5000,
                                    WarehousesCargo = new List<WarehouseCargo>()
                                    {
                                        new WarehouseCargo()
                                        {
                                            CargoName = "Coal",
                                            CargoType = CargoType.BulkMaterial,
                                            Volume = 50,
                                            Description = "Loose carbon"
                                        }
                                    }
                                }
                            }
                        },
                        new Location()
                        {
                            LocationName = "Stockpile Comp. Location 2",
                            Description = "Secondary storage location",
                            Address = "ul.Żużel 3, Tarnowskie Góry 42-500",
                            Warehouses = new List<Warehouse>()
                            {
                                new Warehouse()
                                {
                                    WarehouseName = "Silo for liquids",
                                    Description = "Silo for liquid fuel",
                                    StorageType = StorageType.Silo,
                                    CurrentCapacity = 50,
                                    MaximumCapacity = 500,
                                    WarehousesCargo = new List<WarehouseCargo>()
                                    {
                                        new WarehouseCargo()
                                        {
                                            CargoName = "Petrol",
                                            CargoType = CargoType.Liquid,
                                            Volume = 50,
                                            Description = "Obvious fuel"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return companies;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }

        private User GetUsers()
        {
            string pass = "123123";
            var newUser = new User()
            {
                FirstName = "Admin",
                LastName = "Account",
                Email = "admin@admin.com",
                Department = "IT god",
                RoleId = 3
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, pass);
            newUser.PasswordHash = hashedPassword;
            return newUser;
        }


    }
}
