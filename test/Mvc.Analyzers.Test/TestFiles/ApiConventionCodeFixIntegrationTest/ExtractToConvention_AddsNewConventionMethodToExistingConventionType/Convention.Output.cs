using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToConvention_AddsNewConventionMethodToExistingConventionType._OUTPUT_
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

        /// <summary>
        /// An API convention that matches all methods that start with the term 'Post' containing exactly 1 parameter(s).
        /// Parameters must match the following requirements:
        /// <list type="number">
        /// <item>Parameter at position '1' has suffix 'model'.</item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Generated using the method signature for <see cref="TestController.PostPerson(TestModel)"/>.
        /// </remarks>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Post([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object model)
        {
        }
    }
}
