FROM node:10.19.0-slim AS builder

WORKDIR /src
COPY src/package.json .
RUN npm install

FROM node:10.19.0-slim

CMD ["node", "server.js"]
ENV PGHOST=products-db \
    PGDATABASE=postgres \
    PGUSER=postgres \
    PGPASSWORD=wired \
    PUSHGATEWAY_URL=http://pushgateway:9091/ \
    PRICE_FACTOR=1.1


WORKDIR /app
COPY --from=builder /src/node_modules/ /app/node_modules/
COPY src/ .