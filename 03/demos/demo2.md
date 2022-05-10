# Demo 2

Adding Go HTTP metric collection in middleware.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

Start the latest version of the app running:

```
docker-compose up -d
```

> Check the stock API at http://localhost:8082/stock/1


## 2.1 - Wire up middleware with active users gauge

Add the new middleware file and walk through the code in [prometheus.go](stock-api/src/middleware/prometheus.go).

Wire up the middleware in [router.go](stock-api/src/router/router.go)

```
// imports
"stock-api/middleware"

// func
router.Use(middleware.Prometheus)
```

Rebuild and deploy:

```
docker-compose build stock-api

docker-compose up -d stock-api
```

> Test the API at http://localhost:8082/stock/1; check the metrics at http://localhost:8082/metrics

## 2.2 - Add HTTP duration to middleware

Extend the middleware function in [prometheus.go](stock-api/src/middleware/prometheus.go):

```
// imports
"github.com/gorilla/mux"

// declaration
var (
  httpDuration = promauto.NewHistogramVec(prometheus.HistogramOpts{
	Name: "http_request_duration_seconds",
	Help: "Duration of HTTP requests",
  }, []string{"path"})
)

// func - before ServeHTTP
route := mux.CurrentRoute(r)
path, _ := route.GetPathTemplate()
timer := prometheus.NewTimer(httpDuration.WithLabelValues(path))

// func - after ServeHTTP
timer.ObserveDuration()
```

Rebuild and deploy:

```
docker-compose build stock-api

docker-compose up -d stock-api
```

> Test the API at http://localhost:8082/stock/1; check the metrics at http://localhost:8082/metrics
