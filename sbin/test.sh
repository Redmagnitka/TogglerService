#!/usr/bin/env bash

set -e

cd Toggler.Tests

# will set up 3 toggles and 2 apps using the HTTP API interface
echo $(cat <<EOM 

Assuming that you have already spinned up the API HTTP server.
If not, then run 
  
  make run

in another shell first.

EOM
)

# unit and acceptance tests
dotnet xunit

# tests against HTTP interface
newman run --environment acceptance/toggler_dev.postman_environment.json \
  acceptance/toggler.postman_collection.json
