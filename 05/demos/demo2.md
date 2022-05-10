# Demo 2

Visualizing app metrics with Grafana.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

Get the demo app running from the steps in [Demo 1](./demo1.md)

## 2.1 - Set up Grafana

Grafana is running at http://localhost:3000 - sign in with `admin`/`admin`.

- Add a new Prometheus data source - address http://prometheus:9090
- Import the application dashboard from [dashboard.json](./dashboard.json)

> Explore the dashboard and the PromQL for each component

## 2.2 - Run load tests

Run ongoing tests against web and APIs in [docker-stack-fortio.yaml](./docker-stack-fortio.yaml):

```
docker stack deploy -c docker-stack-fortio.yaml wb-load
```

Check the dashboard during the load.
