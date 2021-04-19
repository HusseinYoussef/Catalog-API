# Catalog API

Simple RESTful API to manage items.

## Tech Stack

- C#
- Asp.NetCore 3.1
- MongoDb
- Docker

## Getting Started

To get things up and running fast, you can go with the dockerizeed approach, run:

`docker-compose up`

To run locally, make sure have the following installed:

- Asp.NetCore 3.1+ SDK
- Mongo database

Then run `dotnet run` within the `Catalog API` folder

## API Reference

### Get /api/items

- Example:
  ````
  curl -X GET http://localhost:5002/api/Items \
  -H 'accept: text/plain'```
  ````
- Response:
  ```
  [
  {
      "id": "52d528b9-cce8-4427-a3f6-6e22b1a7869f",
      "name": "dress",
      "price": 100
  },
  {
      "id": "8632007c-dd91-4fa1-b9e9-a7a0f7efcc18",
      "name": "pants",
      "price": 40
  },
  {
      "id": "b77e5b52-1dac-4913-9e97-8e450f03fe4b",
      "name": "jacket",
      "price": 178
  }
  ]
  ```

GET api/items/{id}

- Example:
  ```
  curl -X GET http://localhost:5002/api/Items/52d528b9-cce8-4427-a3f6-6e22b1a7869f \
  -H 'accept: text/plain'
  ```
- Response:
  ```
  {
  "id": "52d528b9-cce8-4427-a3f6-6e22b1a7869f",
  "name": "dress",
  "price": 100
  }
  ```

POST api/items

- Example:

  ```
  curl -X 'POST' \
  'http://localhost:5002/api/Items' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": Gloves,
  "price": 20
  }'
  ```

- Response:

  ```
    {
        "id": "8616a990-00eb-4fe2-a82f-7e82f0d00617",
        "name": "Gloves",
        "price": 20
    }
  ```

PUT api/items/{id}

- Example:

  ```
  curl -X 'PUT' \
  'http://localhost:5002/api/Items/8616a990-00eb-4fe2-a82f-7e82f0d00617' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": Gloves,
  "price": 40
  }'
  ```

- Response: No Content

DELETE api/items/{id}

- Example:

  ```
  curl -X 'DELETE' \
  'http://localhost:5002/api/Items/8616a990-00eb-4fe2-a82f-7e82f0d00617' \
  -H 'accept: text/plain'
  ```

- Response: No Content

GET /auth

- Example

  ```
  curl -X 'GET' \
  'http://localhost:5002/auth' \
  -H 'accept: */*'
  ```

- Response:
  ```
  {
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQ2F0YWxvZyBVc2VyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZGF0ZW9mYmlydGgiOiI0LzE2LzIwMjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJOb3JtYWwgVXNlciIsIm5iZiI6MTYxODc5NzEwOCwiZXhwIjoxNjE4ODA0MzA4LCJpc3MiOiJDYXRhbG9nIGxvY2FsaG9zdCBBZG1pbiIsImF1ZCI6IkNhdGFsb2cgbG9jYWxob3N0IEFkbWluIn0.EEKGP_3YLAnqBYaB3UAvd0c2bx76VupYjRNlMzfK5Hw"
  }
  ```
