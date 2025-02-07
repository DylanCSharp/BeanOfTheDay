Database project is attached so the scripts may be looked at. I have also attached a .bak and a .bacpac file so you can restore the database locally if you wish.

1. How to Run Project
Multiple start up, API project and Blazor (https) project will need to run in tandem, as the Blazor project speaks to the API.

2. Technology Chosen
   2.1 I have chosen Blazor Server on .NET 8 as it is what I am most familiar with. I am currently busy learning JS so that I can implement a similar solution in JS.
   2.2 I have also added an ASP.NET Core Web API to this project so that the Blazor Server project can talk to an API
   2.3 I have also added a class library so that code can be shared between both projects.

NOTE: For the Bean of the Day feature, there is a job that runs a stored procedure, this job will refresh the Bean of the Day every morning at 00:01. The stored procedure ran is [BOTD].[RefreshBOTD]

3. Basics to expect
   3.1 There is a main page with all the beans loaded with all their images and information. You can view their details and a popup will display.
   3.2 There is a basic orders page when you can add beans and submit an order
   3.3 There is an admin page when you can do all the CRUD options through the UI so that you don't have to call the API manually.
   3.4 There is a Swagger UI endpoint that can be used by running the API project, you can call the API calls manually there if you wish.
   3.5 On the admin page, you can also import the .json file so that all the beans are present.

I'll keep this short, but enjoy :)
   


   



