using Application.Abstractions;
using Application.Common.Mailing;
using Domain.Management.Color;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Create
{
    internal sealed class ColorCreateCommandHandler : ICommandHandler<ColorCreateCommand, Guid>
    {
        private ILogger<ColorCreateCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IColorRepository _colorRepository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IMailService _mailService;

        public ColorCreateCommandHandler(ILogger<ColorCreateCommandHandler> logger, IUnitOfWork unitOfWork, IColorRepository colorRepository, IEmailTemplateService emailTemplateService, IMailService mailService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _colorRepository = colorRepository;
            _emailTemplateService = emailTemplateService;
            _mailService = mailService;
        }

        public async Task<Result<Guid>> Handle(ColorCreateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorCreateCommandHandler");

            try
            {
                var exists = await _colorRepository.AlreadyExists(request.ColorName, cancellationToken);
                if (exists)
                {
                    _logger.LogWarning("ColorCreateCommandHandler: Color already exists!");
                    return Result.Failure<Guid>(DomainErrors.Color.ColorAlreadyExists);

                }
                var newColor = Color.Create(
                    Guid.NewGuid(),
                    request.ColorName);

                await _colorRepository.AddAsync(newColor, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);


                RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
                {
                    Email = "marko.maki.maras@gmail.com",
                    UserName = "mmaras",
                    Url = ""
                };

                var mailRequest = new MailRequest(
                new List<string> { "marko.maki.maras@gmail.com" },
                    "Reservation confirmation",
                    _emailTemplateService.GenerateEmailTemplate("email-confirmation", eMailModel));

                await _mailService.SendAsync(mailRequest);

                _logger.LogInformation("Finished ColorCreateCommandHandler");
                return newColor.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError("ColorCreateCommandHandler error: {0}", ex.Message);
                return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
            }
        }
    }
}
