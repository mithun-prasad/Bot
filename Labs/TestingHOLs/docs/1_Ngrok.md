# Development and Testing with Ngrok
 
## 1.	Objectives
 
With Microsoft Bot Framework, to configure the bot to be available to a particular channel, you will need to host the Bot service on a public URL endpoint. The channel will not be able to access your bot service if it is on a local server port hidden behind a NAT or firewall.
  
When designing / building / testing your code, you do not always want to have to keep redeploying. This will result in additional hosting costs. This is where ngrok can really help in speeding up the development/testing phases of bots. The goal of this lab is to use ngrok to expose your bot to public internet and use the public endpoints to test your bots in the emulator.
  
## 2.	Setup
  
 a.	  If you do not have ngrok installed, download ngrok from https://ngrok.com/download and install for your OS. Unzip ngrok file downloaded and install it.

 b.	  Open any Bot project (from the previous labs) in Visual Studio and run it. If you are using EchoBot, you should be seeing the below message in the browser:

**EchoBot**

Describe your bot here and your terms of use etc.

Visit Bot Framework to register your bot. When you register it, remember to set your bot's endpoint to https://your_bots_hostname/api/messages

## 3.	Forwarding

 a.	 Given the bot is being hosted on localhost:3979, we can use ngrok to expose this port to the public internet

 b.	 Open terminal and go to the folder where ngrok is installed
 
 c.	 Run the below command and you should see the forwarding url:

 ````ngrok.exe http 3979 -host-header="localhost:3979"````

![Forwarding Url](images/ForwardingUrl.png)

 d.	 In some of the older emulator versions, you will also need to provide an emulator url for port 9000. If you see Emulator url (port 9000) listed in your emulator, you will also need to generate a forwarding url using ngrok. Run the below command and you should see the forwarding url for port 9000:

 ````ngrok.exe http -host-header=rewrite 9000````

![Emulator Url](images/EmulatorUrl.png)

*** The above step (d) may not be required if you do not see emulator url in your emulator.

 e.	 Enter the forwarded urls in the bot emulator (bot url and emulator url). The bot url will have /api/messages appended to the forwarding url. Test the bot in the emulator by sending messages.

![Bot Url](images/BotUrl.png)

## 4.	Finish early? Try this exercise for extra credit:

 When you register the bot on the Microsoft Bot Framework, can you use the forwarding url for the Messaging Endpoint? Test the bot on a channel using the forwarding URL on any channel (skype for example).