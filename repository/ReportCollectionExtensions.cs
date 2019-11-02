using System;
using MongoDB.Bson;
using Reboard.Domain.Reports;

namespace Reboard.Repository
{
    public static class ReportCollectionExtensions
    {
        public static ReportMongoDto ToDto(this Report report) =>
            new ReportMongoDto
            {
                Title = report.Title,
                Description = report.Description,
                CreateTime = report.CreateTime,
                Downloads = report.Downloads,
                AverageDuration = report.AverageDuration,
                Rating = report.Rating
            };

        public static Report FromDto(this ReportMongoDto dto) =>
            dto == null ? null :
            new Report
            {
                Id = dto.Id.ToString(),
                Title = dto.Title,
                Description = dto.Description,
                CreateTime = DateTime.SpecifyKind(dto.CreateTime, DateTimeKind.Utc),
                Downloads = dto.Downloads,
                AverageDuration = dto.AverageDuration,
                Rating = dto.Rating
            };

        public static ReportMongoDto AssingNewId(this ReportMongoDto dto)
        {
            dto.Id = new ObjectId();
            return dto;
        }
    }

}
