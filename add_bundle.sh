git clean -df
git reset
git remote add "$1" "$2"
git fetch "$1"
git checkout "$1/main"
echo "Successfully checked out branch $1/main"