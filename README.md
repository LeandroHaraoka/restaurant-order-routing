# restaurant-order-routing

Restaurant Order Routing project aims to solve the following problem: you have 
a restaurant with multiple POS (point-of-sale) instances sending orders that
should be routed to specific areas of a kitchen.

## Requirements

- It's not necessary to implement any database technology.
- Solution most be tested (unit and integration tests).
- Concurrency issues must be avoided.
- Project must be organized considering concerns separation.

## Operation System Requirements
- .Net Core Runtime 3.1
- AspnetCore Runtime 3.1

## How to run

### Build

```bash
$ dotnet build
```

### Run

```bash
$ dotnet run --project <cs proj path>
```

### Test

To run tests commands, you need **dotnet core**, **xunit** and the **coverlet library**.

1. Dependencies

```bash
$ dotnet add <TEST_PROJECT_PATH> package Microsoft.NET.Test.Sdk
```

```bash
$  dotnet tool install --global coverlet.console
```

2. Checking coverage with coverlet

When executing coverlet coverage command, a `coverage.json` file will be generated
in the project folder. It's important to add it to `gitignore` file.

```bash
$  dotnet test <TEST_PROJECT_PATH> /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=[xunit.*]*
```


