using MongoDB.Bson;
using Reboard.Domain;
using Reboard.Domain.Reports;
using System;

namespace Reboard.Repository.Reports.Mongo
{
    public static class ReportCollectionExtensions
    {
        //TODO: Move to another class
        public static ColorMongoDto ToDto(this Color color) =>
            new ColorMongoDto
            {
                R = color.Red,
                G = color.Green,
                B = color.Blue
            };

        public static Color FromDto(this ColorMongoDto dto) =>
            new Color
            {
                Red = dto?.R ?? 0,
                Green = dto?.G ?? 0,
                Blue = dto?.B ?? 0
            };

        public static ReportMongoDto ToDto(this Report report) =>
            new ReportMongoDto
            {
                Title = report.Title,
                Description = report.Description,
                CreateTime = report.CreateTime,
                Downloads = report.Downloads,
                AverageDuration = report.AverageDuration,
                Shortcut = report.Shortcut,
                Color = report.Color.ToDto()
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
                Shortcut = dto.Shortcut,
                Color = dto.Color.FromDto()
            };

        public static ReportMongoDto AssingNewId(this ReportMongoDto dto)
        {
            dto.Id = new ObjectId();
            return dto;
        }
    }
}