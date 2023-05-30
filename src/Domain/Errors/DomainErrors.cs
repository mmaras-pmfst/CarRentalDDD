using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Color
    {
        public static readonly Error ColorAlreadyExists = new Error(
        "Color.ColorAlreadyExists",
        "The specified color name is already in use");
    }


    
}
