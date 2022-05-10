# Demo 1

Adding the Prometheus client library to an ASP.NET Core application.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

## 1.1 - Run the demo app

Use Docker Compose to start all the app components:

```
docker-compose up -d
```

Browse to http://localhost:8080/ and refresh.

Check the logs from the web app:

```
docker-compose logs web
```

## 1.2 - Add the Promtheus NuGet package

Browse to https://www.nuget.org/ and search for `prometheus`. 

Add the ASP.NET Core package to the project file [WiredBrain.Web.csproj](./web/src/WiredBrain.Web/WiredBrain.Web.csproj):

```
<PackageReference Include="prometheus-net.AspNetCore" Version="3.6.0"/>
```

## 1.3 - Wire up the middleware

Reference the library in [Startup.cs](./web/src/WiredBrain.Web/Startup.cs):

```
using Prometheus;
```

And add the metrics server to the _Configure_ method:

```
app.UseMetricServer();
app.UseHttpMetrics();
```

> This needs to come after the `UseRouting` call, so the middleware can discover configured routes

## 1.4 - Rebuild and run

Build the updated web app:

```
docker-compose build web
```

And run the new version:

```
docker-compose up -d web
```

Browse to http://localhost:8080/ and refresh.

> Now check the metrics at http://localhost:8080/metrics
