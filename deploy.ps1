$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir master
Copy-Item -Force .\public\** $buildOutputDir
$commitHash = git rev-parse HEAD

Push-Location $buildOutputDir
git status
git add .
git commit -m "Build $commitHash"
git push
Pop-Location

git worktree remove $buildOutputDir
