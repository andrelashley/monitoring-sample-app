package wiredbrain.products;

import java.math.BigDecimal;
import java.util.Arrays;
import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.client.RestTemplate;

@RestController
public class ProductsController {
    private static final Logger log = LoggerFactory.getLogger(ProductsController.class);
    
    @Autowired
	ProductRepository repository;

    @RequestMapping("/products")
    public List<Product> get() {
        log.debug("** GET /products called");
        List<Product> products = null;
        try {
            products = repository.findAll();
        }
        catch (Exception ex) {
            log.error("Product load failed!");
        }        
        return products;
    }
}
