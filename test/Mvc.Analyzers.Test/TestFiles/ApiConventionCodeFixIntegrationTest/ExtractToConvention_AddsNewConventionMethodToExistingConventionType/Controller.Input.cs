using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToConvention_AddsNewConventionMethodToExistingConventionType._INPUT_
{
    [ApiController]
    [ApiConventionType(typeof(Convention))]
    public class TestController : ControllerBase
    {
        public ActionResult<TestModel> Get(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            return new TestModel();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> PostPerson(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Conflict();
            }

            return CreatedAtAction(nameof(Get), model);
        }
    }

    public class TestModel { }
}
