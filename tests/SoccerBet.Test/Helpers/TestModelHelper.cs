using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SoccerBet.Test.Helpers
{
    public class TestModelHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, results, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);
            return results;
        }

        public static ActionResult CustomResponse(object model)
        {
            var results = Validate(model);
            
            if (results.Any())
            {
                var badResult = new BadRequestResult();
                return badResult;
            }

            var okResult = new OkResult();
            return okResult;
                
        }
    }
}
