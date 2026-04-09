<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<br />
<div align="center">
  <a href="https://github.com/n1zen/ChurchAttendance">
    <img src="images/logo--white.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Church Attendance App</h3>

  <p align="center">
    A web application for tracking member and visitor attendance in churches.
    <br />
    <a href="https://github.com/n1zen/ChurchAttendance"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://fabac-attendance-app.onrender.com">View Demo</a>
    ·
    <a href="https://github.com/n1zen/ChurchAttendance/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/n1zen/ChurchAttendance/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

---

## Table of Contents

- [About The Project](#about-the-project)
  - [Built With](#built-with)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
- [Acknowledgments](#acknowledgments)

---

## About The Project

[![Product Screenshot][product-screenshot]](https://fabac-attendance-app.onrender.com)

The **Church Attendance App (FABAC)** is a web-based attendance management system built with ASP.NET Core. It allows churches to track member and visitor traffic across services, export attendance records to Excel, and manage data through a clean Razor Pages interface.

Key features include:
- Member and visitor attendance tracking
- Attendance history with searchable/filterable tables (jQuery DataTables)
- Excel/CSV export powered by ClosedXML
- JWT-based authentication via ASP.NET Core Identity
- PostgreSQL backend via Entity Framework Core (Npgsql)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

### Built With

[![.NET][dotnet-shield]][dotnet-url]
[![Bootstrap][Bootstrap.com]][Bootstrap-url]
[![jQuery][JQuery.com]][JQuery-url]
[![PostgreSQL][postgres-shield]][postgres-url]
[![Docker][docker-shield]][docker-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- [PostgreSQL 16+](https://www.postgresql.org/download/) (or Docker)
- [Docker](https://www.docker.com/) (optional, for local DB)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/n1zen/ChurchAttendance.git
   cd ChurchAttendance
   ```

2. Set up your database connection and environment variables. Create a `appsettings.Development.json` (or use User Secrets):
    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Host=localhost;Port=5432;Database=churchattendance;Username=postgres;Password=yourpassword"
      },
      "Jwt": {
        "Audience": "YourAudience",
        "Issuer": "YourIssuer",
        "Key": "SecretKey-Minimum-of-32-Characters"
      },
      "AdminSeed": {
        "Email": "admin@example.com",
        "Password": "ExamplePassword@1234"
      }
    }
    ```
    | Variable | Description | Example |
    | --- | --- | --- |
    | `ConnectionStrings:DefaultConnection` | PostgreSQL connection string | `Host=localhost;Port=yourport;Database=yourdatabasename;Username=yourusername;Password=yourpassword` |
    | `Jwt:Audience` | JWT token audience | `YourAudience` |
    | `Jwt:Issuer` | JWT token issuer | `YourIssuer` |
    | `Jwt:Key` | JWT token Key | `your-secret-key-min-32-chars` |
    | `AdminSeed:Email` | Seeded admin account email | `admin@example.com` |
    | `AdminSeed:Password` | Seeded admin account password | `ExamplePassword@1234` |

3. (Optional) Spin up a local PostgreSQL instance with Docker:
   ```sh
   docker run --name church-pg \
     -e POSTGRES_PASSWORD=yourpassword \
     -e POSTGRES_DB=churchattendance \
     -p 5432:5432 -d postgres:16
   ```

4. Apply EF Core migrations:
   ```sh
   dotnet ef database update
   ```

5. Run the application:
   ```sh
   dotnet run
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## Usage

Once running, navigate to `https://localhost:5001` (or the configured port). Log in with your credentials to begin recording attendance for services. Use the export button on any attendance list to download records as an Excel file.

_For more examples, refer to the [Documentation](https://github.com/n1zen/ChurchAttendance)_

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## License

Distributed under the MIT License. See [`LICENSE.txt`](LICENSE.txt) for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## Contact

Louie Izen B. Torres — torresizen070204@gmail.com

Project Link: [https://github.com/n1zen/ChurchAttendance](https://github.com/n1zen/ChurchAttendance)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

## Acknowledgments

- [ClosedXML](https://github.com/ClosedXML/ClosedXML) — Excel export
- [Scalar.AspNetCore](https://github.com/scalar/scalar) — API documentation UI
- [jQuery DataTables](https://datatables.net/) — Searchable/filterable tables
- [othneildrew/Best-README-Template](https://github.com/othneildrew/Best-README-Template) — README template

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/n1zen/ChurchAttendance.svg?style=for-the-badge
[contributors-url]: https://github.com/n1zen/ChurchAttendance/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/n1zen/ChurchAttendance.svg?style=for-the-badge
[forks-url]: https://github.com/n1zen/ChurchAttendance/network/members
[stars-shield]: https://img.shields.io/github/stars/n1zen/ChurchAttendance.svg?style=for-the-badge
[stars-url]: https://github.com/n1zen/ChurchAttendance/stargazers
[issues-shield]: https://img.shields.io/github/issues/n1zen/ChurchAttendance.svg?style=for-the-badge
[issues-url]: https://github.com/n1zen/ChurchAttendance/issues
[license-shield]: https://img.shields.io/github/license/n1zen/ChurchAttendance.svg?style=for-the-badge
[license-url]: https://github.com/n1zen/ChurchAttendance/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/louie-izen-torres-4557243ab
[product-screenshot]: images/screenshot.png
[dotnet-shield]: https://img.shields.io/badge/.NET_10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white
[dotnet-url]: https://dotnet.microsoft.com/en-us/
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[JQuery.com]: https://img.shields.io/badge/jQuery-0769AD?style=for-the-badge&logo=jquery&logoColor=white
[JQuery-url]: https://jquery.com
[postgres-shield]: https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white
[postgres-url]: https://www.postgresql.org/
[docker-shield]: https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white
[docker-url]: https://www.docker.com/