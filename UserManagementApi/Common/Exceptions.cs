using System;

namespace Common
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string message): base(message)
        {

        }
    }
}
