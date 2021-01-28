------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- DROP SCHEMA public;

--CREATE SCHEMA public AUTHORIZATION postgres;

---COMMENT ON SCHEMA public IS 'standard public schema';


-- Permissions

--GRANT ALL ON SCHEMA public TO postgres;
--GRANT ALL ON SCHEMA public TO public;

-- public.do_order definition

-- Drop table
--DROP TABLE public.do_order;

CREATE TABLE public.do_order (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus int2 NOT NULL DEFAULT 0,
	cargo_owner_id int4 NOT NULL,
	do_order_number varchar(24) NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT do_order_id_pk PRIMARY KEY (id)
);

--DROP TABLE public.schema_migration;
CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major int2 NOT NULL,
    version_minor int2 NOT NULL,
    version_patch int2 NOT NULL,
    db_version varchar(17) GENERATED ALWAYS AS (CAST(version_major AS varchar(5)) || '.' || CAST(version_minor AS varchar(5)) || '.' || CAST(version_patch AS varchar(5))) STORED,
    app_version varchar NOT NULL,
    up_script text NOT NULL,
    down_script text NOT NULL,
    applied timestamp NOT NULL,
    CONSTRAINT schema_migration_pk PRIMARY KEY (id)
);



------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------
INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 0, '0.1.0', '', '', CURRENT_TIMESTAMP);

UPDATE schema_migration
SET    up_script ='
------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- DROP SCHEMA public;

--CREATE SCHEMA public AUTHORIZATION postgres;

---COMMENT ON SCHEMA public IS standard public schema;


-- Permissions

--GRANT ALL ON SCHEMA public TO postgres;
--GRANT ALL ON SCHEMA public TO public;

-- public.do_order definition

-- Drop table
---DROP TABLE public.do_order;

CREATE TABLE public.do_order (
	id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
	rowstatus int2 NOT NULL DEFAULT 0,
	cargo_owner_id int4 NOT NULL,
	do_order_number varchar(24) NOT NULL,
	created timestamp NOT NULL,
	creator varchar(64) NOT NULL,
	modified timestamp NULL,
	modifier varchar(64) NULL,
	CONSTRAINT do_order_id_pk PRIMARY KEY (id)
);

--DROP TABLE public.schema_migration;
CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major int2 NOT NULL,
    version_minor int2 NOT NULL,
    version_patch int2 NOT NULL,
    db_version varchar(17) GENERATED ALWAYS AS (CAST(version_major AS varchar(5)) || ''.'' || CAST(version_minor AS varchar(5)) ||''.'' || CAST(version_patch AS varchar(5))) STORED,
    app_version varchar NOT NULL,
    up_script text NOT NULL,
    down_script text NOT NULL,
    applied timestamp NOT NULL,
    CONSTRAINT schema_migration_pk PRIMARY KEY (id)
);



------------------------------------------------------------------------------------------------------------------------
-- DATA
------------------------------------------------------------------------------------------------------------------------
INSERT INTO public.schema_migration
(version_major, version_minor, version_patch, app_version, up_script, down_script, applied)
VALUES (0, 1, 0, ''0.1.0'', '', '', CURRENT_TIMESTAMP);'
WHERE db_version = '0.1.0';