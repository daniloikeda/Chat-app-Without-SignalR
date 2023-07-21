# ChatApp (ASP.NET Core 6.0)

Welcome to ChatApp! This is a real-time chat application developed using ASP.NET Core 6.0. The app allows users to join an open chat room by providing their name through a WebSocket connection. Users can then exchange messages in real-time with other participants in the chat room.

## Features

- Real-time messaging: Instantly send and receive messages in the open chat room.
- No user authentication: Users can join the chat room without the need for authentication or authorization.
- Online status: Track who is currently online in the chat room.

## Getting Started

Follow these instructions to set up and run ChatApp on your local machine for development and testing purposes.

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/your-username/ChatApp.git
cd ChatApp
```

2. Restore .NET dependencies:

```bash
dotnet restore
```

3. Build the application:

```bash
dotnet build
```

### Usage

1. Run the application:

```bash
dotnet run
```

2. Connect to the chat room:

Open your favorite WebSocket client or browser and connect to `wss://localhost:7093/chat`.

3. Register your name:

Upon connecting to the WebSocket, the server will prompt you to provide your name. Enter your desired username, and you'll be registered in the open chat room.

4. Start chatting:

Once registered, you can start sending and receiving messages in real-time.

### Testing

To run the unit tests:

```bash
dotnet test
```

## Technologies Used

- ASP.NET Core 6.0
- WebSocket

## Contributing

Contributions are welcome! If you find any bugs or want to add new features, feel free to open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

Special thanks to the ASP.NET Core community for their excellent tools and resources.

---

Happy Chatting! 🎉

Feel free to contact the developer at [dcidaniloikeda@hotmail.com](mailto:dcidaniloikeda@hotmail.com) for any questions or feedback.
