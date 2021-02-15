------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.do_order definition

-- Drop table

-- DROP TABLE public.do_order;

CREATE TABLE public.do_order
(
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    cargo_owner_id int NOT NULL,
    do_order_number varchar(24) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64),
    CONSTRAINT do_order_id_pk PRIMARY KEY (id)
);
CREATE UNIQUE INDEX do_order_do_order_number_key ON public.do_order USING btree (do_order_number) WHERE rowstatus = 0;
