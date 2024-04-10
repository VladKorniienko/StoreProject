# Store
## To-do
- [ ] Add Forgot Password
- [ ] Add role managing
## Features
### - 3-layered Web API in .NET 8
### - ASP.NET Core Identity
### - JWT authentication (with refresh tokens)

I decided to implement the logout functionality by removing the refresh token from the DB. This way, I avoid the need to create a "blacklist" for tokens and additional DB calls. Token's lifetime is short (appr. 5 min), so when the client wants to refresh its token via already deleted refresh token, the API doesn't allow that.
### - Custom middleware for handling exceptions
### - Repository and UnitOfWork patterns
