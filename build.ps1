Push-Location src
yarn install --frozen-lockfile
dotnet restore
dotnet fable yarn-build
Pop-Location
