# Deploying Docker to EC2 🐳 ☁️


### Repository Secrets

> To use this workflow you will need to add several [repository secrets](https://docs.github.com/en/actions/security-guides/using-secrets-in-github-actions)

```yaml
env:
  EC2_IP_ADDRESS: ${{ secrets.EC2_IP_ADDRESS }}
  EC2_USERNAME: ${{ secrets.EC2_USERNAME }}
  EC2_PEM_KEY: ${{ secrets.EC2_PEM_KEY }}
  REPO_NAME: ${{ secrets.REPO_NAME }}
  DOCKER_USERNAME: ${{ secrets.DOCKER_USERNAME }}
  DOCKER_PASSWORD: ${{ secrets.DOCKER_PASSWORD }}
  DOCKER_COMPOSE: ${{ secrets.DOCKER_COMPOSE }}
```


### Environment Variables in Docker
To keep your environment variables safe during the build step you will need to create a DOCKER_COMPOSE secret which should be formatted as below. 

> ⚠️ This file will be built by the github action and should not be found in your actual source code

_docker-compose.yml_
```yaml
version: '3'
services:
  app:
    image: porteryoung/keeper:latest
    ports:
      - "7045:80"  # EC2_PORT:CONTAINER_PORT
    environment:
      - CONNECTION_STRING="Server=league.mysql.database.azure.com;Port=3306;Database=keeper;Uid=P0rt3r76;Pwd=Y0un9677;SslMode=None;"
      - AUTH_DOMAIN='dev-hhcu2170fadrfsij.us.auth0.com'
      - AUTH_AUDIENCE='https://PortersAuthTest/'
      - AUTH_CLIENT_ID='9ky5h2ESpCSBmkWByEuMajyVhAiNBsZN'
```


This workflow is triggered when changes are made to the `production` branch
