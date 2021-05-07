using Reboard.Core.Domain.Base;
using System.Collections.Generic;

namespace Reboard.Core.Domain.Reports
{
    public class Color : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new System.NotImplementedException();
        }
    }
}