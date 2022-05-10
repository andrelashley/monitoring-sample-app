# Demo 1

Scraping application metrics with Prometheus.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

Switch to Swarm mode and create the network for the app:

```
docker swarm init

docker network create -d overlay wiredbrain
```

## 1.1 - Run the app

Create the config object for the production [prometheus-swarm.yml](./prometheus/configs/prometheus-swarm.yml):

```
docker config create prometheus-config ./prometheus/configs/prometheus-swarm.yml
```

This config uses service discovery to find the application containers:

* using the Swarm service name for the job 
* and the numeric replica slot for the instance 

Deploy the app from [docker-stack.yaml](./docker-stack.yaml):

```
docker stack deploy -c docker-stack.yaml wb-web

docker service ls
```

The service specs use labels to configure the Prometheus setup:

* opting in to be scraped
* specifying the port to use
* and the metrics path

Test the app http://localhost:8080/

Check Promtheus:

* service discovery http://localhost:9090/service-discovery
* `app_info` metric http://localhost:9090/graph 

## 1.2 Run the batch process

Run the pricing batch process from [docker-stack-batch.yaml](./docker-stack-batch.yaml)

```
docker stack deploy -c docker-stack-batch.yaml wb-batch
```

> There are no Prometheus labels in here, the metrics get pushed to the Gateway

Check price increase http://localhost:8080/

And repeat:

```
docker service update --force wb-batch_batch
```

Check metrics:

* `app_info`
* `batch_last_success_seconds`
* `http_request_duration_seconds_bucket`
* `http_server_requests_seconds_sum`