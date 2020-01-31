using Microsoft.AspNetCore.Identity;
using MedAidAPI.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAidAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAidAPI
{
    public class SeedDataInitializer
    {
        public DbContextOptionsBuilder<MedAidAPIContext> optionsBuilder = new DbContextOptionsBuilder<MedAidAPIContext>();

        public async Task SeedData(UserManager<MedAidAPIUser> userManager)
        {
            SeedUsers(userManager);
            await SeedLocationTypes();
            await SeedAlarmTypes();
            await SeedMedicalStores();
            await SeedMedicines();
        }

        private async Task SeedMedicines()
        {
            using (var _medAidAPIContext = new MedAidAPIContext(optionsBuilder.Options))
            {
                if (!_medAidAPIContext.Medicines.Any())
                {
                    string text_MedicineList = System.IO.File.ReadAllText(@"D:\zubair\Personal\FYP\MedAid_API\RestFullAPI\MedicineList.txt");
                    string[] MedicineNames = text_MedicineList.ToString().Trim().Split(':');
                    List<Medicine> Medicines = new List<Medicine>();
                    Random random = new Random();
                    foreach (var medicine in MedicineNames)
                    {
                        Medicines.Add(new Medicine()
                        {
                            Name = medicine.Replace("\r", string.Empty).Replace("\n", string.Empty),
                            RetailPrice = random.Next(5, 2000),
                            AvailableQuantity = random.Next(5, 2000),
                            StoreId = random.Next(1, 5),
                            SaltFormula = "",
                            Brand = "MedAid",
                        });
                    }
                    _medAidAPIContext.Medicines.AddRange(Medicines);
                    await _medAidAPIContext.SaveChangesAsync();
                }
            }


        }

        private async Task SeedMedicalStores()
        {
            using (var _medAidAPIContext = new MedAidAPIContext(optionsBuilder.Options))
            {
                if (!_medAidAPIContext.Stores.Any())
                {
                    List<Store> Stores = new List<Store>()
                    {
                        new Store{ StoreName= "Servaid", StoreContactNo = "03228045900", StoreAddress = "Test Address", StoreLatLong = "21145514.23:45.332", StoreEmailAddress = "mzubairshakoor@hotmail.com"},
                        new Store{ StoreName= "Clinix", StoreContactNo = "03228045900", StoreAddress = "Test Address", StoreLatLong = "485.23:8745.332", StoreEmailAddress = "mzubairshakoor@hotmail.com"},
                        new Store{ StoreName= "Total Care", StoreContactNo = "03228045900", StoreAddress = "Test Address", StoreLatLong = "-2675.23:45.332", StoreEmailAddress = "mzubairshakoor@hotmail.com"},
                        new Store{ StoreName= "Ali Pharmcy", StoreContactNo = "03228045900", StoreAddress = "Test Address", StoreLatLong = "54519.23:55.332", StoreEmailAddress = "mzubairshakoor@hotmail.com"},
                        new Store{ StoreName= "Fazal Din", StoreContactNo = "03228045900", StoreAddress = "Test Address", StoreLatLong = "-2245.23:1245814.332", StoreEmailAddress = "mzubairshakoor@hotmail.com"},
                    };

                    _medAidAPIContext.Stores.AddRange(Stores);
                    await _medAidAPIContext.SaveChangesAsync();
                }
            }
        }

        private async Task SeedAlarmTypes()
        {
            using (var _medAidAPIContext = new MedAidAPIContext(optionsBuilder.Options))
            {
                if (!_medAidAPIContext.AlarmTypes.Any())
                {
                    List<AlarmType> AlarmTypes = new List<AlarmType>()
                    {
                        new AlarmType{ Type = "Take Medicine"},
                        new AlarmType{ Type = "Purchase Medicine"}
                    };

                    _medAidAPIContext.AlarmTypes.AddRange(AlarmTypes);
                    await _medAidAPIContext.SaveChangesAsync();
                }

            }
        }

        private async Task SeedLocationTypes()
        {
            using (var _medAidAPIContext = new MedAidAPIContext(optionsBuilder.Options))
            {
                if (!_medAidAPIContext.LocationTypes.Any())
                {
                    List<LocationType> LocationTypes = new List<LocationType>()
                    {
                        new LocationType{ Type = "Home"},
                        new LocationType{ Type = "Work"},
                        new LocationType{ Type = "Remote"}
                    };

                    _medAidAPIContext.LocationTypes.AddRange(LocationTypes);
                    await _medAidAPIContext.SaveChangesAsync();
                }
            }
        }

        public void SeedUsers(UserManager<MedAidAPIUser> userManager)
        {
            if (userManager.FindByNameAsync("zubair").Result == null)
            {
                MedAidAPIUser user = new MedAidAPIUser();
                user.UserName = "zubair";
                user.Email = "zubair.shakoor@arbisoft.com";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync
                (user, "st#1Zubair").Result;
            }


            if (userManager.FindByNameAsync("shehriyar").Result == null)
            {
                MedAidAPIUser user = new MedAidAPIUser();
                user.UserName = "shehriyar";
                user.Email = "shehriyar.zafar@arbisoft.com";
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync
                (user, "st#1Zubair").Result;
            }
        }
    }
}
