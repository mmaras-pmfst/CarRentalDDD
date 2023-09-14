using Domain.Common.Models;
using Domain.Management.Offices;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Workers;

public sealed class Worker : AggregateRoot, IAuditableEntity
{
    public string PersonalIdentificationNumber { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Guid OfficeId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public Office Office { get; private set; }

    private Worker()
    {

    }

    private Worker(Guid id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Guid officeId, string personalIdentificationNumber)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        OfficeId = officeId;
        PersonalIdentificationNumber = personalIdentificationNumber;
    }

    public static Worker Create(Guid id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, Office office, string personalIdentificationNumber)
    {
        return new Worker(id, firstName, lastName, email, phoneNumber, office.Id, personalIdentificationNumber);
    }

    public void Update(Email email, PhoneNumber phoneNumber, Office office)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        OfficeId = office.Id;
    }


}
