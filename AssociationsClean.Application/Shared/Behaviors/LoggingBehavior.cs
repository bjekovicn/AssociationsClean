﻿using MediatR;
using Serilog.Context;
using Microsoft.Extensions.Logging;
using AssociationsClean.Domain.Shared.Abstractions;

namespace AssociationsClean.Application.Abstractions.Behaviors;


internal sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = request?.GetType().Name ?? "Unknown";

        try
        {

            TResponse result = await next();

            if (result.IsSuccess)
            {
                _logger.LogInformation("Request {RequestName} processed successfully", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, destructureObjects: true))
                {
                    _logger.LogError("Request {RequestName} processed with error", requestName);
                }
            }

            return result;
        }
        catch (Exception exception)
        {
            using (LogContext.PushProperty("Exception", exception, destructureObjects: true))
            {
                _logger.LogError(exception, "Request {RequestName} processing failed", requestName);
            }
            throw;
        }
    }
}