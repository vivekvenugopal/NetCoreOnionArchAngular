using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Demo.Common.ErrorHandling;

public static class APIHelper
{
    public static void AddModelErrorState(ModelStateDictionary modelState, List<DemoValidationResult> validationResults)
    {
        foreach(var validationResult in validationResults)
        {
            modelState.AddModelError(validationResult.FieldName, validationResult.Message);
        }
    }

    internal static void AddModelErrorState(object modelState, object validationResult)
    {
        throw new NotImplementedException();
    }
}