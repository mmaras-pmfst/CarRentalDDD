using Application.Contracts.Events;
using Application.Reservations.Events;
using AutoMapper.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mailing;
internal class SendEmailHandler : 
    INotificationHandler<ContractCreatedEvent>,
    INotificationHandler<ReservationCreatedEvent>
{
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;

    public SendEmailHandler(IEmailTemplateService emailTemplateService, IMailService mailService)
    {
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
    }

    public Task Handle(ContractCreatedEvent notification, CancellationToken cancellationToken)
    {
        var emailModel = new ContractEmailModel(
            notification.RentalPrice,
            notification.TotalPrice,
            notification.CarModelName,
            notification.CarBrandName,
            notification.RegistrationPlate);

        return HandleInternal(notification.Type, notification.Email, emailModel, null, cancellationToken);
    }

    public Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
    {
        var emailModel = new ReservationEmailModel(
            notification.RentalPrice,
            notification.TotalPrice,
            notification.CarModelName,
            notification.CarBrandName);

        return HandleInternal(notification.Type, notification.Email, null, emailModel, cancellationToken);
    }

    public async Task HandleInternal(EmailType type, string email, ContractEmailModel? contractEmailModel, ReservationEmailModel? reservationEmailModel, CancellationToken cancellationToken)
    {
        if (type == EmailType.Contract)
        {
            var mailRequest = new MailRequest(
                new List<string> { email },
                    "Contract Created",
                    _emailTemplateService.GenerateEmailTemplate("contract", contractEmailModel));

            await _mailService.SendAsync(mailRequest);
        }
        else
        {
            var mailRequest = new MailRequest(
                new List<string> { email },
                    "Reservation Made",
                    _emailTemplateService.GenerateEmailTemplate("reservation", reservationEmailModel));

            await _mailService.SendAsync(mailRequest);
        }

    }
}
