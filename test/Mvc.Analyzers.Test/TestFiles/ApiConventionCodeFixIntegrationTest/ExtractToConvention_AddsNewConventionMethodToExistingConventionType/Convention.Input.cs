using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToConvention_AddsNewConventionMethodToExistingConventionType._INPUT_
{
    public static class Convention
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Get(
            [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix)]
            [ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)]
            object id)
        { }
    }
}
