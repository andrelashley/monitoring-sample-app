# Demo 2

Explore the metrics from the .NET client library.

## 2.1 - Run Prometheus

Uses a custom Prometheus image:

- packaged with the [prometheus.yml](./prometheus/prometheus.yml) config file
- defined in the [docker-compose-prometheus.yaml](./docker-compose-prometheus.yaml) Compose override file

Use Docker Compose to run Prometheus:

```
docker-compose -f docker-compose.yaml -f docker-compose-prometheus.yaml up -d
```

Browse to http://localhost:9090/targets and confirm the web app is being scraped.

## 2.2 - Add some Prometheus queries

Browse to http://localhost:9090/graph.

Add a query for HTTP requests in progress:

```
http_requests_in_progress
```

And for memory usage:

```
dotnet_total_memory_bytes
```

And for 90th percentile HTTP request duration:

```
histogram_quantile(0.90, sum without(code, method) (rate(http_request_duration_seconds_bucket{code="200"}[5m])))
```

## 2.3 - Run some load and check the metrics

Use [Fortio](https://github.com/fortio/fortio/) to send multiple concurrent GET requests:

- load test defined in the [docker-compose-fortio.yaml](./docker-compose-fortio.yaml) Compose override file

```
docker-compose -f docker-compose.yaml -f docker-compose-fortio.yaml up fortio
```

> Check the Prometheus graphs and compare the Fortio performance stats to the histogram