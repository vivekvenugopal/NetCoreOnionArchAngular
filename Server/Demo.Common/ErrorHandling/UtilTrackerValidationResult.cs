using System;

namespace Demo.Common.ErrorHandling
{
    public class DemoValidationResult
    {
        public DemoValidationResult()
        {

        }
        public DemoValidationResult(string fieldName, string errorMessage)
        {
            FieldName= fieldName;
            Message = errorMessage;
        }
        public string FieldName { get; set; }
        public string Message { get; set; }

    }
}
