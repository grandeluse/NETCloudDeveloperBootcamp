<h2>The lifetime service for a DbContext</h2>

What service lifetime to use for a dbContext?
- DbContext is designed to used as single Unit of Work
- DbContext created --> entity changes are tracked -> save changes -> dispose
- DB connections are expensive
- DbContext is not thread safe
- Increased memory usage due to change tracking

USE: Scoped service lifetime
- Aligning the context lifetime to the lifetime of the request
- There is only one thread executing each client request at a given time
- Ensure that each request gets a separate DbContext instance
