using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToNewConvention_ClonesExistingConventionTypeAndMethod._OUTPUT_
{
    public static class TestProjectConventions
    {
        /// <summary>
        /// An API convention that matches all methods that start with the term 'Update' containing exactly 2 parameter(s).
        /// Parameters must match the following requirements:
        /// <list type="number">
        /// <item>Parameter at position '1' has suffix 'id'.</item>
        /// <item>Parameter at position '2' has suffix 'item'.</item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Generated using the method signature for <see cref="TestController.Update(Guid, PetModel)"/>.
        /// </remarks>
        [ProducesResponseType(202)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Prefix)]
        public static void Update([ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object id, [ApiConventionNameMatch(ApiConventionNameMatchBehavior.Suffix), ApiConventionTypeMatch(ApiConventionTypeMatchBehavior.Any)] object item)
        {
        }
    }
}