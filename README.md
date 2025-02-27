# Construction Management Project

## deploy elasticSearch using docker

- pull image from docker
```
docker pull elasticsearch:8.12.0
```
- run the image
```
docker run -d --name elasticsearch \
  -p 9200:9200 -p 9300:9300 \
  -e "discovery.type=single-node" \
  -e "xpack.security.enabled=false" \
  elasticsearch:8.12.0
```
