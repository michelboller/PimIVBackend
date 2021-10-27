using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Validator
{
    [Serializable]
    public class GuardException : Exception
    {
        public IReadOnlyCollection<GuardResult> Errors { get; }

        public GuardException()
        {
        }

        public GuardException(ReadOnlyCollection<GuardResult> readonlyCollection)
        {
            Errors = readonlyCollection;
        }

        public GuardException(string message) : base(message)
        {
        }

        public GuardException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GuardException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
