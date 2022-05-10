# Demo 3

Add information metrics to the app.

## 3.1 - Add the code

We'll add a custom metric to record key information about the app.

Reference the library in [Program.cs](./web/src/WiredBrain.Web/Program.cs):

```
using Prometheus;
```

Create a new static variable in the `Program` class:

```
private static readonly Gauge _InfoGauge = Metrics.CreateGauge("web_info", "Web app info", "dotnet_version", "assembly_name", "app_version");
```

- `web_info` is the metric name
- `Web app info` is the help text
- `dotnet_version`, `assembly_name` & `app_version` are labels

And set the value in the `Main` method:

```
_InfoGauge.Labels("3.1.7", "WiredBrain.Web", "0.1.0").Set(1);
```

> You should derive this at runtime instead of hardcoding it

## 3.2 - Rebuild and run

Build the updated web app:

```
docker-compose build web
```

And run the new version:

```
docker-compose up -d web
```

Browse to http://localhost:8080/ and check it still works.

> Now check the `web_info` metric at http://localhost:9090/graph

You can query the number of instances by the app version:

```
count by(app_version)(web_info)
```

And add the app version as a label to the memory usage metric:

```
dotnet_total_memory_bytes * on (instance) group_left(app_version) web_info
```