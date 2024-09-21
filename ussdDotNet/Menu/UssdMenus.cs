using Microsoft.EntityFrameworkCore;
using ussdDotNet.Contracts;
using ussdDotNet.DBContext;
using ussdDotNet.Models;
using static ussdDotNet.Models.UssdModel;

namespace ussdDotNet.Menu
{
    public class UssdMenu
    {
        private readonly ILogger<UssdMenu> _logger;

        private readonly AppSettings _appSettings;

        public UssdMenu(ILogger<UssdMenu> logger, AppSettings appSettings)
        {
            _logger = logger;
            _appSettings = appSettings;
        }


        /// <summary>
        /// Processes the USSD response based on the user's input and session state.
        /// </summary>
        /// <param name="message">The message input from the user.</param>
        /// <param name="network">The network operator of the user.</param>
        /// <param name="mobileNumber">The mobile number of the user.</param>
        /// <param name="requestType">The type of the USSD request (e.g., initiation, response).</param>
        /// <param name="sessionId">The session ID of the USSD session.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the USSD response.</returns>
        public async Task<UssdResponse> UssdResponseAsync(string message, string network, string mobileNumber, string requestType, string sessionId)
        {
            var response = new UssdResponse();

            var options = new DbContextOptions<UssdDBAppContext>();

            using var context = new UssdDBAppContext(options, _appSettings);

            if (requestType.Equals("initiation", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    _logger.LogInformation("Initiation request received for session {SessionId}", sessionId);

                    response.Message = "Welcome to our USSD Service\n";
                    response.Message += "\n";
                    response.Message += "1. Register\n";
                    response.Message += "2. Deposit\n";
                    response.Message += "3. Withdraw\n";
                    response.Message += "4. Exit\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "initiation", null);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing initiation request for session {SessionId}", sessionId);
                    response.Message = "Sorry something went wrong, try and check again";
                    response.Type = "Response";
                }
            }
            else
            {
                _logger.LogInformation("Response request received for session {SessionId}", sessionId);

                var ussdSessions = await context.UssdSessions
                    .Where(s => s.SessionId == sessionId)
                    .OrderBy(d => d.CreatedAt)
                    .ToListAsync();

                var lastSession = ussdSessions[ussdSessions.Count - 1];

                // Register menu
                if (lastSession.Tag.Equals("initiation", StringComparison.OrdinalIgnoreCase) && message.Equals("1") && requestType.Equals("response", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Register menu selected for session {SessionId}", sessionId);

                    response.Message = "Enter your first name\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Register", null);
                    return response;
                }
                // Deposit menu
                else if (lastSession.Tag.Equals("initiation", StringComparison.OrdinalIgnoreCase) && message.Equals("2") && requestType.Equals("response", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Deposit menu selected for session {SessionId}", sessionId);

                    response.Message = "Enter amount to deposit\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Deposit", null);
                    return response;
                }
                // Withdraw menu
                else if (lastSession.Tag.Equals("initiation", StringComparison.OrdinalIgnoreCase) && message.Equals("3") && requestType.Equals("response", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Withdraw menu selected for session {SessionId}", sessionId);

                    response.Message = "Enter withdrawal amount\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Withdraw", null);
                    return response;
                }

                // Exit menu
                else if (lastSession.Tag.Equals("initiation", StringComparison.OrdinalIgnoreCase) && message.Equals("4") && requestType.Equals("response", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Exit menu selected for session {SessionId}", sessionId);

                    response.Message = "Thank you for using our service\n";
                    response.Type = "Release";
                }

                // Register menus
                else if (lastSession.Tag.Equals("Register", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Register first name received for session {SessionId}", sessionId);

                    response.Message = "Enter your last name\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Register.Firstname", null);
                    return response;
                }

                else if (lastSession.Tag.Equals("Register.Firstname", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Register last name received for session {SessionId}", sessionId);

                    var firstNameSession = await context.UssdSessions.FindAsync(sessionId);

                    response.Message = "Confirm details\n";
                    response.Message += $"Firstname: {firstNameSession?.Message}\n";
                    response.Message += $"Lastname: {message}\n";
                    response.Message += $"1: Confirm\n";
                    response.Message += $"2: Cancel\n";
                    response.Type = "Response";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Register.Lastname", null);
                    return response;
                }
                else if (lastSession.Tag.Equals("Register.Lastname", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogInformation("Registration complete for session {SessionId}", sessionId);

                    // Post user details to API

                    response.Message = "Registration successful\n";
                    response.Type = "Release";

                    UssdSession.SaveSession(context, _appSettings, requestType, sessionId, mobileNumber, network, message, "Register.Complete", null);
                    return response;
                }
            }

            return response;
        }
    }
}
