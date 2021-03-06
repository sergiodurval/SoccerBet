using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Notifications;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace SoccerBet.Api.Controllers
{

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotification _notification;
        public readonly IUser AppUser;

        public Guid UserId { get; set; }
        protected bool UserIsAuthenticated { get; set; }
        protected MainController(INotification notification, IUser appUser)
        {
            _notification = notification;
            AppUser = appUser;

            if (AppUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                UserIsAuthenticated = true;
            }
        }

        protected bool OperationIsValid()
        {
            return !_notification.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationIsValid())
            {
                return Ok(new
                {
                    sucess = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                sucess = false,
                errors = _notification.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyErrorModelInvalid(modelState);

            return CustomResponse();
        }

        protected void NotifyErrorModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(erroMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notification.Handle(new Notification(message));
        }
    }
}
