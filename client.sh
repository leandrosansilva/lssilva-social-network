#!/bin/bash

while read comando; do
  telnet localhost 1234 <<< $comando
done
