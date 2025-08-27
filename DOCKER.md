# TechFood - Docker Deployment

Este documento contém as instruções para executar a aplicação TechFood usando Docker e Docker Compose.

## Pré-requisitos

- [Docker](https://docs.docker.com/get-docker/) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado

## Getting Started com Docker

Para executar o projeto usando Docker, siga os passos abaixo:

### 1. Clone o repositório

```bash
git clone <repository-url>
cd tech-challenge/fase1
```

### 2. Execute com Docker Compose

No diretório raiz do projeto, execute:

```bash
docker-compose up -d
```

### 3. Acesse as aplicações

Após a inicialização dos containers, as aplicações estarão disponíveis nos seguintes endereços:

- **API Swagger**: http://localhost:5000/api/swagger/index.html
- **Self-Order App**: http://localhost:5000/self-order/
- **Monitor App**: http://localhost:5000/monitor/
- **Admin App**: http://localhost:5000/admin/

### 4. Parar os containers

Para parar e remover os containers:

```bash
docker-compose down
```

## Configurações

### Credenciais do Mercado Pago

Para integração com pagamentos, use as seguintes credenciais de teste:

- **Seller Username**: `TESTUSER1125814911`
- **Seller Password**: `DD1wLKK8sd`
- **Customer Username**: `TESTUSER1370967485`
- **Customer Password**: `ayGV80NpxL`
- **User ID**: `2414323212`
- **Access Token**: `APP_USR-5808215342931102-042817-5d5fee5e46fe9a6b08d17f29e741091f-2414323212`

### String de Conexão do Banco de Dados

A API utiliza a seguinte string de conexão para o SQL Server:

**Connection String**: `Server=techfood.db;Database=dbtechfood;User Id=sa;Password=123456#4EA;TrustServerCertificate=True;`

## Arquitetura dos Containers

O projeto utiliza os seguintes containers:

- **SQL Server**: Banco de dados que armazena todos os dados da aplicação
- **API**: Backend desenvolvido em ASP.NET Core
- **Self-Order**: Frontend para clientes realizarem pedidos
- **Monitor**: Aplicação para monitoramento de pedidos em tempo real
- **Admin**: Interface administrativa para gerenciamento do restaurante
- **NGINX**: Proxy reverso que roteia as requisições

## Volumes e Persistência

Os dados do banco SQL Server são persistidos através de volumes Docker, garantindo que as informações não sejam perdidas quando os containers são reiniciados.

## Logs e Debugging

Para visualizar os logs dos containers:

```bash
# Logs de todos os serviços
docker-compose logs

# Logs de um serviço específico
docker-compose logs <service-name>

# Logs em tempo real
docker-compose logs -f
```

Para acessar o shell de um container:

```bash
docker-compose exec <service-name> /bin/bash
```

## Troubleshooting

### Porta já está em uso

Se você receber erro de porta em uso, verifique se não há outros serviços rodando nas portas utilizadas:

```bash
# Windows
netstat -an | findstr :5000

# Linux/Mac
lsof -i :5000
```

### Containers não inicializam

Verifique se o Docker está rodando e se há recursos suficientes disponíveis:

```bash
docker --version
docker-compose --version
docker system info
```

### Reset completo

Para fazer um reset completo removendo volumes e imagens:

```bash
docker-compose down -v --rmi all
docker system prune -a
```
