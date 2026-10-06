#!/bin/bash
# Provisions the wedding login role + database on the external PostgreSQL server.
# Run by the one-shot `wedding-db-init` service (prod profile) as the server's admin role
# $DB_ADMIN_USER (PGHOST / PGPORT / PGPASSWORD). Exits early when the database already exists;
# otherwise creates the missing role (password applied only on creation) and the database.
# Works with a superuser or a CREATEROLE + CREATEDB admin: a non-superuser admin is granted the
# new role, which CREATE DATABASE ... OWNER requires.
set -e

wedding_psql() {
    psql -v ON_ERROR_STOP=1 --no-psqlrc --username "$DB_ADMIN_USER" --dbname postgres "$@"
}

if [ -z "$DB_NAME" ] || [ -z "$DB_USER" ] || [ -z "$DB_PASSWORD" ] || [ -z "$DB_ADMIN_USER" ]; then
    echo "wedding-db-init: DB_NAME, DB_USER, DB_PASSWORD and DB_ADMIN_USER are required." >&2
    exit 1
fi

if ! wedding_psql -tAq -c "SELECT 1" > /dev/null; then
    echo "wedding-db-init: cannot connect to the PostgreSQL server as '${DB_ADMIN_USER}'." >&2
    exit 1
fi

if [ "$(wedding_psql -tAq -v db="$DB_NAME" <<< "SELECT 1 FROM pg_database WHERE datname = :'db';")" = "1" ]; then
    echo "wedding-db-init: database '${DB_NAME}' already exists, nothing to do."
    exit 0
fi

echo "wedding-db-init: provisioning database '${DB_NAME}', role '${DB_USER}'."
wedding_psql -v db="$DB_NAME" -v user="$DB_USER" -v password="$DB_PASSWORD" <<'EOSQL'
SELECT format('CREATE ROLE %I LOGIN PASSWORD %L', :'user', :'password')
WHERE NOT EXISTS (SELECT FROM pg_roles WHERE rolname = :'user') \gexec
SELECT format('GRANT %I TO %I', :'user', current_user)
WHERE NOT (SELECT rolsuper FROM pg_roles WHERE rolname = current_user)
  AND :'user' <> current_user \gexec
SELECT format('CREATE DATABASE %I OWNER %I', :'db', :'user') \gexec
EOSQL
