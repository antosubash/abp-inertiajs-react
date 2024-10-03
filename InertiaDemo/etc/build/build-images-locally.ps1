param ($version='latest')

$currentFolder = $PSScriptRoot
$slnFolder = Join-Path $currentFolder "../../"
$appFolder = Join-Path $slnFolder "InertiaDemo"


Write-Host "********* BUILDING Application *********" -ForegroundColor Green
Set-Location $appFolder
dotnet publish -c Release
docker build -f Dockerfile.local -t mycompanyname/inertiademo:$version .

### ALL COMPLETED
Write-Host "********* COMPLETED *********" -ForegroundColor Green
Set-Location $currentFolder