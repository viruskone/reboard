using System;

namespace Reboard.Repository.Exceptions
{
    public class DuplicateEntryException : Exception
    {
        public DuplicateEntryException(string uniqueId) : base($"Entry duplicated on id: {uniqueId}")
        {
        }
    }
}