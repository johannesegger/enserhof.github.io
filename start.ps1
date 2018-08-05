Push-Location .\src
try {
    dotnet fable yarn-start
}
finally {
    Pop-Location
}