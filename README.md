# CRUD Web-API with CSharp [asp net core] 💖

## About 
> I created my first crud in the Asp Net Core with [MVC].

## Server

```HOST: https://localhost/```

### Data Type
```C# 
User = { 
  id: long, 
  name: string, 
  age: int 
} 
```
### Routes
```r
[GET] - 'api/users' # find all users
[POST] - 'api/users' { name, age } # create user

[GET] - 'api/users/:id' # find one user by id
[PUT] - 'api/users/:id' { name: 'optional', age: 'optional' } 
[DELETE] - 'api/users/:id' # delete user by id
```
