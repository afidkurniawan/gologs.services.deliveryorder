------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.notify_party definition

-- Drop table

-- DROP TABLE public.notify_party;

CREATE TABLE public.notify_party
(
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    cargo_owner_id integer NOT NULL,
    notify_address varchar(128) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64),
    CONSTRAINT notify_party_id_pk PRIMARY KEY (id)
)
CREATE UNIQUE INDEX notify_party_key ON public.notify_party USING btree (cargo_owner_id) WHERE rowstatus = 0;

------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.do_notify_party definition

-- Drop table

-- DROP TABLE public.do_notify_party;

CREATE TABLE public.do_notify_party
(
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    do_order_number varchar(24) NOT NULL,
    notify_address varchar(128) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64),
    CONSTRAINT do_notify_party_id_pk PRIMARY KEY (id)
)
CREATE UNIQUE INDEX do_notify_party_key ON public.do_notify_party USING btree (do_order_number) WHERE rowstatus = 0;