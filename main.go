package main

import (
	"fmt"
	"os"
	"os/signal"
	"syscall"
)

func main() {
	sigCh := make(chan os.Signal, 1)
	fmt.Println("Running Simulator. Press Ctrl+C to exit.")
	signal.Notify(sigCh, syscall.SIGINT)

	

	<-sigCh
	fmt.Println("Ctrl+C received. Exiting.")
}
