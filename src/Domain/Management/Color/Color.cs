using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Color;
public sealed class Color : AggregateRoot
{
    public string Name { get; private set; }
    private Color(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    private Color() { }

    public static Color Create(Guid id, string name)
    {
        return new Color(id, name);
    }

    public void Update(string name)
    {
        Name = name;
    }
}
