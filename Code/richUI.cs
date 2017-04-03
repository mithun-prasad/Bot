        private async Task GiveTask(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if (activity.Text.ToLower().Contains("timesheet"))
            {
                await context.PostAsync($"So you want to fill in your timesheet?");
            }
            else if (activity.Text.ToLower().Contains("expense"))
            {
                await context.PostAsync($"So you want to do something around expenses?");
            }
            else if (activity.Text.ToLower().Contains("restart") || activity.Text.ToLower().Contains("reset"))
            {
                await context.PostAsync($"Oh, you want to restart our conversation?  No problem!");
            }

            else if ( activity.Text.ToLower().Contains("bye") || activity.Text.ToLower().Contains("farewell"))
            {
                await context.PostAsync($"Oh, you want to leave?  No problem!");
            }
            else
            {
                // await context.PostAsync($"No task detected.. I'm still a bot in training :(  Would you like to do one of the followin?");
                var PromptOptions = new string[] { "Timesheet", "Expenses", "Goodbye" };
                PromptDialog.Choice(context, TestConfirm, PromptOptions, $"No task detected.. I'm still a bot in training :(  Would you like to do one of the followin?");
            }
        }

        private async Task TestConfirm(IDialogContext context, IAwaitable<string> result)
        {
            if(result.Equals("Timesheet"))
            {
                await context.PostAsync($"So you want to fill in your timesheet?");
            }
            else if(result.Equals("Expenses"))
            {
                await context.PostAsync($"So you want to do something around expenses eh?");
            }
            else
            {
                await context.PostAsync($"Ok, bye!");
                context.Wait(ActivityReceivedAsync);
            }
        }