using System;

namespace BLL.Validation
{
    public class ForumException : Exception
    {
        public ForumException(string message) : base(message)
        {
        }
    }
}