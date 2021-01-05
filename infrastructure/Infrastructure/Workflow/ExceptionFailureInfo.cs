using System;

namespace Infrastructure.Workflow
{
    public class ExceptionFailureInfo : FailureInfo
    {
        public ExceptionFailureInfo(Exception e, string message = null) :
            base(FailureType.Exception, message ?? e.Message) { }
    }
}