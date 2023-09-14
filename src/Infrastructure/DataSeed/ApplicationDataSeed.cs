using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Management.Cars;
using Domain.Management.CarBrands;
using Domain.Management.CarModels;
using Domain.Management.CarCategories;
using Domain.Management.Offices;
using Domain.Management.Workers;
using Domain.Repositories;
using Domain.Sales.Extras;
using JsonNet.ContractResolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Persistence;

namespace Infrastructure.DataSeed;
public class ApplicationDataSeed
{
    public static async void Seed(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var _dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            var _unitOfWorkRepository = serviceScope.ServiceProvider.GetService<IUnitOfWork>();
            var _carBrandRepository = serviceScope.ServiceProvider.GetService<ICarBrandRepository>();
            var _carCategoryRepository = serviceScope.ServiceProvider.GetService<ICarCategoryRepository>();
            var _carModelRepository = serviceScope.ServiceProvider.GetService<ICarModelRepository>();
            var _carRepository = serviceScope.ServiceProvider.GetService<ICarRepository>();
            var _extraRepository = serviceScope.ServiceProvider.GetService<IExtrasRepository>();
            var _officeRepository = serviceScope.ServiceProvider.GetService<IOfficeRepository>();
            var _reservationDetailRepository = serviceScope.ServiceProvider.GetService<IReservationItemRepository>();
            var _reservationRepository = serviceScope.ServiceProvider.GetService<IReservationRepository>();
            var _workerRepository = serviceScope.ServiceProvider.GetService<IWorkerRepository>();

            _dbContext!.Database.EnsureCreated();
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var jsonRootPath = Path.Combine(path, "DataSeed\\JsonFiles");
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            //#region Office

            //if (!_dbContext.Set<Office>().ToListAsync().Result.Any())
            //{
            //    List<Office> data = new List<Office>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/Office.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<Office>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _officeRepository.AddAsync(x));
            //}


            //#endregion

            //#region Worker

            //if (!_dbContext.Set<Worker>().ToListAsync().Result.Any())
            //{
            //    List<Worker> data = new List<Worker>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/Worker.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<Worker>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _workerRepository.AddAsync(x));
            //}

            //#endregion

            

            //#region CarBrand

            //if (!_dbContext.Set<CarBrand>().ToListAsync().Result.Any())
            //{
            //    List<CarBrand> data = new List<CarBrand>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/CarBrand.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<CarBrand>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _carBrandRepository.AddAsync(x));
            //}

            //#endregion

            //#region CarCategory


            //if (!_dbContext.Set<CarCategory>().ToListAsync().Result.Any())
            //{
            //    List<CarCategory> data = new List<CarCategory>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/CarCategory.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<CarCategory>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _carCategoryRepository.AddAsync(x));
            //}

            //#endregion

            //#region CarModel

            //if (!_dbContext.Set<CarModel>().ToListAsync().Result.Any())
            //{
            //    List<CarModel> data = new List<CarModel>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/CarModel.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<CarModel>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _carModelRepository.AddAsync(x));
            //}

            //#endregion

            //#region Car

            ////if (!_dbContext.Set<Car>().ToListAsync().Result.Any())
            ////{
            ////    List<Car> data = new List<Car>();
            ////    using (StreamReader r = new StreamReader(rootPath + "/JsonFiles/Car.json"))
            ////    {
            ////        var json = r.ReadToEnd();
            ////        data = JsonConvert.DeserializeObject<List<Car>>(json, jsonSettings)!;
            ////    }
            ////    data.ForEach(x => _carRepository.AddAsync(x));
            ////}

            //#endregion



            //#region Extra

            //if (!_dbContext.Set<Extra>().ToListAsync().Result.Any())
            //{
            //    List<Extra> data = new List<Extra>();
            //    using (StreamReader r = new StreamReader(jsonRootPath + "/Extra.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<Extra>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _extraRepository.AddAsync(x));
            //}

            //#endregion



            #region Reservation

            //if (!_dbContext.Set<Reservation>().ToListAsync().Result.Any())
            //{
            //    List<Reservation> data = new List<Reservation>();
            //    using (StreamReader r = new StreamReader(rootPath + "/JsonFiles/Reservation.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<Reservation>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _reservationRepository.AddAsync(x));
            //}

            #endregion

            #region ReservationDetail

            //if (!_dbContext.Set<ReservationDetail>().ToListAsync().Result.Any())
            //{
            //    List<ReservationDetail> data = new List<ReservationDetail>();
            //    using (StreamReader r = new StreamReader(rootPath + "/JsonFiles/ReservationDetail.json"))
            //    {
            //        var json = r.ReadToEnd();
            //        data = JsonConvert.DeserializeObject<List<ReservationDetail>>(json, jsonSettings)!;
            //    }
            //    data.ForEach(x => _reservationDetailRepository.AddAsync(x));
            //}

            #endregion

            await _unitOfWorkRepository.SaveChangesAsync();
        }
    }
}
