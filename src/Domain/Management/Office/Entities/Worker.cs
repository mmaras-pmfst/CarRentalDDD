using Domain.Common.Models;
using Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Office.Entities;
public sealed class Worker : Entity
{
    private readonly List<Guid> _contracts = new(); // ???

    public string PersonalIdentificationNumber { get; private set; }
    public FirstName FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Guid OfficeId { get; private set; }

    public IReadOnlyCollection<Guid> Contracts => _contracts; // ???

    private Worker()
    {

    }

    private Worker(Guid id, FirstName firstName, string lastName, Email email, PhoneNumber phoneNumber, Office office, string personalIdentificationNumber)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        OfficeId = office.Id;
        PersonalIdentificationNumber = personalIdentificationNumber;
    }

    public static Worker Create(Guid id, FirstName firstName, string lastName, Email email, PhoneNumber phoneNumber, Office office, string personalIdentificationNumber)
    {
        return new Worker(id, firstName, lastName, email, phoneNumber, office, personalIdentificationNumber);
    }

    public void Update(Email email, PhoneNumber phoneNumber, Office office)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        OfficeId = office.Id;
    }


}
