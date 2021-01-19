-- DROP SCHEMA public;

CREATE SCHEMA public AUTHORIZATION postgres;

COMMENT ON SCHEMA public IS 'standard public schema';


-- Permissions

GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

-- public.do_order definition

-- Drop table

-- DROP TABLE public.do_order;

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

-- Permissions

ALTER TABLE public.do_order OWNER TO postgres;
GRANT ALL ON TABLE public.do_order TO postgres;