using Microsoft.AspNetCore.Mvc;
using System;

namespace Reboard.WebServer.Architecture
{
    internal static class CommandControllerExtensions
    {
        internal static IActionResult AcceptedAtTask(this ControllerBase controller, Guid id)
            => controller.Accepted($"/api/command/{id}");
    }
}