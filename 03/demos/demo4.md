# Demo 4

Adding custom metrics for API functions.

## 4.1 - Add data load counter

Record a custom metric when the controller fetches data from the database - in [ProductsController.java](products-api/src/main/java/com/wiredbrain/controllers/ProductsController.java).

```
// imports
import io.micrometer.core.instrument.MeterRegistry;

// declarations
@Autowired
MeterRegistry registry;

// in get() 
registry.counter("products_data_load_total", "status", "called").increment();

// in catch block
registry.counter("products_data_load_total", "status", "failure").increment();
```

Rebuild & deploy:

```
docker-compose build products-api

docker-compose up -d products-api
```

> Call into the API at http://localhost:8081/products and check `products_data_load_total` in  http://localhost:8081/actuator/prometheus

## 4.2 - Wire up timed aspect for controller

Micrometer supports Aspect-Oriented Programming where you can decorate methods to have the execution time collected as a metric. 

First you need to register the aspect - add `configuration` folder and walk through [RegistryConfiguration.java](products-api/src/main/java/com/wiredbrain/configuration/RegistryConfiguration.java).

Then add the `Timed` aspect in [ProductsController.java](products-api/src/main/java/com/wiredbrain/controllers/ProductsController.java).

```
// imports
import io.micrometer.core.annotation.Timed;

// no declarations

// decorate get() 
@Timed()
```

Rebuild & deploy:

```
docker-compose build products-api

docker-compose up -d products-api
```

> Refresh API and check metrics at  http://localhost:8081/actuator/prometheus