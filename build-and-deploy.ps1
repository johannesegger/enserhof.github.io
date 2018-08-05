$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir master

Push-Location src
yarn install --frozen-lockfile
dotnet restore
dotnet fable yarn-build
Pop-Location

Copy-Item -Force .\public\** $buildOutputDir

$commitHash = git rev-parse HEAD

Push-Location $buildOutputDir
git add .
git commit -m "Build $commitHash"
git push
Pop-Location

git worktree remove $buildOutputDir
