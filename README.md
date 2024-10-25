# DaData API

DaData API представляет собой веб-сервис для получения информации об адресах через одноименный сервис DaData

## Использование

Перед тем, как испольовать сервис, неободимо ввести токен и секрет в файле appsettings.json

Для получения информации об интересующем адресе сервис предоставляет конечную точку /adress/info

*  **URL**

`/adress/info/:adress`


*  **Method**

`GET`


*  **URL Params**
 
`adress=[string]`

* **Response**

```
{
  "postalCode": "600015",
  "country": "Россия",
  "city": "Владимир",
  "street": "Ленина",
  "houseNumber": "22",
  "floorNumber": null,
  "flatNumber": "11",
  "flatArea": "67.6"
}
```

* **Error Response**


**Code:** 500 Internal Server Error

```
"type": "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
"title": "Internal server error",
status": 500
```
