# SmallSimpleOA
- This is my final project of Junior .NET Developer Training by Manitoba Start & ComIT.
- It is based on .NET Core and MVC.
- It is developed on macOS with Visual Studio and MS SQL SERVER in Docker. Following link explains how to install MS SQL SERVER in Docker:          
https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-2017
- To run this project, you need to add following NuGet packages:   
    Microsoft.AspNetCore.App   
    Microsoft.EntityFrameworkCore   
    Microsoft.EntityFrameworkCore.Proxies   
    Newtonsoft.Json      
    SignalR library is also needed. I developed this project on a Mac, so I install the library by command line. If you run it on Windows, you might need to install the library from NuGet.

# Demo Video
- Add new user by admin account: https://drive.google.com/open?id=1ZfT6MVthMLkJWxWQyebn0M1k09843ZD7
- Message: https://drive.google.com/open?id=1ZLR8yikKeHFx-8FYVgA4QQOxQZ6Ub9Xe
- Attendance: https://drive.google.com/open?id=1qqGkBT7wus2e-M0vCtq9KwOLxWj3uID3
- Leave request: https://drive.google.com/open?id=1FNP3R8Kq-7GImm1uESTXMyNR9B1hLIRA
- Todo: https://drive.google.com/open?id=10fFga4J0iHYCnkjg2ID94mx3WGDqr92l

## Login/Logout:
- You can login by email and password;
- You can logout by the menu on the top-right corner.

## User Management:
- You can add user by admin account:
- username: admin@ssoa.ca
- password: password
- Every user's password stored in database is encoded by MD5 with password itself plus a random "salt".

- With our test data we have other users listed as below and the password are their first name in lower case:

- **Finance department:**
    - nancy@ssoa.ca
    - carrol@ssoa.ca
    - cherry@ssoa.ca
    - jason@ssoa.ca
    - clark@ssoa.ca

- **HR deparmtnet:**
    - alexandra@ssoa.ca
    - kirk@ssoa.ca

- **Sale department:**
   - harpreet@ssoa.ca
   - bozena@ssoa.ca
   - cliff@ssoa.ca
   - emile@ssoa.ca

- **Develop department:**
   - keith@ssoa.ca
   - eric@ssoa.ca


## Attendance:
- Everyday if you have not clock in, the home page gives you a tip to clock in.
- You can clock in one time per day.
- You can clock out several time per day, and the later clock out overwrites the earlier one.
- You can clock in and out manually by the menu on the top-right corner.
- Everyone can check their own attendance by month.
- Within a department a guy with higher level can check other guys' attendance whose level is lower than him/her.
- Members of HR department can check othre guys' attendance.

## Message
- Every user can send a message to another.
- If the receiver is online, he/she will receive a notification at once.
- And he/she can click the notification to go to the message page to chat with the sender.
- In the message page, you can view your chat history with others.
- If you receive a new message when you are in the message page, it shows directly in your chat dialog or shows a badge on the user list.
- When you log in it shows a badge on the navigation side if you have an unreaded message.

## AskForLeave:
- Everyone can request for leave and the leave request flows to every supervisor recursively until the highest one. Once one of the supervisor refuses the request, the request is rejected, or the request is approved.
- A leave request from a guy with a highest level in his/her department will be approved immediately by him/herself. 

## Todo List
- You can add todo tasks for yourself and they show in different colors according to how long from now to the dead line.
- In home page, only unfinished tasks are listed sorted by dead line asc.
- In todo task list page, unfinished tasks come first sorted by dead line asc and finished ones come after sorted by dead line desc.

