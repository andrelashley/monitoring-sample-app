# Demo 2

Instrumenting Node.js apps with Prometheus.

## Pre-reqs 

[Docker](https://www.docker.com/products/docker-desktop) (use Linux container mode if you're using Docker Desktop on Windows).

Run the WiredBrain app:

```
docker-compose up -d
```

## 2.1 - Wire up the gateway

Add the dependency in [package.json](batch/src/package.json):

```
"prom-client" : "12.0.0"
```

Add the push code in [server.js](batch/src/server.js):

```
// imports
const prometheus = require('prom-client');

// declaration
var gateway = new prometheus.Pushgateway(process.env.PUSHGATEWAY_URL);

// exit
  gateway.pushAdd({ jobName: 'batch'}, function(err, resp, body) {
    if (err) {
      console.log(err)
    } 
    else {
      console.log('Added metrics to pushgateway') 
    }    
    process.exit();
  })
```

## 2.2 - Record the info metric

Set the info metric in [server.js](batch/src/server.js):

```
var appInfoGauge = new prometheus.Gauge({
  name: 'app_info', 
  help: 'Application info',
  labelNames: ['version', 'node_version'],
});
appInfoGauge.labels('0.3.0', '10.19.0').set(1);
```

Rebuild the batch app:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 build batch
```

## 2.3 - Test the batch process

Try the app at http://localhost:8080/

Check `app_info` metric at http://localhost:9090/graph

Run the batch process:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 up batch
```

Check metrics at http://localhost:9090/graph

* `app_info` contains batch app
* no runtime metrics from batch
