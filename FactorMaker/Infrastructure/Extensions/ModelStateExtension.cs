using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common
{
    public static class ModelStateExtension
    {
        static ModelStateExtension()
        {

        }

        public static List<string> GetErrorMessages(this ModelStateDictionary modelState)
        {
            var errors = modelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();

            var list = new List<string>();

            foreach (ModelErrorCollection errorCollection in errors)
            {
                foreach (var item in errorCollection)
                {
                    list.Add(item.ErrorMessage);
                }
            }

            return list;
        }
    }
}
