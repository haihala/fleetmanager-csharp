# Fleetmanager

## Installation and launching
Identical to the original software.

## Api calls

### HTTP GET

The API accepts two different GET calls. One to fetch a possibly filtered list of all cars and one to get the data of a specific car.

`/api/car/`

Returns all cars in the system. Accepts `minYear`, `maxYear`, `model` and `manufacturer` parameters.

Example:

`/api/car/?minYear=1999&maxYear=2010&model=yaris&manufacturer&toyota`

Returns all the Toyota Yaris cars made between 1999 and 2010.

Both year arguments are exclusive, meaning that when searching with minYear=n, cars made in exactly n are not shown.


`/api/car/{id}`

Returns all data the system has on a car with the given id.

Example:

`/api/car/8e22ef2a-16b5-4c63-81fd-16795725d766`

Returns a car made in 1999 from manufacturer 'make10' of model 'model7' with registration 'NJY-102' that was last inspected on the 23rd of march 2018, has an 2981 sized engine that produces 382 power. (seed data)

If no car with given id can be found, returns NotFound()

### HTTP POST

`/api/car/{id}`

Takes the following form-data:

* ModelYear
* Model
* Manufacturer
* Registration
* InspectionDate
* EngineSize
* EnginePower

All of the fields mentioned above can be missing.

Either modifies or adds a car with given id depending on whether or not such car already exists.

Returns the new or modified car similarly to how HTTP GET on `/api/car/{id}` returns cars.

### HTTP DELETE

`/api/car/{id}`

Attempts to delete the car with the given id. Returns the deleted car similarly to how POST and GET return cars.