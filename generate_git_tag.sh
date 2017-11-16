# This script will crate a git tag based on the latest tag from git plus the build number.
echo Getting Git Tag...
latestTag=$(git tag --list | tail -n 1)
BUILD_NUMBER=30

echo creating tag...
git tag -a $latestTag.$BUILD_NUMBER -m "$latestTag Build $BUILD_NUMBER"


