# Exception Handling in the Application

Shells: higher the number, more scoped exception, closer to the core

### Shell 1: Unmanaged Exception Handling
1. If none of the inner handlers can catch the error, the ErrorController will take care of it and write a neat JSON structured exception message about it. 
   ```json
   {
       "error": "UnhandledException",
       "message": "An unexpected error occurred.",
       "details": "Stack trace details here..."
   }
   ```
2. The JSON will be returned to the client side and processed by the HandleFailedRequest method in the GenericApiCommandHandler.
3. The HandleFailedRequest will throw a new ApiCommandExecutionUnknownException to notify the MediatR pipeline about the problem.
4. The ApiCommandExecutionUnknownException will be caught by the UnhandledExceptionBehaviour inside the MediatR pipeline and will be prompted to the user by the Dialog Service (including the exception's stack trace, etc).

### Shell 2: Managed Execution Exception Handling
If the execution throws a known "managed" exception inside the MediatR pipeline, but outside of the real business logic, it will likely be handled by the ApiExceptionFilterAttribute. These "managed" exceptions are used for general request validation, user validation, and permission validation. [provide the full list]

1. MediatR pipeline throws a managed (CommonApiLogicException) exception, with a message and additional arguments.
2. ApiExceptionFilterAttribute catches the exception and prints it as a JSON response to the client. 
   ```json
   {
       "error": "CommonApiLogicException",
       "message": "A validation error occurred.",
       "details": "Validation details here..."
   }
   ```
3. The HandleFailedRequest inside the GenericApiCommandHandler on the client side will parse the error JSON.
4. The HandleFailedRequest inside the GenericApiCommandHandler on the client side recognizes the specific exception type and throws a CommonApiLogicException with the exception code from the ApiLogicExceptionCode enum, and with the detail.
5. The description of the error is specified in the ApiLogicExceptionCode enum.
6. The HandleApiLogicException in the UnhandledExceptionBehaviour parses the exception code and acts as specified inside it.
7. The error description will likely be shown to the user with the Dialog Service.

### Shell 3: Input Validation Error Handling
The input validation is simple in VetCMS's architecture. Every API command's response must implement an IClientCommand interface, which includes a "message" and a "success" member. 
   ```csharp
   public interface IClientCommand
   {
       string Message { get; set; }
       bool Success { get; set; }
   }
   ```
If a validation error occurs, the ValidationBehaviour creates an instance of the TResponse, transforms the failures into the IClientCommand's "message" member, and sets "success" to false. 
   ```csharp
   public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TResponse : IClientCommand, new()
   {
       public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
       {
           var response = await next();
           if (!response.Success)
           {
               response.Message = "Validation errors occurred.";
           }
           return response;
       }
   }
   ```
The client command handler should prompt the error message to the user. Input validation will be executed both on the client and server side to minimize the performance pressure on the server instance.

### Shell 4: Business Logic Execution
The feature's business logic (in line with the architecture) executes in the feature's command handler. If a known managed or unmanaged error happens in the command handler, the request response will be marked as not successful, and the specified message will be (should be) shown to the user. The cause of such an error is likely a user error.


