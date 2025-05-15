# TownSquare - where Community thrives

![TownSquare Logo](https://i.imgur.com/GpPYafA.png)


**TownSquareAPI** powers the backend of *TownSquare*, a platform designed to bring people together and foster real connections within local communities.

In a world where social media often isolates rather than connects, ComApp aims to bridge the gap by providing dedicated spaces for communities to engage, share, and communicate.

Features
--------

-   🌍 **Functioning Google Maps API integration**

-   🔐 **Register or Login capabilities**

-   📍 **Create and display Pins on the map**

-   📰 **Publish Newsletters for the community**

-   🆘 **Post and respond to Help Posts**

-   ✏️ **Edit Profile Name or Bio**

-   🗑️ **Delete Profile**

-   🚦 **Rate-Limiting**

### Known Issues🐛:

-   ✅ ~~Can't create membership requests.~~
-   ⏳ Do to using a free Host, Response Times are quite slow.
-   ❌ Deleting a User with a active membership request is not possible.


Overview
--------

TownSquareAPI provides **RESTful** endpoints for managing:

-   Community memberships

-   Help posts

-   Location pins

-   User authentication

-   General community posts

**Base Production URL:** http://mc.dominikmeister.com/api

**Base Development URL:** https://townsquareapi-dev.onrender.com/

**Swagger URL:** https://townsquareapi0-1.onrender.com/swagger/index.html *-- Temporarly URL, will be replaced soon*

API Endpoints
-------------

### 🏘️ Community

#### Get All Communities

```
GET /api/Community/GetAll
```

-   **Description:** Retrieve a list of all available communities.

-   **Parameters:** None

-   **Response:**

    -   `200 OK`: List of communities

#### Request Membership

```
POST /api/Community/RequestMembership
```

-   **Description:** Request to join a community.

-   **Parameters:**

    -   `userId` (int32) -- User ID

    -   `communityId` (int32) -- Community ID

-   **Response:**

    -   `200 OK`: Membership request sent

* * * * *

### 🆘 Help Posts

#### Get All Help Posts

```
GET /api/HelpPost/GetHelpPosts
```

-   **Description:** Retrieve all help posts.

-   **Parameters:** None

-   **Response:**

    -   `200 OK`: List of help posts

#### Add a Help Post

```
POST /api/HelpPost/AddHelpPost
```

-   **Description:** Create a new help post.

-   **Request Body:**

```
{
  "id": 0,
  "title": "string",
  "description": "string",
  "price": 0,
  "telephone": "string",
  "helpposttime": "2025-02-20T20:07:41.614Z",
  "user_id": 0
}
```

-   **Response:**

    -   `200 OK`: Help post added successfully

* * * * *

### 🔐 Authentication

#### User Login

```
POST /api/Login/Login
```

-   **Description:** Authenticate and retrieve a user token.

-   **Request Body:**

```
{
  "username": "string",
  "password": "string"
}
```

-   **Response:**

    -   `200 OK`: Successful authentication

* * * * *

### 📍 Pins

#### Get All Pins

```
GET /api/Pin/GetPins
```

-   **Description:** Retrieve all pin locations.

-   **Parameters:** None

-   **Response:**

    -   `200 OK`: List of pins

#### Insert a Pin

```
POST /api/Pin/InsertPin
```

-   **Description:** Create a new pin on the map.

-   **Request Body:**

```
{
  "id": 0,
  "user_id": 0,
  "title": "string",
  "description": "string",
  "posttime": "2025-02-20T20:07:41.615Z",
  "x_cord": 0,
  "y_cord": 0,
  "community_id": 0,
  "pintype": 0
}
```

-   **Response:**

    -   `200 OK`: Pin successfully added

* * * * *

### 📰 Posts

#### Get Community Posts

```
GET /api/Post/GetPosts/{isNews}
```

-   **Description:** Retrieve community posts.

-   **Parameters:**

    -   `isNews` (int32) -- Filter by news posts (1 for true, 0 for false)

-   **Response:**

    -   `200 OK`: List of posts

#### Create a Post

```
POST /api/Post/CreatePost
```

-   **Description:** Create a new community post.

-   **Request Body:**

```
{
  "content": "string",
  "user_id": 0,
  "isnews": 0,
  "community_id": 0
}
```

-   **Response:**

    -   `200 OK`: Post created successfully

* * * * *

### 👤 User

#### Register a User

```
POST /api/User/Register
```

-   **Description:** Register a new user.

-   **Request Body:**

```
{
  "id": 0,
  "name": "string",
  "email": "string",
  "password": "string",
  "bio": "string"
}
```

-   **Response:**

    -   `200 OK`: User registered successfully

#### Update User 

```
PUT /api/User/UpdateBio/{userId}
```

-   **Description:** Update a user's name and bio.

-   **Parameters:**

    -   `userId` (int32) -- User ID

-   **Request Body:**

```
{
  "newUsername": "string",
  "newBio": "string"
}
```

-   **Response:**

    -   `200 OK`: Bio updated successfully

 #### Delete User 

```
DELETE /api/User/UpdateBio/{userId}
```

-   **Description:** Delete a User.

-   **Parameters:**

    -   `userId` (int32) -- User ID

-   **Response:**

    -   `200 OK`: User deleted.

## Info
**TownSquaresAPI** is a REST-API for a mobile application called [TownSquare](https://github.com/CivicSolutions/TownSquare), developed using `.NET MAUI`, currently optimized primarily for Android devices. This project was created as part of a school assignment and is still in the development phase. While significant progress has been made, several key features are still under development.
## Authors

- [Sergio Soares](https://www.github.com/Sergio-404)
- [Dominik Meister](https://github.com/MinenMaster)
- [Luka Ljubisavljevic](https://www.github.com/Lurx381)
- [Kai Parkinson](https://www.github.com/Kai1732)
## Support

For contact, email Sergio.SoaresFonseca@protonmail.com.
