#!/bin/bash

while read comando; do
  echo -e $comando  | telnet localhost 1234
done
