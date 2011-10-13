#!/bin/bash

while read command; do
  nc localhost 1234 <<< $command
done
