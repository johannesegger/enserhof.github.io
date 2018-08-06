$buildOutputDir = ".\build-tmp"

git worktree add $buildOutputDir master
Remove-Item $buildOutputDir -Exclude .git -Recurse -Force
Copy-Item -Force .\public\** $buildOutputDir
$commitHash = git rev-parse HEAD

Push-Location $buildOutputDir
git add .
git status
git commit -m "Build $commitHash"
git push
Pop-Location

git worktree remove $buildOutputDir
