using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace SitefinityWebApp.CustomServices
{
    [ServiceContract]
    public interface IFormsServiceExtended
    {
        /// <summary>
        /// Tests the connection to the service.
        /// </summary>
        [WebHelp(Comment = "Tests the connection to the service. Result is returned in JSON format.")]
        [WebGet(UriTemplate = "GetSingleFormSubscribers/?itemId={formId}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetSubscribers(string formId);
    }
}
