#!/bin/sh
WORKING_DIR="/app"

cd $WORKING_DIR || exit



# Path to the database file

DB_FILE_DIR="/app/db"
DB_FILE="$DB_FILE_DIR/vetcms.db"

# Path to the source database file
SOURCE_DB_FILE="/app/db_scheme/vetcms.db"

# Check if the database file exists
if [ -f "$DB_FILE" ]; then
    echo "Database file already exists. No action needed."
else
    echo "Database file does not exist. Copying from source."
    cp "$SOURCE_DB_FILE" "$DB_FILE_DIR"
    echo "Database file copied successfully."
fi

nginx && dotnet vetcms.WebApi.dll