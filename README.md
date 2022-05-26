# WebApi
## Table of Contents
1. Introduction
2. Demonstration Images
3. Dependencies
4. Setup and User Guide
5. Contributors

***

# 1. Introduction

In this project we created Web-Api server that supports the following functionality by returning answers in Json fromats.<br/>
Suppose our domain is `http://localhost:7000/` then:
* For the address `http://localhost:7000/api/contacts/`:
  * The GET operation will return all contacts of the current user.
  * The POST operation will create a new contact for the current user.
* For the address `http://localhost:7000/api/contacts/id`:
  * The GET operation will return the contact details with that `id`.
  * The PUT operation will edit the contact details with that `id`.
  * The DELETE operation will delete the contact with that `id`.
* For the address `http://localhost:7000/api/contacts/id/messages/`:
  * The GET operation will return all the messages that had been sent or received with the contact that `id` belongs to him.
  * The POST operation will add a new message between the current user and the contact that `id` belongs to him.
* For the address `http://localhost:7000/api/contacts/id/messages/id2`:
  * The GET operation will return the message with the `id2` between the current user and the contact with this `id`.
  * The PUT operation will edit the message content with the `id2` between the current user and the contact with this `id`.
  * The DELETE operation will deletethe message with the `id2` between the current user and the contact with this `id`.
* For the address `http://localhost:7000/api/invitations/`:
  * The POST operation will contain an invitation to a new conversion.
* For the address `http://localhost:7000/api/transfer/`:
  * The POST operation will contain a message to send to one of the contacts.
  

***

# 2. Demonstration Images
### Get Contacts of Admin user:
![localhost7000apicontactsGET](https://user-images.githubusercontent.com/92301625/170488564-ebcb0b8e-61f0-44aa-b6df-65440743bbb5.png)
### invitation of a contact:
![invitation](https://user-images.githubusercontent.com/92301625/170489186-19da3448-ea44-46ba-ab61-897ff02b1d11.png)
### Get contact by id:
![getcontactbyid](https://user-images.githubusercontent.com/92301625/170489463-2321ebe0-7d32-4e8d-8377-edc92fb5fb09.png)
### Edit contact by id:
![changecontactdetailed](https://user-images.githubusercontent.com/92301625/170489732-7595ebdd-59cf-4478-82d2-3f253289645b.png)
### Transfer a message:
![asktosendmessage](https://user-images.githubusercontent.com/92301625/170490158-5f2bff4a-ed7f-45a4-9380-f57c3e611dd0.png)
### Post a message:
![postmessage](https://user-images.githubusercontent.com/92301625/170490801-0f6dcfc7-194b-4329-b9e5-04aee676f29c.png)
### Get messages with contact:
![getmessageswithcontact](https://user-images.githubusercontent.com/92301625/170490692-c13769b4-7520-4028-ba8c-fffc2435a0bd.png)

***

# 3. Dependencies

This Web-Api server uses:
* NuGet packeges:
  * Microsoft.AspNetCore.SignalR
  * Microsoft.EntityFrameworkCore.Tools
  * Pomelo.EntityFrameworkCore.MySql
* MariaDB Entity Framework

***

# 4. Setup and User Guide
## 4.1 Setup

* Download mariaDB : `https://mariadb.org/` and when asking for password give the password `toor`.
* Open HeidiSQL : in Session manager enter on `New`<br/>
and fill the fileds `User` with `root` and `Password` with `toor` and press `Open`.
* Clone the repo: 
  ```bash
  git clone https://github.com/noaziv55/web_development.git
  ```
* Delete the Migration folder.
* Change `connectionString` field to:
  ```bash
   "server=localhost;port=3306;database=ChatAppDB;user=root;password=toor"
   ```
* In Case you want to change the domain go to `Properties\launchSettings.json` file and change the WebApi `applicationUrl`.
* Install the requied dependencies by entering to the Package Manager Console: 
   ```bash
   Install-Package Pomelo.EntityFrameworkCore.MySql -Version 6.0.1
   ```
   ```bash
   Install-Package Microsoft.EntityFrameworkcore.Tools -Version 6.0.1
   ```
* Create a migartion by typing `Add-Migartion Init`
* Apply the migartion by typing `Update-Database`
* Run the server by press run project button in vs.
* Open `http://domain/` with a browser

## 4.2 User Guide

### How to create a new user:
* With Swagger go to User POST operation and enter the required fields.<br/>
or
* With using Database Go to `HeidiSQL` to the users database press on `data` and create a new row.

***

# 5. Contributors

* [Noam Gini](https://github.com/NoamGini)
* [Noa Ziv](https://github.com/noaziv55)

***
