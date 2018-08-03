Param(
  [Parameter(Mandatory=$True)][string]$commitMessage
)

$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir master

Push-Location src
dotnet fable yarn-build
Pop-Location

Copy-Item -Force .\public\** $buildOutputDir

Push-Location $buildOutputDir
git add .
git commit -m "$commitMessage"
git push
Pop-Location

git worktree remove $buildOutputDir
