FROM oven/bun:latest as base
WORKDIR /app

FROM base AS install
ARG NODE_ENV
ENV NODE_ENV $NODE_ENV
RUN mkdir -p /temp/dev
COPY package.json /temp/dev/
RUN cd /temp/dev && bun install

RUN mkdir -p /temp/prod
COPY package.json /temp/prod/
RUN cd /temp/prod && bun install

FROM base AS development
COPY . .
COPY --from=install /temp/prod/node_modules node_modules
COPY --from=install /temp/dev/.nuxt .nuxt

EXPOSE 3000
EXPOSE 4000
EXPOSE 24678
ENTRYPOINT [ "bun", "--hot", "run", "dev"]
