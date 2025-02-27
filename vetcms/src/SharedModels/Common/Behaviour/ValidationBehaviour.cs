﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Abstract;

namespace vetcms.SharedModels.Common.Behaviour
{
    /// <summary>
    /// Validátor behaviour, amely az API parancsok bemeneti valiálását végzi.
    /// </summary>
    /// <typeparam name="TRequest">Az API kérés típusa.</typeparam>
    /// <typeparam name="TResponse">Az API válasz típusa.</typeparam>
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ApiCommandBase<TResponse>
    where TResponse : ICommandResult, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;


        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Kezeli a kérést és végrehajtja a validálást.
        /// </summary>
        /// <param name="request">Az API kérés.</param>
        /// <param name="next">A következő kezelő a pipeline-ban.</param>
        /// <param name="cancellationToken">A lemondási token.</param>
        /// <returns>Az API válasz.</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    TResponse response = new TResponse();
                    response.Success = false;
                    response.Message = TrasformFailures(failures);
                    return response;
                    //throw new ValidationException(failures);
                }
            }

            return await next();
        }

        /// <summary>
        /// Átalakítja az érvényesítési hibákat szöveges üzenetté.
        /// </summary>
        /// <param name="failures">Az érvényesítési hibák listája.</param>
        /// <returns>Az érvényesítési hibák szöveges üzenete.</returns>
        private string TrasformFailures(List<ValidationFailure> failures)
        {
            string message = "A mezőket helyesen kell kitölteni: ";
            foreach (ValidationFailure failure in failures)
            {
                message += failure.ErrorMessage + ", ";
            }
            return message;
        }
 
    }

}
