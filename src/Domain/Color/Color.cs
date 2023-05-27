using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Color;

public sealed class Color : AggregateRoot
{
    public string ColorName { get; private set; }
    private Color(Guid id, string colorName)
        : base(id)
    {
        ColorName = colorName;
    }

    private Color() { }

    public static Color Create(Guid id, string colorName)
    {
        return new Color(id, colorName);
    }

    public void Update(string colorName)
    {
        ColorName = colorName;
    }
}
