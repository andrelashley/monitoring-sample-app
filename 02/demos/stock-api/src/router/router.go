package router

import (
	"github.com/gorilla/mux"
	"stock-api/handlers"
)

func Router() *mux.Router {
	router := mux.NewRouter()

	router.HandleFunc("/stock/{id}", handlers.GetProductStock).Methods("GET", "OPTIONS")
	router.HandleFunc("/stock/{id}", handlers.SetProductStock).Methods("PUT", "OPTIONS")

	return router
}
