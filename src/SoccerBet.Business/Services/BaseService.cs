using FluentValidation;
using FluentValidation.Results;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Business.Notifications;


namespace SoccerBet.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotification _notification;

        protected BaseService(INotification notification)
        {
            _notification = notification;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notification.Handle(new Notification(message));
        }

        protected bool ExecuteValidation<TV,TE>(TV validation , TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid)
                return true;

            Notify(validator);

            return false;
        }
    }
}
