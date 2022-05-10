# Demo 3

Recording batch process metrics.

## 3.1 - Add gauges to record status times

Add new gauges in [server.js](batch/src/server.js):

``` 
// error block
var errorGauge = new prometheus.Gauge({
  name: 'batch_last_error_seconds', 
  help: 'Latest batch job error time'
});
errorGauge.setToCurrentTime();

// success block
var successGauge = new prometheus.Gauge({
  name: 'batch_last_success_seconds', 
  help: 'Latest batch job success time'
});
successGauge.setToCurrentTime();
```

Rebuild:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 build batch
```

## 3.2 - Test success and failure

Stop the database container:

```
docker-compose stop products-db
```

Run the batch process - this will fail:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 up batch
```

Start the database container:

```
docker-compose start products-db
```

Run the batch process - this succeeds:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 up batch
```

Refresh the [WiredBrain app](http://localhost:8080/) - prices have increased.

> Check `batch_last_error_seconds` and `batch_last_success_seconds` metrics

## 3.3 - Add duration gauge

In [server.js](batch/src/server.js):

```
var durationGauge = new prometheus.Gauge({
  name: 'batch_duration_seconds', 
  help: 'Batch job duration'
});
const end = durationGauge.startTimer();

// after query
end();
```

Rebuild & run:

```
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 build batch
 
docker-compose `
 -f docker-compose.yaml `
 -f docker-compose-batch.yaml `
 up batch
```

> Check `batch_last_success_seconds` and `batch_duration_seconds` metrics