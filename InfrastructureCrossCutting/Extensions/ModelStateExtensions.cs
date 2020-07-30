namespace InfrastructureCrossCutting.Extensions
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensions
    {
        /// <summary>
        /// Get all properties errors from model.
        /// </summary>
        /// <param name="modelState">The model state dictionary.</param>
        /// <returns>All errors join in one string separated by comma.</returns>
        public static string GetAllInvalidKeys(this ModelStateDictionary modelState)
        {
            var invalidKeys = modelState
                .Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                .Select(x => x.Key);

            return string.Join(", ", invalidKeys);
        }
    }
}
