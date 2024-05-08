# Derby

## Steps to run

1) Clone the repo
2) run the docker compose to start the mongo storage service
3) run the backend service
4) check swagger health check responds. https://localhost:7072/Insturment/HealthCheck
5) feel free to check hangfire too for scheduled jobs https://localhost:7072/hangfire

## Note worthy items

1) There are over 4000 instruments, the rate limit is 20 requests per second
2) That means to go through every instrument requires 200 seconds assuming 20 requests do actually get processed every second.
3) To query every instrument, the fastest cycle is there for 200 seconds for the most up to date queries
4) please check the derebitService.cs and comment  out line 32 instead of 31 to gather more trades as the current system is polling the most recent trades
