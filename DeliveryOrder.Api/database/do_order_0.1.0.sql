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
-- Table: public.do_order

-- DROP TABLE public.do_order;

CREATE TABLE public.do_order
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    rowstatus smallint NOT NULL DEFAULT 0,
    cargo_owner_id integer NOT NULL,
    do_order_number varchar(24) COLLATE pg_catalog."default" NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) COLLATE pg_catalog."default" NOT NULL,
    modified timestamp,
    modifier varchar(64) COLLATE pg_catalog."default",
    CONSTRAINT do_order_id_pk PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.do_order
    OWNER to postgres;
-- Index: do_order_do_order_number_key

-- DROP INDEX public.do_order_do_order_number_key;

CREATE UNIQUE INDEX do_order_do_order_number_key
    ON public.do_order USING btree
    (do_order_number COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default
    WHERE rowstatus = 0;

--DROP TABLE public.schema_migration;
CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major integer NOT NULL,
    version_minor integer NOT NULL,
    version_patch integer NOT NULL,
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
-- Table: public.do_order

-- DROP TABLE public.do_order;

CREATE TABLE public.do_order
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    rowstatus smallint NOT NULL DEFAULT 0,
    cargo_owner_id integer NOT NULL,
    do_order_number varchar(24) COLLATE pg_catalog."default" NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) COLLATE pg_catalog."default" NOT NULL,
    modified timestamp,
    modifier varchar(64) COLLATE pg_catalog."default",
    CONSTRAINT do_order_id_pk PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.do_order
    OWNER to postgres;
-- Index: do_order_do_order_number_key

-- DROP INDEX public.do_order_do_order_number_key;

CREATE UNIQUE INDEX do_order_do_order_number_key
    ON public.do_order USING btree
    (do_order_number COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default
    WHERE rowstatus = 0;

--DROP TABLE public.schema_migration;
CREATE TABLE public.schema_migration (
    id int4 NOT NULL GENERATED ALWAYS AS IDENTITY,
    version_major integer NOT NULL,
    version_minor integer NOT NULL,
    version_patch integer NOT NULL,
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