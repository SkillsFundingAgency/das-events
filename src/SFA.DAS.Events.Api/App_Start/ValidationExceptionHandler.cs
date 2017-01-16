using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;
using FluentValidation;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.Events.Api
{
    public class ValidationExceptionHandler : ExceptionHandler
    {
        private static ILog Logger = new NLogLogger();

        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is ValidationException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                var message = CreateMessage(context);
                response.Content = new StringContent(message);
                context.Result = new ValidationErrorResult(context.Request, response);

                Logger.Warn(context.Exception, "Validation error");

                return;
            }

            Logger.Error(context.Exception, "Unhandled exception");

            base.Handle(context);
        }

        private static string CreateMessage(ExceptionHandlerContext context)
        {
            var exception = (ValidationException)context.Exception;

            if (exception.Errors.Count() > 0)
            {
                var message = new StringBuilder();

                foreach (var error in exception.Errors)
                {
                    message.AppendLine($"Property Name: {error.PropertyName}");
                    message.AppendLine($"Error: {error.ErrorMessage}");
                }

                return message.ToString();
            }

            return ((ValidationException)context.Exception).Message;
        }
    }
}
