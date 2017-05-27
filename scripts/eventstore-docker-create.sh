#!/bin/sh
sudo docker run --name eventstore-node -it -p 2113:2113 -p 1113:1113 eventstore/eventstore
