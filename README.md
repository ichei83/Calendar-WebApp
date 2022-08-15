# Calendar-App

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.com/calendargroup/calendar-app.git
git branch -M main
git push -uf origin main
```

## Name
Calendar Application.


## Description
Settinng evenst with start and end date & time in the calender.
Event can be deleted, added or updated by drag&drop.
You can view history and future events by going back\Next in calendar.


## Pre Installation
Mongo:
Install mongo compass from: https://www.mongodb.com/docs/compass/current/install/

Docker:
Install docker or docker desktop from: https://docs.docker.com/desktop/install/windows-install/

AngularCalendarApp
Install Node.js from: https://nodejs.org/en/download/
Install angular cli

CalendarApp
Required software: Visual Studio.
Install next dependencies using nuget:

 -1 MongoDB.Bson
 -2 MongoDB.Driver
 -3 MongoDB.Driver.Core
 -4 Swashbuckle.AspNetCore

## Installation and Build Commands
AngularCalendarApp
Go to download folder ./AngularCalendarApp and open cmd window
run 'npm install' in target have all dependencies
npm run build # build to a directory
npm run start # continously build, as a server

CalendarApp
Required software: Visual Studio.
Install next dependencies using nuget:

 -1 MongoDB.Bson
 -2 MongoDB.Driver
 -3 MongoDB.Driver.Core
 -4 Swashbuckle.AspNetCore

Mongo: 
- Open mongo compass software
- Create manual CalendarDb database and new collection called: CalendarMongo


## Run Applications
There are 2 options to run the system:
 - Using localhost
 - Using Docker-compose

 Localhost Option:

 AngularCalendarApp:
  - Go to download folder ./AngularCalendarApp and open cmd window
  - run npm start or ng serve is angular cli is installed
  Client web application is up and listening on: http://localhost:4200/

CalendarApp:
 - Open CalendarApp1 solution using visual studio (considering project already have all relevant dependencies) just build and run   as CalendarApp application (development mode)
  Calendar Server web application is up and listening on: http://localhost:5000/
  * Using this link: http://localhost:5000/swagger/index.html will open Swashbuckle and will use for testing relevant api

 Docker-compose Option:

  - AngularCalendarApp:
    go to ./AngularCalendarApp and open cmd window:
    - docker-compose build
    - docker-compose up  
    Client web application is up and listening on: http://localhost:4014/

  - CalendarApp1:
  go to ./CalendarApp1  open cmd window:
    - docker-compose build
    - docker-compose up  
    Calendar Server web application is up and listening on: http://localhost:5000/
    * Using this link: http://localhost:5000/swagger/index.html will open Swashbuckle and will use for testing relevant api
    go to ./CalendarApp1  open cmd window:
    - docker-compose build
    - docker-compose up 

Docker Verification:
- go to docker desktop, and verify all container are up an running

## Usage
Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support
For any issue, please contact: ichei83@gmail.com

