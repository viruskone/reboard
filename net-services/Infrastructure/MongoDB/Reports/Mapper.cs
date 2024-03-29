﻿using System;
using System.Linq;
using Reboard.Core.Domain.Reports;
using Reboard.Core.Domain.Shared;

namespace Reboard.Infrastructure.MongoDB.Reports
{
    public static class Mapper
    {
        public static Report FromDto(this ReportDto dto)
            => dto != null ?
                Report.Make(
                    dto.Id,
                    (ReportTitle)dto.Title,
                    dto.Description,
                    (ReportShortcut)dto.Shortcut,
                    dto.Color.FromDto(),
                    dto.AverageDuration,
                    dto.CreateTime,
                    dto.Downloads,
                    dto.AllowedUsers.Select(d => (UserId)d).ToArray(),
                    dto.AllowedCompanies.Select(d => (CompanyId)d).ToArray()
                    ) :
                null;

        public static Color FromDto(this ColorDto dto)
            => dto != null ?
                Color.Make(dto.R, dto.G, dto.B) :
                null;

        public static ColorDto ToDto(this Color color)
            => new ColorDto
            {
                R = color.Red,
                G = color.Green,
                B = color.Blue
            };

        public static ReportDto ToDto(this Report report)
                => new ReportDto
                {
                    Id = report.Id,
                    Title = report.Title,
                    Description = report.Description,
                    Shortcut = report.Shortcut,
                    Color = report.Color.ToDto(),
                    AverageDuration = report.AverageGenerationTime,
                    CreateTime = report.CreateTime,
                    Downloads = report.DownloadTimes,
                    AllowedCompanies = report.AllowedCompanies.Select(d => (Guid)d).ToArray(),
                    AllowedUsers = report.AllowedUsers.Select(d => (Guid)d).ToArray()
                };
    }
}