# Customer Service

## Introduction

This service is part of the "Wasale" Food Delivery Service, an ASP.NET application that works with PostgreSQL. It handles customer account and details management.

The purpose of this service is to facilitate the ordering of food from different restaurants. The system supports adding/removing different restaurants to the restaurant catalog. This document will guide the development team, ensuring that all necessary features and functionalities are implemented.
m.

## Getting Started

### Docker Compose

To run the project locally using Docker Compose, follow these steps:

1. Download docker-compose and postman collections [download](https://drive.google.com/drive/folders/1a-NlPiDkp8zquijd9lQY_L0PNoV_7Jsv?usp=sharing)
2. At The same docker-compose path run the following command:

```bash
docker-compose up
```
## Using Postman Collection

### Importing the Collection

1. **Download the Collection**: Insure that you download collection in previous link [download](https://drive.google.com/drive/folders/1a-NlPiDkp8zquijd9lQY_L0PNoV_7Jsv?usp=sharing).

2. **Import into Postman**: Open Postman, click on the "Import" button in the top left corner, and choose CusomerService.postman_collection.json. This will import the collection into your Postman workspace, reapte this for DeliveryService.postman_collection.json .

### Running the Collection

1. **Open Collection**: In Postman, navigate to the Collections tab on the left sidebar. Find and click on the imported collection to open it.

2. **Run Collection**: Click on the "Run" button located on the top right corner of the collection window. This will trigger the execution of all requests within the collection.

3. **View Results**: Postman will execute each request in the collection one by one and display the results in the Runner window. You can review the responses, check for errors, and inspect the test results.

3. **Run Individual Requests**: To run individual requests, navigate to the request you want to execute within the collection. Click on the request name to open it, then click on the "Send" button to send the request individually.

4. **View Examples**: Many requests within the collection may include examples. To view these examples, open the request and navigate to the "Examples" tab. Here you can see sample request and response payloads, headers, and other relevant information.
