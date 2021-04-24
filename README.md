# Catalog API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/dwyl/esta/issues)

Simple RESTful API performing CRUD operations to manage items. The API is secured using JWT authorization.

## Tech Stack

- C#
- Asp.NetCore 3.1
- EntityFramework
- MongoDB
- MySQL
- Docker

## Getting Started

To get things up and running fast, you can go with the dockerizeed approach, run:

`docker-compose up`

To run locally, make sure you have the following installed:

- Asp.NetCore 3.1+ SDK
- MongoDB
- MySQL

Then run `dotnet run` within the `Catalog API` folder after setting up the below configurations.

## Environment Variables

Make sure you set these configurations either by using environment variables or the user-secrets manager tool

- MongoDbSettings:Host
- MongoDbSettings:Username
- MongoDbSettings:Password
- MysqlDbSettings:Host
- MysqlDbSettings:Username
- MysqlDbSettings:Password
- JwtSettings:Key

## Setting Up Database

Apply the migrations in `migrations` folder to set up the Mysql database by running:

`dotnet ef database update`

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
    "token": "{JWT Token}"
  }
  ```

### POST /login

- General: Login with an existing user.
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
    "token": "{JWT Token}"
  }
  ```
