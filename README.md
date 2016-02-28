# DealsNet

A RESTful API for accessing shopping "deals"
This README acts as the API documentation for the API.

## API Endpoints and Example Usage

* All responses are send in JSON, all requests should have Content-Type: application/json in their headers or they will be rejected.
* Unless otherwise specified all requests must include an "auth" parameter for authentication.

Endpoint Name (/Name) | Description
----------------------|------------
Create User (CreateUser) | Creates a new user with the given user_name. Does not require authentication
Create Deal (CreateDeal) | Creates a new deal with parameters
Find Deal (FindDeal) | Used to find a deal by product name, store name, zip code, or any combination of the three (case insensitive)
Like/Dislike (ExpressFeelings) | Allows the user to like or dislike a deal, or remove their like or dislike from a deal.
Get All Deals (GetAllDeals) | Non-authenticated method that allows you to get all deals. Mostly for debugging

### Create User

#### Example Request
```JSON
{
	"user_name":"john"
}
```
#### Example Response 
```json
{
	"user_name": "josh",
	"id": "56d330e7f002e711eea64c7b"
}
```
__Note:__ The ID will be used for the auth parameter for any other requests you would like to make.

### Create Deal
#### Example Request
```JSON
{
  "auth":"56ca052bf002e73f906ce880",
  "product_name":"Mo Money",
  "price":"50.0",
  "store_name":"Mo Problems",
  "zip_code":"46818",
  "expiry_date":"2016-02-14"
}
```
#### Example Response
```JSON
{
"auth": "56ca052bf002e73f906ce880",
"product_name": "Mo Money",
"price": "50.0",
"store_name": "Mo Problems",
"zip_code": "46818",
"expiry_date": "2016-02-14"
}
```
__Note:__ If successful, the API will simply return the request that you sent it.

### Find Deal
#### Example Request
```JSON
{
  "auth":"56d30919f002e77ef9c45e80",
  "store_name":"Problems"
}
```
__NOTE__ You can search for any combination of "store_name", "product_name", or "zip_code" the searches stack and are case insensitive
#### Example Response
```JSON
	[{"likers":[],"dislikers":[],"_id":"56d30d8df002e70285ca0249","submitter_name":"jane","product_name":"Mo Money","price":50.0,"store_name":"Mo Problems","zip_code":46818,"expiry_date":"2016-03-29T04:00:00Z"}]
```
The response will be a list of JSON objects representing the deals, the people that liked them and disliked them.

### Like/Dislike
#### Example Request
```JSON
{
  "auth":"56d30919f002e77ef9c45e80",
  "feelings":1,
  "deal_id":"56d306bcf002e703072773d9"
}
```
Feelings can be one of the following options, otherwise they are ignored and an error is returned.
Feeling Setting | Meaning
----------------|--------
1 | Like
0 | Remove feelings from deal
-1 | Dislike deal

#### Example Response
This does not give a response, any error is communicated through the response code.


### Get All Deals
Response is the same as the Find Deals, except will run without authentication, return all deals in the database and doesn't require a request parameter





