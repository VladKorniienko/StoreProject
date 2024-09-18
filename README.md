# Store
## The app's demo has been deployed on Azure: [link to demo](https://storeappfront-hhd7akevdeejdfdd.polandcentral-01.azurewebsites.net/) and [link to the API](https://storeapp.azurewebsites.net/api/genres)
## Description
This application shares many features with Google Play Market, AppStore from Apple, Steam, and other app marketplaces.
Users can create an account, log in, view and change their balance, buy products, and view their library.
Admins can additionally manage all users and products.
The front-end part is located [here](https://github.com/VladKorniienko/StoreClientAngular).
## Features
### - Layered Architecture - Web API .NET 8
- Presentation Layer
- Business Logic Layer
- Data Access Layer
- Common (Constants + Exceptions)
### - ASP.NET Core Identity
### - JWT authentication (with refresh tokens)

I implemented the logout functionality by removing the refresh token from the DB. This way, I avoid the need to create a "blacklist" for tokens and additional DB calls. The token's lifetime is short (appr. 5 min), so when the client wants to refresh its token via an already deleted refresh token, the API doesn't allow that.

After some research, I decided to utilize HttpOnly cookies, although at first, my auth endpoint just returned a JSON with an access token and refresh token. After all the changes, I put my access token and refresh token into two different HttpOnly cookies, which adds a layer of protection to the data. Having tokens stored on the client side in local storage makes them vulnerable to cross-site scripting (XSS) attacks since JavaScript has access to local storage. On logout, I also delete respective cookies with tokens.

In my implementation, the client sends a login request with the user's data and receives two HTTPOnly cookies with corresponding tokens. If the client wants to access protected API endpoints, it sends these cookies alongside the request, and the server takes the access token from the cookie and puts it into the context, thus giving the user access to the endpoint.

### - Custom middleware for handling exceptions
### - Services

Services use AutoMapper to convert between models and DTOs. I also created different AutoMapper profiles for each entity.

Custom validators are leveraged in Services to make sure only valid data is being transferred.
### - Repository and UnitOfWork patterns
### - Caching

I implemented caching for products due to the frequent calls to this data. I used `IMemoryCache` through dependency injection in my ProductService. 

Before fetching products from the database, a cache key specific to the requested page and page size is constructed. First, it is checked if the data corresponding to the cache key exists in the cache. If the data is not found, it fetches the products from the database, maps them using AutoMapper, and stores the result in the cache with a sliding expiration of 5 minutes. If the data is found in the cache, it directly returns the cached data.

Methods for adding, deleting, and altering Products include a `ClearProductCache` call after modifying the database to ensure that any cached data related to products is invalidated and cleared from the cache. This maintains data consistency between the database and the cache.

### - Configuration

Service configurations and dependency injection setup are provided using extension methods for each layer.

Entities and their relationships are managed by separate configuration classes via Fluent API. These classes implement the `IEntityTypeConfiguration<TEntity>` interface provided by EF Core.
