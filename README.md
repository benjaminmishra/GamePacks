<div align="center">
  <h1>Code Assignment</h1>
  <img src="https://img.shields.io/badge/type-backend-FF5A00">
  <img src="https://img.shields.io/badge/language-any-00AFFF">
  <img src="https://img.shields.io/badge/concepts-REST APIs & databases-05C805">
  <img src="https://img.shields.io/badge/est. duration-max 4 hours-F28EB4">
  <br/><br/>
</div>

The goal with this assignment is to evaluate your ability to write good quality code in
a practical exercise.

After the assignment has been submitted you will be invited to
[a follow-up interview](#the-follow-up-interview) with our developers.

<details><summary><b>Table of contents</b></summary>

- [The task: a simplified game packs API](#the-task-a-simplified-game-packs-api)
  - [Requirements](#requirements)
    - [API](#api)
    - [Database](#database)
    - [Tests](#tests)
    - [Code style](#code-style)
    - [Environment and infrastructure](#environment-and-infrastructure)
  - [Documentation](#documentation)
- [Technologies and tools](#technologies-and-tools)
- [The follow-up interview](#the-follow-up-interview)
- [Checklist](#checklist)

</details>

## The task: a simplified game packs API

In a game, users may purchase packs that add content to their game. The packs can contain new locations, new characters, and a plethora of other fun and
exciting things. A pack may also contain other packs, where if you buy the parent pack you also receive the sub packs.

In this assignment we want you to **create a simplified remote API for game packs** that are **stored in a database**. It is not expected to take more than 4 hours, so only take the time you feel you need!

### Requirements

#### API

The API **must** provide following operations and should be implemented in C# using ASP.NET:

- Add new pack to the database.
- List all packs that are in the database.
- Return complete pack content with all its dependencies.

##### The `pack` entity

A pack object should be identified by an id and have a name and an active flag. You may add additional information. It could look something like this:

```json
{
  "id": "pack.school",
  "packName": "The School Pack",
  "active": true,
  "price": 10,
  "content": ["furniture.whiteboard"],
  "childPackIds": ["pack.classroom", "pack.playground"]
}
```

#### Database

The packs should be saved in an appropriate database.

You are free to use whatever database you want, and structure it in a way you think works best for the task.

#### Tests

Please include tests for you code, similar to how you would test a service running in production.

#### Code style

Feel free to use whatever code style, lint rules, or conventions that you prefer. Just remember that the code should be easy for the reviewer to read and understand.

#### Environment and infrastructure

The `docker-compose.yml` should be configured so that both the service and the database can be started with a single `docker compose -up` command. It should not require the reviewer to configure anything else on their side (e.g. environment variables).

The service and database should only run locally and not interact with any remote sources.

### Documentation

The code should be documented enough so that the reviewer can understand it and try it out.

The readme should at least include:

- Setup instructions (how to install and run the project).
- Usage instructions (how to query the endpoints).
- Any extra information that might be good for the reviewer to know.

## Technologies and tools

You are free to use any libraries or tools that you might need.

## The follow-up interview

After you have submitted your code you will be invited to a follow-up interview with our developers.

During this interview we will discuss your code, such as your choice of technologies and how you implemented the API. The goal is to use the assignment as a base for discussion, _not_ to punch holes in it or look for gotchas and flaws.

**Depending on the role, we might ask questions like:**

- _Explain your reasoning around the project, how did you go about starting it and were there any problems?_
- _Why did you choose the tools that you did?_
- _Is there anything you would like to improve, like code choices, tools used etc?_
- _Is there anything missing in this service? What would be the next step if you had more time?_

## Checklist

That was a lot of information! To help you ensure that you've included everything, here's a quick
checklist.

**My submission**:

- [ ] Includes all [expected endpoints](#api).
- [ ] Has [sufficient tests](#tests) that are passing.
- [ ] Has [sufficient documentation](#documentation).
- [ ] Can be [started with a single `docker compose -up`](#environment-and-infrastructure) command.
