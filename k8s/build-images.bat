@echo off
REM Script para fazer build das imagens Docker no Minikube

echo 🐳 Fazendo build das imagens Docker para o Minikube...

REM Configura o ambiente Docker para usar o Minikube
echo 🔧 Configurando ambiente Docker do Minikube...
FOR /f "tokens=*" %%i IN ('minikube docker-env --shell cmd') DO %%i

REM Build da API
echo 📦 Building TechFood.Api...
docker build -t techfood.api:latest -f src/TechFood.Api/Dockerfile .
if errorlevel 1 (
    echo ❌ Erro ao fazer build da API
    exit /b 1
)

REM Build do Admin
echo 📦 Building TechFood.Admin...
docker build -t techfood.admin:latest -f apps/admin/Dockerfile .
if errorlevel 1 (
    echo ❌ Erro ao fazer build do Admin
    exit /b 1
)

REM Build do Self-Order
echo 📦 Building TechFood.Self-Order...
docker build -t techfood.self-order:latest -f apps/self-order/Dockerfile .
if errorlevel 1 (
    echo ❌ Erro ao fazer build do Self-Order
    exit /b 1
)

REM Build do Monitor
echo 📦 Building TechFood.Monitor...
docker build -t techfood.monitor:latest -f apps/monitor/Dockerfile .
if errorlevel 1 (
    echo ❌ Erro ao fazer build do Monitor
    exit /b 1
)

REM Build do Nginx
echo 📦 Building TechFood.Nginx...
docker build -t techfood.nginx:latest -f nginx/Dockerfile nginx/
if errorlevel 1 (
    echo ❌ Erro ao fazer build do Nginx
    exit /b 1
)

echo ✅ Todas as imagens foram criadas com sucesso!
echo.
echo Para fazer o deploy, execute:
echo kubectl apply -k k8s/overlays/development/
echo.
echo Ou use o script de deploy:
echo k8s\deploy.bat
pause
