# SignalR Live Chat Application

This is a simple real-time chat application built using **SignalR** for real-time communication and **Angular** for the client-side interface. The application allows users to register, log in, and chat with other users in real-time.

## Features

- **Real-time Messaging**: Users can send and receive messages instantly.
- **User Status**: Displays whether a user is online or offline.
- **User Registration**: Users can register with a name and avatar.
- **User Login**: Users can log in to access the chat interface.
- **Chat History**: Displays previous messages between users.
- **Responsive Design**: The client-side interface is mobile-friendly.

## Technologies Used

### Backend
- **ASP.NET Core**: Used to build the Web API.
- **SignalR**: For real-time communication.
- **Entity Framework Core**: For database operations.
- **SQLite**: As the database provider.

### Frontend
- **Angular**: For building the client-side application.
- **Bootstrap**: For responsive design.
- **Font Awesome**: For icons.

## Prerequisites

- **.NET SDK** (version 9.0 or higher)
- **Node.js** (version 18 or higher)
- **Angular CLI** (version 19.2.9 or higher)

## Setup Instructions

### Backend Setup

1. Navigate to the backend project directory:
```bash
cd SignalRWeb.API
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Apply migrations and update the database:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. Run the backend server:
```bash
dotnet run
```

  The backend server will start at `http://localhost:5012`.

### Frontend Setup

1. Navigate to the frontend project directory:
```bash
cd ChatAppClient
```

2. Install dependencies:
```bash
npm install
```

3. Start the Angular development server:
```bash
ng serve
```

   The frontend application will be available at `http://localhost:4200`.

## Usage

1. Open the frontend application in your browser at `http://localhost:4200`.
2. Register a new user by providing a name and avatar.
3. Log in with the registered user.
4. Select a user from the list to start chatting.
5. Messages will be delivered in real-time.

## Project Structure

### Backend (`SignalRWeb.API`)

- **Controllers**: Contains the `ChatsController` for handling API requests. 
- **Hubs**: Contains the `ChatHub` for SignalR communication.
- **Models**: Defines the `Chat` and `User` models.
- **Context**: Contains the `ApplicationDbContext` for database operations.

### Frontend (`ChatAppClient`)

- **Components**:
  - `register`: Handles user registration.
  - `login`: Handles user login.
  - `home`: Displays the chat interface.
- **Models**: Defines the `ChatModel` for chat data.
- **Services**: Manages API and SignalR communication.