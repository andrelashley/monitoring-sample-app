# Demo 1

Running and using the Prometheus Pushgateway.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

## 1.1 - Run the Pushgateway

Start the gateway container using the [docker-compose.yaml](docker-compose.yaml) specification:

```
docker-compose up -d pushgateway
```

Browse to:

* Pushgateway UI http://localhost:9091
* Pushgateway metrics http://localhost:9091/metrics

> Standard Go runtime metrics

## 1.2 Push some sample metrics

Post the data in [sample-metrics.txt](sample-metrics.txt) to the gateway:

```
curl -X POST 'http://localhost:9091/metrics/job/batch-test/instance/test01' `
--header 'Content-Type: text/plain' `
--data-binary '@sample-metrics.txt'
```

Check the metrics from the gateway:

* Pushgateway UI http://localhost:9091
* Pushgateway metrics http://localhost:9091/metrics

## 1.3 Scrape Pushgateway with Prometheus

Prometheus scrapes from the Pushgateway and you need to configure it correctly to preserve the job labels from the source.

See the problem by running Prometheus with incorrect config in [prometheus.yml](prometheus/push/prometheus.yml):

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-push.yaml `
 up -d prometheus
```

Check the metrics coming into Prometheus:

* Target list http://localhost:9090/targets
* Check `test_gauge` metric in Graph page 

Replace Prometheus with the correct config in [prometheus.yml](prometheus/prometheus.yml):

```
docker-compose up -d prometheus
```

Check [targets](http://localhost:9090/targets); check metrics:

* `up`
* `test_counter`

Send updated metrics in [sample-metrics-2.txt](sample-metrics.txt) to the gateway:

```
curl -X POST 'http://localhost:9091/metrics/job/batch-test/instance/test01' `
--header 'Content-Type: text/plain' `
--data-binary '@sample-metrics-2.txt'
```

Check [metrics](http://localhost:9090/graph) & graph:

* `test_counter`
* `test_gauge`