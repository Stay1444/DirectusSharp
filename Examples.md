# Examples

### Creating a client

There are two ways of creating a client. Both need an HttpClient with a preset `BaseUri`.

#### Unauthenticated
```csharp
var httpClient = new HttpClient() {
    BaseUri = new Uri("https://directus.io")
};

var client = DirectusClient.Create(httpClient);
```

#### Authenticated

There are multiple ways to do authentication in Directus. You can read more about it [in the official documentation](https://docs.directus.io/reference/authentication.html).

##### Static Token

```csharp
DirectusClient.Create(new StaticIdentity() {
    Token = "STATIC_TOKEN"
}, ...);
```

##### TemporaryIdentity

Temporary tokens must be refreshed manually (DirectusSharp will **not** automatically refresh tokens for you)

```csharp
DirectusClient.Create(new TemporaryIdentity() {
    AccessToken = ...,
    RefreshToken = ...
}, ...);
```

You can obtain a TemporaryIdentity through the `LoginRequest`.

```csharp
var response = client.ExecuteAsync(new LoginRequest() {
    Email = ...,
    Password = ...
});

if (!response.IsSuccess) { /* Handle Login Failure */ }

client.Identity = response.Data;
```

### Collection examples

Check out the [generator examples](./Examples/DirectusSharp.Examples.Collections/README.md).

To Query, Create or Modify our collection, we need to create the following classes associated to it. In this example I'll use a collection called `Pets`.

### Boilerplate
```csharp
class Pet {
    public int Id { get; set; } = 0; // Directus will auto-increment it on insert
    public string Name { get; set; }
    public int Age { get; set; }
}

class GetPetRequest : GetItemRequest<Pet> {
    public required int Id { get; init; }
    
    protected override string GetCollection() => "pets";
    protected override string GetItemId() => Id.ToString();
}

class CreatePetRequest : CreateItemRequest<Pet> {
    public required Pet Pet { get; init; }
    
    protected override string GetCollection() => "pets";
    protected override Pet GetItem() => Pet;
}

class UpdatePetRequest : UpdateItemRequest<Pet> {
    public required Pet Pet { get; init; }
    
    protected override string GetCollection() => "pets";
    protected override Pet GetItem() => Pet;
    protected override string GetItemId() => Pet.Id.ToString();
}
```

#### Querying an Item
```csharp
var response = await client.ExecuteAsync(new GetPetRequest() {
    Id = 0
});

if (response.IsSuccess) {
    Console.WriteLine($"Pet name: {response.Data.Name}");
}
// 
```

#### Creating an Item
```csharp
var response = await client.ExecuteAsync(new CreatePetRequest() {
    Pet = new Pet() {
        Name = "Rudolf",
        Age = 3   
    }
});

if (response.IsSuccess) {
    Console.WriteLine($"Created pet with id: {response.Data.Id}");
}
```

#### Updating an Item
It's practically the same thing as `CreatePetRequest` but with `UpdatePetRequest`
```csharp
var response = await client.ExecuteAsync(new UpdatePetRequest() {
    Pet = ...
};
```
