#!/bin/sh
sudo docker run --name postgres-instance -it -p 127.0.0.1:5432:5432 -d postgres
