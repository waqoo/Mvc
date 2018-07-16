using System;

namespace Microsoft.AspNetCore.Mvc.Analyzers.ExtractToNewConvention_ClonesExistingConventionTypeAndMethod._INPUT_
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TestController : ControllerBase
    {
        public IActionResult Update(Guid petId, PetModel petItem)
        {
            if (petId == Guid.Empty)
            {
                return NotFound();
            }

            UpdatePet(petItem);
            return Accepted();
        }

        private void UpdatePet(PetModel pet)
        {
            throw new NotImplementedException();
        }
    }

    public class PetModel { }
}
