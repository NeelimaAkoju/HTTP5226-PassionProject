# Trip Application CMS

## Project Overview
The Trip Application CMS is designed to manage and showcase information about trips and places. It helps administrators post information about various trips and places, manage user engagement, and guide enthusiasts who want to explore more about different destinations and travel experiences.

## Key Features

### Trip Management
- Add, update, delete, and display trips.

### Place Management
- Add, update, delete, and display places.
- Associate places with specific trips.

### User Management
- Add, update, and display user information.
- Display trips associated with the user.

### Trip Management Page
- **List view**: Display all trips with options to add, edit, or delete.
- **Form**: Add or edit trip details, including name, description, start date, end date, and associated places.

### Place Management Page
- **List view**: Display all places with options to add, edit, or delete.
- **Form**: Add or edit place details, including name, description, city, and associated trips.

### User Management Page
- **List view**: Display all users with options to add, edit, or delete.
- **Form**: Add or edit user details, including username, email.

## Entities and Attributes

### Users
- **UserID** (Primary Key)
- **Username**
- **Email**

### Trips
- **TripID** (Primary Key)
- **TripName**
- **TripDescription**
- **StartDate**
- **EndDate**
- **CustomerId** (Foreign Key, references Users)
- **CustomerName**

### Places
- **PlaceId** (Primary Key)
- **PlaceName**
- **PlaceDescription**
- **PlaceCity**

## Relationships
- A Trip can have many Places.
- A Place can belong to many Trips.
- A User can be associated with multiple Trips.

## Storyboard

### Scenario: User Updates Trip Details

**User Journey**:
1. Navigate to the "Trip List" page.
3. They select a trip from the list to update its details (e.g., change the description or update the date).
4. They save the changes, and the trip details are updated in the system.

## API Endpoints Overview

### Trip Data Controller
- **GET /api/TripData/ListTrips**: List all trips.
- **GET /api/TripData/FindTrip/{id}**: Find a specific trip by ID.
- **POST /api/TripData/AddTrip**: Add a new trip.
- **POST /api/TripData/DeleteTrip/{id}**: Delete a specific trip by ID.
- **POST /api/TripData/UpdateTrip/{id}**: Update a specific trip by ID.
- **POST /api/TripData/AddPlaceToTrip/{tripId}/{placeId}**: Add a place to a trip.
- **POST /api/TripData/RemovePlaceFromTrip/{tripId}/{placeId}**: Remove a place from a trip.
- **GET /api/TripData/TripsForCustomer/{customerId}**: List trips for a specific customer.

### Place Data Controller
- **GET /api/PlaceData/ListCities**: List all places.
- **GET /api/PlaceData/FindPlaces/{id}**: Find a specific place by ID.
- **POST /api/PlaceData/AddPlace**: Add a new place.
- **POST /api/PlaceData/DeletePlace/{id}**: Delete a specific place by ID.
- **POST /api/PlaceData/UpdatePlace/{id}**: Update a specific place by ID.
- **GET /api/PlaceData/ListPlacesForTrip/{id}**: List places for a specific trip.

### Customer Data Controller
- **GET /api/CustomerData/ListCustomers**: List all customers.
- **GET /api/CustomerData/FindCustomer/{id}**: Find a specific customer by ID.
- **POST /api/CustomerData/AddCustomer**: Add a new customer.
- **POST /api/CustomerData/UpdateCustomer/{id}**: Update a specific customer by ID.
