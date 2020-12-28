# CRUD Web-API with CSharp [asp net core] 💖

## About 
> I created my first crud in the Asp Net Core with [MVC].

## Server

```HOST: https://localhost/```

## Routes

### [ Tech ] - Router
```r

Tech = { 
  id: long, 
  name: string
} 

### Users Router
[GET] - 'api/techs' # find all techs
[POST] - 'api/techs' { name } # create tech
```


### [ User ] - Router
```r

User = { 
  id: long, 
  name: string, 
  age: int,
  techs: List<TechItem>
} 

### Users Router
[GET] - 'api/users' # find all users
[POST] - 'api/users' { name, age } # create user

[GET] - 'api/users/:id' # find one user by id
[PUT] - 'api/users/:id' { name: 'optional', age: 'optional' } #edit user data
[DELETE] - 'api/users/:id' # delete user by id

[POST] - 'api/users/:user_id/techs/:tech_id' # add a technology to the user
[DELETE] - 'api/users/:user_id/techs/:tech_id' # remove a user technology
```
