using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
 
namespace ChoicesBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;
 
                // return our reply to the user
                //Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
 
                await Conversation.SendAsync(activity, () => new SimpleDialog());
                //await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
 
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
 
            return null;
        }
 
        [Serializable]
        public class SimpleDialog : IDialog
        {
            public async Task StartAsync(IDialogContext context)
            {
                context.Wait(GiveTask);
            }
 
            public async Task GiveTask(IDialogContext context, IAwaitable<IMessageActivity> result)
            {
                var activity = await result as Activity;
                if (activity.Text.ToLower().Contains("timesheet"))
                {
                    await context.PostAsync($"So you want to fill in your timesheet?");
                    //context.Wait(GiveTask);
                }
                else if (activity.Text.ToLower().Contains("expense"))
                {
                    await context.PostAsync($"So you want to do something around expenses?");
                    //context.Wait(GiveTask);
                }
                else if (activity.Text.ToLower().Contains("restart") || activity.Text.ToLower().Contains("reset"))
                {
                    await context.PostAsync($"Oh, you want to restart our conversation?  No problem!");
                    //context.Wait(GiveTask);
                }
 
                else if (activity.Text.ToLower().Contains("bye") || activity.Text.ToLower().Contains("farewell"))
                {
                    await context.PostAsync($"Oh, you want to leave?  No problem!");
                    //context.Wait(GiveTask);
                }
                else
                {
                    //await context.PostAsync($"No task detected.. I'm still a bot in training :(  Would you like to do one of the followin?");
                    var PromptOptions = new string[] { "Timesheet", "Expenses", "Goodbye" };
                    PromptDialog.Choice(context, TestConfirm, PromptOptions, $"No task detected.. I'm still a bot in training :(  Would you like to do one of the followin?");
                    
                }
                //context.Wait(GiveTask);
 
            }
 
            public async Task TestConfirm(IDialogContext context, IAwaitable<object> result)
            {
                var confirm = await result;
                if ((string)confirm == "Timesheet")
                {
                    await context.PostAsync($"So you want to fill in your timesheet?");
                    context.Wait(GiveTask);
                }
                else if ((string)confirm == "Expenses")
                {
                    await context.PostAsync($"So you want to do something around expenses eh?");
                    context.Wait(GiveTask);
                }
                else
                {
                    await context.PostAsync($"Ok, bye!");
                    context.Wait(GiveTask);
                }
            }
        }
    }
}
