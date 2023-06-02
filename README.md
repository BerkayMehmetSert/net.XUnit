# Net XUnit Testing Example

This is a simple example of an XUnit test project. This project provides an API for managing products. You can create,
update, delete, and list products.

## Installing

1. Clone the repository

```shell
git clone https://github.com/BerkayMehmetSert/net.XUnit.git
```

2. Install dependencies

```shell
dotnet restore
```

3. Create database

```shell
dotnet ef database update
```

4. Run the project

```shell
dotnet run
```

## Postman Collection

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/23538386-d8a67b39-5a51-4be9-a562-ac1e7dffe4fd?action=collection%2Ffork&collection-url=entityId%3D23538386-d8a67b39-5a51-4be9-a562-ac1e7dffe4fd%26entityType%3Dcollection%26workspaceId%3D81da7b17-d919-484f-81a7-a0ea4c8bd87a)

## Usage

### Products

**Get all products**

```bash
GET /api/product
```

Response body:

```json
[
  {
    "name": "Product 1",
    "description": "Description 1",
    "price": 110,
    "id": "7704a048-3f80-4c4a-53cd-08db637142ef"
  }
]
```

**Get product by id**

```bash
GET /api/product/{id}
```

Response body:

```json
{
  "name": "Product 1",
  "description": "Description 1",
  "price": 110,
  "id": "7704a048-3f80-4c4a-53cd-08db637142ef"
}
```

**Get product by name**

```bash
GET /api/product/name/{name}
```

Response body:

```json
{
  "name": "Product 1",
  "description": "Description 1",
  "price": 110,
  "id": "7704a048-3f80-4c4a-53cd-08db637142ef"
}
```

**Create product**

```bash
POST /api/product
```

Request body:

```json
{
  "name": "Product 1",
  "description": "Description 1",
  "price": 110
}
```

**Update product**

```bash
PUT /api/product/{id}
```

Request body:

```json
{
  "name": "Product 1",
  "description": "Description 1",
  "price": 110
}
```

**Delete product**

```bash
DELETE /api/product/{id}
```

## Testing

1. Run the tests

```shell
dotnet test
```

## Built With

* [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) - The framework used
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - The ORM used
* [XUnit](https://xunit.net/) - The testing framework used
* [Rider](https://www.jetbrains.com/rider/) - The IDE used
* [Postman](https://www.getpostman.com/) - The API collection tool used
