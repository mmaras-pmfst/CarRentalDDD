using Application.Mappings.Profiles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.Configurations;
public class AutoMapperConfiguration
{
    public MapperConfiguration Configure()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.ShouldMapMethod = (m => false);

            cfg.AddProfile<CarBrandProfile>();
            cfg.AddProfile<CarBrandDetailProfile>();
            cfg.AddProfile<CarModelProfile>();
            cfg.AddProfile<CarModelDetailProfile>();
            cfg.AddProfile<CarProfile>();
            cfg.AddProfile<CarDetailProfile>();
            cfg.AddProfile<ExtraProfile>();
            cfg.AddProfile<CarCategoryProfile>();
            cfg.AddProfile<CarCategoryDetailProfile>();
            cfg.AddProfile<OfficeProfile>();
            cfg.AddProfile<OfficeDetailProfile>();
            cfg.AddProfile<WorkerProfile>();
            cfg.AddProfile<WorkerDetailProfile>();

        });
        return config;
    }
}
