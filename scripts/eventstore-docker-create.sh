#!/bin/sh
sudo docker run --name eventstore-node -it -p 127.0.0.1:2113:2113 -p 127.0.0.1:1113:1113 eventstore/eventstore
