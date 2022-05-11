const { Client } = require('pg')
const client = new Client()
const prometheus = require('prom-client');

const sql = 'UPDATE "public"."products" SET price = price * $1'
const factor = [process.env.PRICE_FACTOR]
var gateway = new prometheus.Pushgateway(process.env.PUSHGATEWAY_URL);

var appInfoGauge = new prometheus.Gauge({
  name: 'app_info',
  help: 'Application info',
  labelNames: ['version', 'node_version'],
});
appInfoGauge.labels('0.3.0', '10.19.0').set(1);

var durationGauge = new prometheus.Gauge({
  name: 'batch_duration_seconds',
  help: 'Batch job duration'
});
const end = durationGauge.startTimer();

client.connect()
client.query(sql, factor, (err, res) => {
  end();

  if (err) {
    // error block
    var errorGauge = new prometheus.Gauge({
      name: 'batch_last_error_seconds',
      help: 'Latest batch job error time'
    });
    errorGauge.setToCurrentTime();
    console.log(err.stack)
  }
  else {
    // success block
    var successGauge = new prometheus.Gauge({
      name: 'batch_last_success_seconds',
      help: 'Latest batch job success time'
    });
    successGauge.setToCurrentTime();
    console.log(`Prices updated by factor: ${process.env.PRICE_FACTOR}`)
  }

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
})