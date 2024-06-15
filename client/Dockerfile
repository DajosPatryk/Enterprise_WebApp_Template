ARG NODE_IMAGE=oven/bun:latest
FROM $NODE_IMAGE AS base
WORKDIR /app

FROM base AS setup
COPY package.json ./
RUN bun install

FROM base AS development
COPY --from=setup /app/node_modules ./node_modules
COPY . .

EXPOSE 3000

ENTRYPOINT ["bun", "--hot", "run", "dev"]
