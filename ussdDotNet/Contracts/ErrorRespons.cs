using static ussdDotNet.Models.UssdModel;

namespace ussdDotNet.Contracts
{
    public class ErrorRespons
    {


        public static UssdResponse ussdStatusResponse(AppSettings appSettings)
        {
            var resp = new UssdResponse();

            resp.Message = appSettings.DisableAppMsg.ToString();
            resp.Type = "Release";

            return resp;
        }

        public static UssdResponse ussdTestResponse()
        {
            var resp = new UssdResponse();
            resp.Message = "Welcome to Cliffrod MOMM USSD Mobile Service.\n Access Denied";
            resp.Type = "Release";
            return resp;
        }

        public static UssdResponse ussdDebugResponse(AppSettings appSettings)
        {
            var resp = new UssdResponse();
            resp.Message = appSettings.DebugMsg.ToString();
            resp.Type = "Release";
            return resp;
        }

        public static UssdResponse ussdBlockedResponse()
        {
            var resp = new UssdResponse();

            resp.Message = "Sorry, You are unable to access Service. Kindly contact our Customer care Center. Thank you.";
            resp.Type = "Release";
            return resp;
        }

        public static UssdResponse ussdErrorhandler_Response()
        {
            var resp = new UssdResponse();

            resp.Type = "Release";
            resp.Message = "Sorry, unable to process request. Kindly try again later.";
            return resp;
        }
    }
}
