using System;
using System.Collections.Generic;

namespace Demo.Common.ErrorHandling {
    public class DemoAPIException : Exception {
        public DemoAPIException () {

        }
        public DemoAPIException (List<DemoValidationResult> result) 
        {
            ValidationResult = result;
        }
        public DemoAPIException (string fieldName, string errorMessage) : this(new List<DemoValidationResult>
        {
            new DemoValidationResult{ FieldName = fieldName, Message = errorMessage}
        })
        {
            
        }
        public DemoAPIException (string errorMessage) : base(errorMessage)       
        {
            ValidationResult = new  List<DemoValidationResult>{
                new DemoValidationResult("",errorMessage)
            };
        }
      
        public List<DemoValidationResult> ValidationResult { get; set; }
    }
}