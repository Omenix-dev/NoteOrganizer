# NOTE ORGANIZER PROJECT <Using ASP.NET CORE 6.0>

## broiler plate consist of 4 project and one solution

    1.) NoteOrganizer : API project layer
    2.) NoteOrganizer.Model : contains api models
    3.) NoteOrganizer.DataAccess : data access layer
    4.) NoteOrganizer.Core : Contains the business Logic for the services

## Design Pattern

    CLEAN ARCHITECTURE

## Technology

    C#, EntityFrameWOrkCore(ORM), .NetCore, ASP.NETCore
    fluentAPI, fluentValidation, UnitOfWork

## ENDPOINT
BASR URL = https://localhost:7076/

USER CONTROLLER
POST
​/api​/User​/User​/register

POST
​/api​/User​/User​/login

NOTE CONTROLLER
POST
/api/Note/Note/CreateNoteById
POST
​/api​/Note​/Note​/UpdateNoteById

DELETE
​/api​/Note​/Note​/DeleteNoteById

GET
​/api​/Note​/Note​/GetNotesById

GET
​/api​/Note​/Note​/GetNotesByUserId
    

## Instruction to run Application

    1. to run the application you must have an already running / installed sql server
    2. then do uppdate database and make sure that the connection string marches your database connection string

## Note : Swagger Documentation

    1.)  https://localhost:7076/swagger/index.html
