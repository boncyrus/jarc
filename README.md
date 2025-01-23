# Overview

**Just Another Result Class (JARC)** is a lightweight library for .NET that follows the Result pattern.

## Why Result pattern

Result pattern allow developers to explicitly identify success and failure scenarios so it can be easily handled gracefully.

## Why use this library

The motivation for this library is to share something that worked for me as I've used it successfully in some projects. I also like the simplicity of it.

Of course there are other libraries out there that pretty much does the same thing such as [Language.Ext](https://github.com/louthy/language-ext), [FluentResults](https://github.com/altmann/FluentResults), etc. so feel free to choose whatever suits your needs.

## Creating a Result

A `Result` or `Result<T>` allows you to return an object that contains the value and error.

The non-generic version is typically used when you don't have a value to return. On the other hand, `Result<T>` is used when you want a value as part of the result.

```csharp
// Using non-generic

// You can return Result.Empty; for a success scenario
Result resultSuccess = Result.Empty;
Result resultError = new Exception();

// Using generic
Result<int> resultSuccessGeneric = 5;
Result<int> resultErrorGeneric = new Exception();
```

You'll notice that in the generic example, I was able to assign a value directly without explicitly creating the `Result<T>`. This is becase it has `implicit operators` under the hood so it makes your code a lot cleaner.

## Processing a Result

The `Result` class offers a few properties and method that you can use to react upon.

```csharp
Result<int> result1 = Calculate();

if (!result1.IsSuccess) 
{
    // Handle failed scenario.
    // Read result1.Error;
}
```

Using the `Match` method.
```csharp
Result<int> result1 = Calculate();

result1.Match(value => 
{
    // Handle success
    return Ok();
},
err => 
{
    // Handdle error.
    return BadRequest();
})
```

You can also throw the exception and allow it to be handled somewhere else e.g. using `IExceptionHandler` for ASP.NET Core.
```csharp
Result<int> Calculate()
{
    // Doing something here...

    return new SomeDomainException("An error occured");
}

Result<int> result1 = Calculate();
result1.EnsureSuccess();
```
`EnsureSuccess()` throws the `Error` in the `Result` object if it is in a failed state.

## License

This project is licensed under the [MIT license](LICENSE.md).