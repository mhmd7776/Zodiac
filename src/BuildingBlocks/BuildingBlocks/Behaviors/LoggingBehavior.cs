using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[Start] {typeof(TRequest).Name}.Handle with request {request}");

            var timer = new Stopwatch();

            timer.Start();
            var response = await next(cancellationToken);
            timer.Stop();

            var takenTime = timer.Elapsed;
            if (takenTime.Seconds > 2)
                logger.LogWarning($"[Performance] {typeof(TRequest).Name}.Handle with request {request} took {takenTime.Seconds} seconds");

            logger.LogInformation($"[End] {typeof(TRequest).Name}.Handle with response {response}");

            return response;
        }
    }
}
