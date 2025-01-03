﻿using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using vetcms.ClientApplication.Common.Exceptions;
using vetcms.ClientApplication.Common.IAM;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.ApiLogicExceptionHandling;

namespace vetcms.ClientApplication.Common.Abstract
{
    internal class GenericApiCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : ApiCommandBase<TResult>
    where TResult : ICommandResult
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationManger _credentialStore;
        public GenericApiCommandHandler(HttpClient httpClient, AuthenticationManger credentialStore)
        {
            _httpClient = httpClient;
            _credentialStore = credentialStore;

        }

        public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
        {
            if(await _credentialStore.HasAccessToken())
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", await _credentialStore.GetAccessToken()
                );
            }
            
            var response = await DispatchRequest(request);
            return await ProcessResponse(response);
        }

        private async Task<HttpResponseMessage?> DispatchRequest(TCommand request)
        {
            switch (request.GetApiMethod())
            {
                case HttpMethodEnum.Get:
                    return await ProcessGet(request);
                case HttpMethodEnum.Post:
                    return await ProcessPost(request);
                case HttpMethodEnum.Patch:
                    return await ProcessPatch(request);
                case HttpMethodEnum.Put:
                    return await ProcessPut(request);
                case HttpMethodEnum.Delete:
                    return await ProcessDelete(request);
                default:
                    throw new NotImplementedException($"Http method not implemented in request dispatcher: {Enum.GetName(request.GetApiMethod())}");
            }
        }

        private async Task<TResult> ProcessResponse(HttpResponseMessage? response)
        {
            if(response == null)
            {
                throw new Exception("A szerver nem válaszolt a kérésre");
            }
            if (response.IsSuccessStatusCode)
            {
                return await ProcessResult(response);
            }
            else
            {
                await HandleFailedRequest(response);
                return default!;
            }
        }

        private async Task HandleFailedRequest(HttpResponseMessage? response)
        {
            ProblemDetails? problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problem != null)
            {
                if (problem.type == typeof(CommonApiLogicException).ToString())
                {
                    throw new CommonApiLogicException((ApiLogicExceptionCode)problem.status, problem.detail);
                }
                else
                {
                    throw new ApiCommandExecutionUnknownException(problem);
                }
            }
            throw new Exception("Szerveroldali hiba történt a kérés során");
        }

        private async Task<TResult> ProcessResult(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            TResult? result = JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if(result == null)
            {
                throw new Exception("Üres vagy helytelen Http válasz.");
            }
            return result;
        }

        private async Task<HttpResponseMessage?> ProcessGet(TCommand command)
            => await _httpClient.GetAsync(command.GetApiEndpoint());
        private async Task<HttpResponseMessage?> ProcessPost(TCommand command)
            => await _httpClient.PostAsJsonAsync(command.GetApiEndpoint(), command);

        private async Task<HttpResponseMessage?> ProcessPatch(TCommand command)
            => await _httpClient.PatchAsJsonAsync(command.GetApiEndpoint(), command);

        private async Task<HttpResponseMessage?> ProcessPut(TCommand command)
            => await _httpClient.PutAsJsonAsync(command.GetApiEndpoint(), command);

        private async Task<HttpResponseMessage?> ProcessDelete(TCommand command)
            => await _httpClient.DeleteAsync(command.GetApiEndpoint());
    }
}
