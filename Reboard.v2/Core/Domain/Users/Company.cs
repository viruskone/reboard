using Reboard.Core.Domain.Base;
using Reboard.Core.Domain.Users.OutboundServices;
using System;
using static Reboard.Core.Domain.Base.Rules.RuleValidator;

namespace Reboard.Core.Domain.Users
{
    public class Company : Entity
    {
        public CompanyId Id { get; }
        public CompanyName Name { get; }

        private Company(CompanyName name)
        {
            Name = name;
            Id = CompanyId.Make(Guid.NewGuid());
        }

        private Company(CompanyId id, CompanyName name)
        {
            Id = id;
            Name = name;
        }

        public static Company CreateNew(CompanyName name, ICompanyUniqueNameChecker checker)
        {
            CheckRule(new CompanyNameMustBeUniqueRule(checker, name));
            return new Company(name);
        }

        public static Company Make(CompanyId id, CompanyName name)
        {
            return new Company(id, name);
        }
    }
}