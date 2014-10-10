using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Web.Services;

namespace SitefinityWebApp.CustomServices
{
    /// <summary>
    /// Sitefinity web service.
    /// </summary>
    /// <remarks>
    /// If this service is a part of a Sitefinity module,
    /// you can install it by adding this to the module's Initialize method:
    /// App.WorkWith()
    ///     .Module(ModuleName)
    ///     .Initialize()
    ///         .WebService<FormsServiceExtended>(ServiceUrl); // ServiceUrl example: "Sitefinity/Services/ModuleName/FormsServiceExtended.svc/"
    /// </remarks>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class FormsServiceExtended : IFormsServiceExtended
    {

        public string GetSubscribers(string formId)
        {
            string subscriberEmails = FormSubscriberEmails(formId);

            if (string.IsNullOrEmpty(subscriberEmails))
            {
                return "There are no subscribers for this form!";
            }

            return subscriberEmails;
        }

        public string FormSubscriberEmails(string formId)
        {
            var formManager = FormsManager.GetManager();

            var id = new Guid(formId);
            var form = formManager.GetForms().Where(f => f.Id == id).SingleOrDefault();

            var subscriptionListId = form.SubscriptionListId;

            var notificationService = SystemManager.GetNotificationService();
            var serviceContext = new ServiceContext("ThisApplicationKey", formManager.ModuleName);

            List<ISubscriberResponse> formSubscribers = notificationService.
                GetSubscribers(serviceContext, subscriptionListId, new QueryParameters())
                .ToList();

            List<string> subscriberEmails = new List<string>();

            if (formSubscribers != null)
            {
                foreach (var subscriber in formSubscribers)
                {
                    subscriberEmails.Add(subscriber.Email);
                }
            }

            string result = string.Join("</br>", subscriberEmails);

            return result;
        }
    }
}