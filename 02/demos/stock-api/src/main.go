package main

import (
	"log"
	"net/http"
	"stock-api/router"
)

func main() {
	r := router.Router()
	log.Println("Starting server on port 8080...")
	log.Fatal(http.ListenAndServe(":8080", r))
}
