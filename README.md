<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Razor Pages Dating Site</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
      margin: 0;
      padding: 0;
      background-color: #f9f9f9;
      color: #333;
    }

    header {
      background-color: #ff4d6d;
      color: white;
      padding: 1.5rem;
      text-align: center;
    }

    main {
      max-width: 900px;
      margin: 2rem auto;
      padding: 0 1rem;
    }

    h1, h2, h3 {
      color: #ff4d6d;
    }

    code {
      background-color: #eee;
      padding: 2px 6px;
      border-radius: 4px;
      font-family: monospace;
    }

    pre {
      background-color: #f0f0f0;
      padding: 1rem;
      border-radius: 5px;
      overflow-x: auto;
    }

    a {
      color: #ff4d6d;
      text-decoration: none;
    }

    a:hover {
      text-decoration: underline;
    }

    ul {
      list-style: square;
      padding-left: 20px;
    }

    footer {
      text-align: center;
      margin-top: 3rem;
      padding: 1rem;
      font-size: 0.9rem;
      color: #666;
    }
  </style>
</head>
<body>

<header>
  <h1>💘 Razor Pages Dating Site</h1>
  <p>A secure dating web application built with .NET and Razor Pages</p>
</header>

<main>
  <section>
    <h2>🔐 Features</h2>
    <ul>
      <li><strong>User Authentication:</strong> Secure login system with hashed password storage.</li>
      <li><strong>Custom User Profiles:</strong> Users can create profiles with interests, bios, and more.</li>
      <li><strong>Advanced Match Filtering:</strong> Find compatible matches using dynamic search filters.</li>
      <li><strong>Mutual Match System:</strong> Users can only message if both show interest.</li>
      <li><strong>In-App Messaging:</strong> Private messaging system for date invitations.</li>
      <li><strong>Secure SQL Backend:</strong> Uses stored procedures to prevent SQL injection.</li>
    </ul>
  </section>

  <section>
    <h2>🛠️ Technologies Used</h2>
    <ul>
      <li><a href="https://dotnet.microsoft.com/en-us/" target="_blank">.NET Core</a></li>
      <li>Razor Pages</li>
      <li>SQL Server</li>
      <li>Entity Framework Core</li>
      <li>ASP.NET Core Identity</li>
    </ul>
  </section>

  <section>
    <h2>📂 Project Structure</h2>
    <pre>
/DatingApp
│
├── Pages/                # Razor Pages (UI)
├── Models/               # Entity and View Models
├── Data/                 # Database context and stored procedures
├── Services/             # Business logic and helpers
├── wwwroot/              # Static files (CSS, JS, images)
├── appsettings.json      # App configuration
└── Startup.cs            # Middleware setup and DI
    </pre>
  </section>

  <section>
    <h2>🚀 Getting Started</h2>
    <ol>
      <li><strong>Clone the repository</strong>
        <pre><code>git clone https://github.com/yourusername/dating-app.git
cd dating-app</code></pre>
      </li>
      <li><strong>Configure the Database</strong><br>
        Update <code>appsettings.json</code> with your SQL Server connection string.<br>
        Run the provided SQL script or use EF Core migrations.
      </li>
      <li><strong>Run the application</strong>
        <pre><code>dotnet build
dotnet run</code></pre>
      </li>
      <li>Visit <code>https://localhost:5001</code> in your browser.</li>
    </ol>
  </section>

  <section>
    <h2>🔒 Security</h2>
    <ul>
      <li>Passwords are hashed using ASP.NET Core Identity.</li>
      <li>Inputs are validated and sanitized to prevent injection/XSS attacks.</li>
      <li>Secure communication over HTTPS.</li>
    </ul>
  </section>

  <section>
    <h2>✍️ Author</h2>
    <p><strong>Your Name</strong></p>
    <p><a href="https://linkedin.com/in/yourprofile" target="_blank">LinkedIn</a> | 
       <a href="https://yourwebsite.com" target="_blank">Portfolio</a></p>
  </section>

  <section>
    <h2>📝 License</h2>
    <p>This project is for educational purposes. You may adapt or extend it, but please give credit.</p>
  </section>
</main>

<footer>
  &copy; 2024 Your Name. Built with 💗 and .NET.
</footer>

</body>
</html>
