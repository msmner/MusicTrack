using MusicTrack.Exceptions;
using MusicTrack.Exceptions.AlreadyExists;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using System.Net;
using System.Text.Json;

namespace MusicTrack.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IHostEnvironment environment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error, error.Message);
                context.Response.ContentType = "application/json";
                MusicTrackException response;
                switch (error)
                {
                    case UsernameAlreadyExistsException:
                    case EnterValidCredentialsException:
                    case PlaylistDurationExceededException:
                    case PlaylistIsEmptyException:
                    case TrackAlreadyIncludedInPlaylistException:
                    case TrackDoesNotBelongToAlbumException:
                    case TrackIsNotIncludedInPlaylistException:
                    case TrackTypeNotValidException:
                    case ErrorCreatingUser:
                    case AddRoleToUserException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        response = _environment.IsDevelopment() ?
                        new MusicTrackException(error.Message) { StackTrace = error.StackTrace?.ToString() }
                        : new MusicTrackException("Internal server error!");
                        break;
                    case AlbumNotFoundException:
                    case PlaylistNotFoundException:
                    case TrackNotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        response = _environment.IsDevelopment() ?
                        new MusicTrackException(error.Message) { StackTrace = error.StackTrace?.ToString() }
                        : new MusicTrackException("Internal server error!");
                        break;
                    case InvalidUsernameOrPasswordException:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        response = _environment.IsDevelopment() ?
                        new MusicTrackException(error.Message) { StackTrace = error.StackTrace?.ToString() }
                        : new MusicTrackException("Internal server error!");
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        response = _environment.IsDevelopment() ?
                        new MusicTrackException(error.Message) { StackTrace = error.StackTrace?.ToString() }
                        : new MusicTrackException("Internal server error!");
                        break;
                }

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
