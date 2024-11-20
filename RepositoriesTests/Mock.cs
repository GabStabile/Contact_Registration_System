using System;

namespace ControlContacts.Tests
{
    internal class Mock<T>
    {
        public Mock()
        {
        }

        internal object Setup(Func<object, object> value)
        {
            throw new NotImplementedException();
        }

        internal void Verify(Func<object, object> value, object once)
        {
            throw new NotImplementedException();
        }
    }
}