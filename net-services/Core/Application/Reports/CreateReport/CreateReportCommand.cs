using MediatR;
using Reboard.Core.Domain.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reboard.Core.Application.Reports.CreateReport
{
    public class CreateReportCommand : IRequest
    {
        public Guid[] AllowedCompanies { get; }
        public Guid[] AllowedUsers { get; }
        public ColorDto Color { get; }
        public string Description { get; }
        public string Shortcut { get; }
        public string Title { get; }

        public CreateReportCommand(string title, string description, string shortcut, ColorDto color, Guid[] allowedUsers, Guid[] allowedCompanies)
        {
            Title = title;
            Description = description;
            Shortcut = shortcut;
            Color = color;
            AllowedUsers = allowedUsers;
            AllowedCompanies = allowedCompanies;
        }
    }
}