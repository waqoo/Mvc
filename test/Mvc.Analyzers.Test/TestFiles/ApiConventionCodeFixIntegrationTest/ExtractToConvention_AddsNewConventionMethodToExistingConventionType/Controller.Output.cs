using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToConvention_AddsNewConventionMethodToExistingConventionType._OUTPUT_
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
