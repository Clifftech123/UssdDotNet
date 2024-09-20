using Microsoft.AspNetCore.Mvc;
using ussdDotNet.Contracts;
using ussdDotNet.Menu;
using static ussdDotNet.Models.UssdModel;

namespace ussdDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UssdController : ControllerBase
    {
        private readonly UssdMenu _ussdMenu;

        /// <summary>
        /// Initializes a new instance of the <see cref="UssdController"/> class.
        /// </summary>
        /// <param name="ussdMenu">The USSD menu service.</param>
        public UssdController(UssdMenu ussdMenu)
        {
            _ussdMenu = ussdMenu;
        }

        /// <summary>
        /// Handles the USSD request and returns a response.
        /// </summary>
        /// <param name="ussdRequest">The USSD request data.</param>
        /// <returns>An <see cref="ActionResult"/> containing the USSD response.</returns>
        [HttpPost("api/Ussd")]
        public async Task<ActionResult> Index(UssdRequestData ussdRequest)
        {
            try
            {
                var objResp = await _ussdMenu.UssdResponseAsync(ussdRequest.Message, ussdRequest.Operator, ussdRequest.Mobile, ussdRequest.Type, ussdRequest.SessionId);

                // Log the response
                Console.WriteLine($"Response: {objResp}");

                if (objResp == null)
                {
                    return Ok(new { Message = "No response from UssdMenu.UssdResponseAsync" });
                }

                return Ok(objResp);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception: {ex.Message}");

                var errorResponse = ErrorRespons.ussdErrorhandler_Response();

                // Log the error response
                Console.WriteLine($"Error Response: {errorResponse}");

                return Ok(new { ussdRequest.Mobile, errorResponse });
            }
        }
    }
}
