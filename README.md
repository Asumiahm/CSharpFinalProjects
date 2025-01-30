# Blood Bank API

An API for managing blood donors, inventory, requests, recipients, and donations using **.NET Core** and **MongoDB**.

## Features
**CRUD Operations** for:
- **Donors**
- **Inventory**
- **Requests**
- **Recipients**
- **Donations**
- **Input Validation**
- **Pagination for large datasets**

## Tech Stack
- **Backend:** ASP.NET Core Web API
- **Database:** MongoDB
- **Authentication:** None (replaced with Input Validation)
- **Documentation:** Swagger

## Getting Started

### Prerequisites
Ensure you have the following installed:
- [.NET 6 or later](https://dotnet.microsoft.com/en-us/download)
- [MongoDB](https://www.mongodb.com/try/download/community)

### Clone the Repository
```bash
git clone https://github.com/yourusername/BloodBankAPI.git
cd BloodBankAPI
```

### Configure MongoDB Connection
Update the `appsettings.json` file with your MongoDB connection string:
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017"
  }
}
```

### Install Dependencies
```bash
dotnet restore
```

### Run the Application
```bash
dotnet run
```

### Access Swagger API Docs
Once the API is running, open **Swagger UI** in your browser:
👉 [`https://localhost:5001/swagger`](https://localhost:5001/swagger)

## API Endpoints

### Donors
| Method | Endpoint          | Description                  |
|--------|-------------------|------------------------------|
| **GET** | `/api/donors`      | Get all donors (with pagination) |
| **GET** | `/api/donors/{id}` | Get donor by ID              |
| **POST** | `/api/donors`      | Create a new donor           |
| **PUT** | `/api/donors/{id}` | Update a donor               |
| **DELETE** | `/api/donors/{id}` | Delete a donor               |

### Inventory
| Method | Endpoint          | Description                  |
|--------|-------------------|------------------------------|
| **GET** | `/api/inventory`      | Get all blood inventory (with pagination) |
| **GET** | `/api/inventory/{id}` | Get inventory by ID          |
| **POST** | `/api/inventory`      | Add new blood inventory      |
| **PUT** | `/api/inventory/{id}` | Update inventory details     |
| **DELETE** | `/api/inventory/{id}` | Delete inventory            |

### Requests
| Method | Endpoint          | Description                  |
|--------|-------------------|------------------------------|
| **GET** | `/api/requests`    | Get all blood requests (with pagination) |
| **POST** | `/api/requests`    | Create a new blood request   |

### Recipients
| Method | Endpoint          | Description                  |
|--------|-------------------|------------------------------|
| **GET** | `/api/recipients`  | Get all recipients (with pagination) |
| **GET** | `/api/recipients/{id}` | Get recipient by ID          |
| **POST** | `/api/recipients`  | Create a new recipient       |
| **PUT** | `/api/recipients/{id}` | Update recipient details     |
| **DELETE** | `/api/recipients/{id}` | Delete recipient           |

### Donations
| Method | Endpoint           | Description                  |
|--------|--------------------|------------------------------|
| **GET** | `/api/donations`    | Get all donations (with pagination) |
| **POST** | `/api/donations`    | Create a new donation        |

## Input Validation

- Input validation is used across some of API routes to ensure correct data is provided.
- Invalid input will return a **400 Bad Request** with a descriptive error message.(clear error handling)

## Pagination

To paginate results, use the following query parameters:
- **`page`**: Page number 
- **`pageSize`**: Number of items per page 

Example:
```
GET /api/donors?page=2&pageSize=5
```

## Contributing
1. Fork the repository
2. Create a new branch (`git checkout -b feature-branch`)
3. Commit changes (`git commit -m "Add new feature"`)
4. Push to your branch (`git push origin feature-branch`)
5. Submit a Pull Request
