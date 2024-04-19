# Store
## Features
### - 3-layered Web API in .NET 8
### - ASP.NET Core Identity
### - JWT authentication (with refresh tokens)

I decided to implement the logout functionality by removing the refresh token from the DB. This way, I avoid the need to create a "blacklist" for tokens and additional DB calls. Token's lifetime is short (appr. 5 min), so when the client wants to refresh its token via already deleted refresh token, the API doesn't allow that.

After some research, I decided to utilize HttpOnly cookies, although at first my auth endpoint just return a JSON with an access token and refresh token. Now I put my access token and refresh token into two different HttpOnly cookies, which adds a layer of protection to the data. Having tokens stored on the client side in local storage makes them vulnerable to cross-site scripting (XSS) attacks, since JavaScript has access to local storage.

In my implementation, client sends a login request with user's data and receives two HttpOnly cookies with corresponding tokens. If client wants to access protected API endpoints, it sends these cookies alongside the request and the server takes the access token from the cookie and puts it into the context, thus giving the user access to endpoint.

### - Custom middleware for handling exceptions
### - Repository and UnitOfWork patterns
