# Demo 3

Adding applications metrics to a Java REST API.

## 3.1 - Add the Micrometer dependency & config

Micrometer is a wrapper, abstracting the Prometheus client library; the Sprin Actuator provides the metrics endpoint. Add the dependencies in [pom.xml](products-api/pom.xml).

```        
<dependency>
    <groupId>org.springframework.boot</groupId>
    <artifactId>spring-boot-starter-actuator</artifactId>
</dependency>
<dependency>
    <groupId>io.micrometer</groupId>
    <artifactId>micrometer-registry-prometheus</artifactId>
    <version>1.5.4</version>
</dependency>
```

And configure the actuator to expose the Prometheus metrics in [application.properties](products-api/src/main/resources/application.properties)

```
management.endpoints.web.exposure.include=prometheus
```

Rebuild and deploy:

```
docker-compose build products-api

docker-compose up -d products-api
```

> Browse to http://localhost:8081/actuator/prometheus

## 3.2 - Add info metric

Create the new file and walk through code in [ApplicationStartup.java](products-api/src/main/java/com/wiredbrain/startup\ApplicationStartup.java).

Sets app info gauge in the run method:

```
registry.gauge("app.info", Tags.of("version", "0.2.0", "java.version", "11-jre"), appInfoGaugeValue);
```

Rebuild and deploy:

```
docker-compose build products-api

docker-compose up -d products-api
```

> Check app_info in  http://localhost:8081/actuator/prometheus