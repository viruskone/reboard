using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Reboard.WebServer.Architecture
{
    internal static class CommandControllerExtensions
    {
        internal static IActionResult AcceptedAtTask(this ControllerBase controller, Guid id)
            //        => controller.Accepted($"/api/command/{id}");
            => controller.Accepted(new Uri(controller.Request.GetAppBaseUri(), $"/api/command/{id}"));

        private static Uri GetAppBaseUri(this HttpRequest request)
            => new Uri($"{request.Scheme}://{request.Host}{request.PathBase}");

    }
}