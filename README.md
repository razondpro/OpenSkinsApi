# OpenSkinsApi

`OpenSkinsApi` is a test project developed for the Backend Hackathon organized by Mobile World Capital. The project is designed to showcase the use of Domain-Driven Design (DDD) principles, Clean Architecture, and CQRS (Command Query Responsibility Segregation) pattern in building a scalable and maintainable service.

The project is built using .NET Core, and uses a modular architecture with each module representing a distinct domain of the application. The Skins module is included as an example of how to implement a module using the DDD approach.

It has been designed to be easily extensible and maintainable, with a focus on separation of concerns and testability. The project includes unit tests for the `Skins` module, which demonstrate how to test the various components of the module using the DDD approach.

Overall, `OpenSkinsApi` is a demonstration of how to build a modern, scalable, and maintainable projects using the latest technologies and best practices.

## Architecture

The project is divided into the following layers:

- **Config**: Contains the configuration for the application.
- **Shared**: Contains the shared code for the application, including the abstractions for the `Domain` and `Application` layers, and the implementation details of external services in the `Infrastructure` layer.
- **Modules**: Contains the modules of the application, such as the `Skins` module, which includes the `Domain`, `Application`, and `Infrastructure` subfolders.

The `Skins` module contains the following subfolders:

- `Domain`: Contains the domain entities, value objects, and domain services for the `Skins` module.
- `Application`: Contains the application services, which orchestrate the domain logic and handle the application-specific use cases for the `Skins` module.
- `Infrastructure`: Contains the implementation details of the `Skins` module, such as the database access, http services, and other infrastructure concerns.

In the `Skins` module, the `Skin` entity is the aggregate root, which means that it is the main entity that all other entities in the module depend on. The `Skin` entity represents a skin item that can be owned by one or more users.

The `Owner` entity represents a user who owns one or more skins. In the context of the Skins module, it makes sense to use the term `Owner` instead of `User`, because the entity is specifically related to the ownership of skins.

The `Purchase` entity represents a purchase transaction between an `Owner` and a `Skin`. This entity is used to track the ownership of skins over time, and to provide a history of ownership for each skin.

Overall, the entities in the `Skins` module are designed to represent the domain concepts of skin ownership and purchase transactions, and to provide a flexible and scalable data model for the module.

**Important:**

Since there is no `authentication` or `authorization` required in the hackathon project, a default Owner is created during migrations. This Owner entity is used to associate all Skin entities with an owner, and to provide a simple authentication mechanism for the module.

To implement this authentication mechanism, the Owner entity is created during migrations with a default email address. This email address is then used to add a claim to each request using a middleware. The claim represents the authenticated Owner entity, and can be used to authorize access to certain resources or actions within the module.

Overall, this authentication mechanism provides a simple way to associate Skin entities with an owner, and to provide a basic level of authentication for the module. However, it is important to note that this mechanism is not secure or scalable, and should not be used in a production environment.

---

## Installation

To install this Service you will need to have Docker Desktop installed and up-to-date. Once you have Docker Desktop installed, follow these steps:

1. Clone the repository to your local machine.
2. Open a terminal window and navigate to the root directory of the project.
3. Run the following command to build the Docker image for the project:

```bash
docker compose up -d
```

This command will start the MySQL container and the skins-app container, which contains the Skins module.

**Note**: If the ports 3306 and 5108 are already in use on your local machine, the docker-compose up command may fail. In this case, you will need to modify the docker-compose.yaml file to use different ports, or make the ports available on your local machine.

4. Once the containers are running, you can access the Swagger UI for the Skins module at the following URL:

```bash
http://localhost:5108/swagger
```

The project defines the following routes:

- **/api/skins/available**: This route supports the GET HTTP method and returns a list of available skins that can be purchased by the user.

- **/api/skins/{id}**: This route supports the GET HTTP method and returns a specific skin by its ID. The ID is passed as a path parameter in the URL.

- **/api/skins/purchase**: This route supports the POST HTTP method and allows a user to purchase a skin. The skin ID is passed in the request body as a JSON object.

- **/api/skins/myskins**: This route supports the GET HTTP method and returns a list of skins that the user has purchased.

- **/api/skins/purchase/color**: This route supports the PUT HTTP method and allows a user to change the color of a purchased skin. The skin ID and new color are passed in the request body as a JSON object.

- **/api/skins/purchase/{purchaseId}**: This route supports the DELETE HTTP method and allows a user to delete a purchased skin. The purchase ID is passed as a path parameter in the URL.

Note that the available `colors` for a skin are limited to the following options:

- Red
- Green
- Blue
- Yellow
- Orange
- Purple
- Pink
- Brown
- Black
- White

When making a request to change color route, the new color for the skin should be specified as an integer between 0 and 9, where each integer corresponds to one of the available colors. For example, an integer value of 0 represents the color Red, while an integer value of 1 represents the color Green, and so on.

The `Type` enum is defined in the OpenSkinsApi.Modules.Skins.Domain.Enums namespace, and represents the different types of skins that can be purchased in the Skins module. The Type enum includes the following values:

- Normal: Represents a normal skin, which is the most common type of skin.
- Rare: Represents a rare skin, which is less common than a normal skin.
- Epic: Represents an epic skin, which is even less common than a rare skin.
- Legendary: Represents a legendary skin, which is the rarest and most valuable type of skin.

Note that the `Type` enum values are represented as integers internally, with Normal being 0, Rare being 1, Epic being 2, and Legendary being 3. This allows for easy comparison and sorting of skin types in the codebase.

---
