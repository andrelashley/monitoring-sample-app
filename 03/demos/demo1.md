# Demo 1

Adding applications metrics to a Go REST API.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

Start the latest version of the app running:

```
docker-compose up -d
```

> Check the stock API at http://localhost:8082/stock/1

## 1.1 - Add the Promtheus module & handler

Reference the client library as a Go module in [go.mod](stock-api/src/go.mod):

```
github.com/prometheus/client_golang v1.7.1
```

Then expose the metrics endpoint with a new handler in [router.go](stock-api/src/router/router.go):

```
// imports
"github.com/prometheus/client_golang/prometheus/promhttp"

// func
router.Path("/metrics").Handler(promhttp.Handler())
```

Rebuild and deploy the API:

```
docker-compose build stock-api

docker-compose up -d stock-api
```

> Browse to the metrics endpoint http://localhost:8082/metrics

## 1.2 - Add the app info metric

Create a gauage and set the value in [main.go](stock-api/src/main.go):

```
// imports
"github.com/prometheus/client_golang/prometheus"
"github.com/prometheus/client_golang/prometheus/promauto"

// declaration
var (
	appInfo = promauto.NewGaugeVec(prometheus.GaugeOpts{
	  Name: "app_info",
	  Help: "Application info",
	}, []string{"version", "goversion"})
)

// func
appInfo.WithLabelValues("0.2.0", "1.14.4").Set(1)
```

(A library function exists for this - see [Go app info example](https://github.com/prometheus/common/blob/8558a5b7db3c84fa38b4766966059a7bd5bfa2ee/version/info.go)).

Rebuild and deploy:

```
docker-compose build stock-api

docker-compose up -d stock-api
```

> Check the `app_info` metric is exposed http://localhost:8082/metrics
