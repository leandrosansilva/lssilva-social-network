#!/bin/bash

while read comando; do
  nc localhost 1234 <<< $comando
done
