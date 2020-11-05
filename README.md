# Curriculum Vitae Builder
This is a service to build and manipulate CV's

# Setup

To get the Curriculum Vitae Builder service up and running locally on your machine, just follow this step by step guide.

## Requirements

* Docker - You will need Docker desktop installed and running. Docker desktop can be downloaded from the following pages:

  https://hub.docker.com/editions/community/docker-ce-desktop-windows/

## Steps

1. First you will need to update your appsettings.json file. You need to updated the `"ConnectionString"` property in the `"PostgreSql"` object to be: 
    
    ```Json
    "Server=localhost;Port=5432;Database=curriculum_vitae_builder;UserName=postgres;Password=password;"
    ```
    Your appsettings.json files should now look like this:

    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      },
      "AllowedHosts": "*",
      "Postgres": {
        "ConnectionString": "Server=localhost;Port=5432;Database=curriculum_vitae_builder;UserName=postgres;Password=password;"
      }
    }
    ```

2. From a terminal, navigate to the Curriculum Vitae Builder folder inside the solution, once there run:
    ```
    docker-compose up
    ```

    This should start setting up all of the programs in a container that are needed for the Curriculum Vitae Builder service to function, once this is done, move onto the next step.

3. In a browser, navigate to the localhost:7070 to open pgAdmin, now login using the credentials specified in the dockerfile. In this case it should be the following:

    ```
    Email: `hello@hello.com`

    Password: `password`
    ```

4. Now that you're in, you will need to create a server, near the top left of the window you should see something that says Servers, right click on this and follow Create -> Server, this will open a modal where you will configure the server.

5. First name your server, in the General tab of the modal, fill in the Name field, this can be anything you want. Next go to the Connection tab and update the following fields:

    ```
    Host name/address: host.docker.internal
    
    Port: 5432

    Username: postgres
    
    Password: password
    ```

    The Username & Password are set to what was specified in the `appsettings.json` file.

    Once this is done, press save.

6. Once the server has been created you will need to create the Curriculum Vitae Builder database, to do this, expand the list of servers in the top left of the window. You should now see the server you have just created, expand that server, right click on the Databases section and follow Create -> Database, this will open up another modal. In the General tab, set the Database field to:

    ```
    curriculum_vitae_builder
    ```
     and press save.

7. Setting up the server and database should now be complete, all you need to do now is run the Assets API.
