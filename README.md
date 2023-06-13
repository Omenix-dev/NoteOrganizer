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

    

## Instruction to run Application

    1. install docker and docker-compose engine
    2. go to directory of the application
    3. enter "docker-compose build"
    4. enter "docker-compose up -d"
    5. for swagger documentation go to when the ochestrated container is running 

## Note : docker volumes

    1.)  the database in the docker compose runs on a temporal data volume to create a the storage
    2.)  open the docker compose file and uncomment the volume to enable the permanent database storage
