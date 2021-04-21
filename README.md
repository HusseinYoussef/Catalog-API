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

- General: Retrieve all items.
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

### GET api/items/{id}

- General: Retrieve the specified item.
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

### POST api/items

- General: Create a new item.
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

### PUT api/items/{id}

- General: Update the specified item.
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

### DELETE api/items/{id}

- General: Delete the specified item.
- Example:

  ```
  curl -X 'DELETE' \
  'http://localhost:5002/api/Items/8616a990-00eb-4fe2-a82f-7e82f0d00617' \
  -H 'accept: text/plain'
  ```

- Response: No Content

### POST /register

- General: Register a new user.
- Example

  ```
    curl -X 'POST' \
    'http://localhost:5002/register' \
    -H 'accept: text/plain' \
    -H 'Content-Type: application/json' \
    -d '{
      "email": "Renfri@gmail.com",
      "password": "Test@1234"
    }'
  ```

- Response:

  ```
  {
    "token": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJSZW5mcmlAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6IlJlbmZyaUBnbWFpbC5jb20iLCJJZCI6IjgwMDc1YmFiLWQ3YjgtNDk4ZC1hNTM3LTc5ZjY3NDZjNDgzZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3NpZCI6IjhkZTc4MmY1LTgyMDMtNGNmZi1hZmJjLWNlNjg5ODg1ZDUxOCIsIm5iZiI6MTYxOTAzNDUwMywiZXhwIjoxNjE5MDQxNzAzfQ.Dow3BdNQT3y7w7EuY-o9YJalfSw-xs8Bm1Gz-9iuOjk"
  }

  ```
